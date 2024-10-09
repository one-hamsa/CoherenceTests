using System;
using System.Linq;
using Coherence.Toolkit;
using Coherence.Toolkit.Bindings;
using UnityEngine;

[DefaultExecutionOrder(1000)]
public class RigidbodySync : MonoBehaviour
{
    public bool debug;
    
    [Range(0, 1)] 
    public float baseReconciliationRate = 0.5f;
    
    private Rigidbody _rigidbody;
    private CoherenceBridge _bridge;
    private CoherenceSync _coherenceSync;
    
    private Collider _collider;

    private float _reconciliationRate = 1f;

    private HistoryBuffer<Vector3> _positionHistory = new Vector3HistoryBuffer();
    private HistoryBuffer<Quaternion> _rotationHistory = new QuaternionHistoryBuffer();

    [Sync, NonSerialized]
    public double dataUpdateTime;

    [Sync, NonSerialized]
    public Vector3 position;
    [Sync, NonSerialized]
    public  Vector3 velocity;
    [Sync, NonSerialized]
    public  Quaternion rotation;
    [Sync, NonSerialized] 
    public  Vector3 angularVelocity;

    private Vector3 _lastPositionError;
    private Quaternion _lastRotationError;

    private ValueBinding<double> dataUpdateBinding;

    private bool _remoteDataUpdated;

    private double _lastRemoteDataUpdateTime;
    private double _remoteDataUpdateTime;
    private double _networkTimeWhenLastRemoteDataUpdated;
    private Vector3 _positionUpdateError;
    private Quaternion _rotationUpdateError;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _coherenceSync = GetComponentInParent<CoherenceSync>();
        _coherenceSync.InterpolationLocationConfig = CoherenceSync.InterpolationLoop.UpdateAndFixedUpdate;
        _collider = GetComponentInChildren<Collider>();
    }

    private void OnEnable()
    {
        dataUpdateBinding = (ValueBinding<double>)_coherenceSync.Bindings.Last(b => b.Name == "dataUpdateTime");
    }

    private void Start()
    {
        if (!CoherenceBridgeStore.TryGetBridge(gameObject.scene, out _bridge))
        {
            Debug.LogWarning("Couldn't find CoherenceBridge in the scene.");
            return;
        }
    }

    private void Update()
    {
        if (debug)
            Debug.Log($"{Time.fixedTime}: Update (in Fixed = {Time.inFixedTimeStep})");
    }

    public bool useSimpleReconciliation = false;
    
    private void FixedUpdate()
    {
        if (!_coherenceSync.HasStateAuthority)
        {
            _remoteDataUpdateTime = dataUpdateBinding.Interpolator.Buffer.Last.Value.Time;
            if (_remoteDataUpdateTime > _lastRemoteDataUpdateTime + 0.001)
                _remoteDataUpdated = true;
        }

        if (debug)
            Debug.Log($"{Time.fixedTime}: FixedUpdate (have new data = {_remoteDataUpdated})");

        // Add current state to history buffers
        double currentNetworkTime = _bridge.Client.NetworkTime.TimeAsDouble;
        _positionHistory.AddSample(currentNetworkTime, _rigidbody.position);
        _rotationHistory.AddSample(currentNetworkTime, _rigidbody.rotation);
        
        if (_coherenceSync.HasStateAuthority)
        {
            position = _rigidbody.position;
            velocity = _rigidbody.velocity;
            rotation = _rigidbody.rotation;
            angularVelocity = _rigidbody.angularVelocity;
            dataUpdateTime = currentNetworkTime;
        }
        else
        {
            Vector3 futurePos = position;
            Quaternion futureRot = rotation;

            _lastPredictedPos = futurePos;
            _lastPredictedRot = futureRot;

            float propertyRefreshInterval = 1f/20f; // 20Hz updates
            float rate = baseReconciliationRate;// * _reconciliationRate;
            float fixedDt = Time.fixedDeltaTime;
            float invFixedDT = 1f / fixedDt;
            Vector3 velocityChange = Vector3.zero;
            Vector3 angularVelocityChange = Vector3.zero;
            
            if (useSimpleReconciliation)
            {
                Vector3 deltaPos = futurePos - _rigidbody.position;
                velocityChange = (deltaPos * invFixedDT - _rigidbody.velocity) * rate;
            }
            else
            {
                if (_remoteDataUpdated)
                {
                    // We have a new (past) position sample - correct the error we had in the past
                    Vector3 myPositionAtTimeOfSample = _positionHistory.Evaluate(_remoteDataUpdateTime);
                    Quaternion myRotationAtTimeOfSample = _rotationHistory.Evaluate(_remoteDataUpdateTime);
                    _positionUpdateError = futurePos - myPositionAtTimeOfSample;
                    _rotationUpdateError = RigidbodyPredictor.ShortWorldSpaceDeltaTo(myRotationAtTimeOfSample, futureRot);
                    _remoteDataUpdated = false;
                    _networkTimeWhenLastRemoteDataUpdated = currentNetworkTime;
                }
                
                // use extrapolated positions to fix towards 
                double extrapolationDT = (currentNetworkTime - _remoteDataUpdateTime) * 0.5;
                
                // Limit extrapolation to not look too much into the future (not getting data?)
                if (extrapolationDT > 0.1f)
                    extrapolationDT = 0.1f + Math.Log((extrapolationDT - 0.1)*50f + 1) / 50f;
                
                if (extrapolationDT > 0) 
                    RigidbodyPredictor.PredictFutureState(_rigidbody, position, rotation, velocity, angularVelocity, (float)extrapolationDT, out futurePos, out futureRot);
                
                Vector3 deltaPos = futurePos - _rigidbody.position;
                velocityChange = (deltaPos * invFixedDT - _rigidbody.velocity) * (rate * _reconciliationRate);

                Quaternion deltaRotation = RigidbodyPredictor.ShortWorldSpaceDeltaTo(_rigidbody.rotation, futureRot);
                RigidbodyPredictor.SafeToAngleAxis(deltaRotation, out float angleDegrees, out Vector3 axis);
                angularVelocityChange = axis * (Mathf.Deg2Rad * angleDegrees * invFixedDT * rate) - _rigidbody.angularVelocity;

                // Last error correction (smeared across the property refresh interval - which is currently 20Hz)
                double timeSinceLastUpdate = currentNetworkTime - _networkTimeWhenLastRemoteDataUpdated;
                if (timeSinceLastUpdate <= propertyRefreshInterval)
                {
                    float curDT = Mathf.Min(fixedDt, (float)(propertyRefreshInterval - timeSinceLastUpdate));
                    float currentErrorFix = Mathf.Clamp01(curDT / propertyRefreshInterval);
                    velocityChange += (_positionUpdateError * (currentErrorFix * invFixedDT) - _rigidbody.velocity) * (rate * _reconciliationRate);
                    RigidbodyPredictor.SafeToAngleAxis(_rotationUpdateError, out float errorAngle, out Vector3 errorAxis);
                    angularVelocityChange += (errorAxis * (errorAngle * currentErrorFix * invFixedDT) - _rigidbody.angularVelocity) * (rate * _reconciliationRate);
                    _positionHistory.ApplyOffset(_positionUpdateError * currentErrorFix);
                    _rotationHistory.ApplyOffset(Quaternion.AngleAxis(errorAngle * currentErrorFix, errorAxis) );
                }
            }

            _rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
            _rigidbody.AddTorque(angularVelocityChange, ForceMode.VelocityChange);
        }
    }

    private Vector3 _lastPredictedPos;
    private Quaternion _lastPredictedRot;
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;
        if (_coherenceSync == null)
            return;
        
        Gizmos.color = Color.green;
        if (!_coherenceSync.HasStateAuthority && _collider != null)
        {
            if (_collider is SphereCollider sc)
                Gizmos.DrawWireSphere( position + rotation * (sc.transform.localPosition + sc.center), sc.radius);
            if (_collider is BoxCollider bc)
            {
                Gizmos.matrix = Matrix4x4.TRS(position + rotation * (_collider.transform.localPosition + bc.center), rotation, _collider.transform.lossyScale);
                Gizmos.DrawWireCube(bc.center, bc.size);
            }
        }
    }

    public static class RigidbodyPredictor
    {
        public static Quaternion ShortWorldSpaceDeltaTo(Quaternion from, Quaternion to) {
            var delta = to * Quaternion.Inverse(from);
            if (delta.w < 0) {
                // Convert the quaterion to eqivalent "short way around" quaterion
                delta.x = -delta.x;
                delta.y = -delta.y;
                delta.z = -delta.z;
                delta.w = -delta.w;
            }
            return delta;
        }

        public static void SafeToAngleAxis(Quaternion q, out float angle, out Vector3 axis)
        {
            q.ToAngleAxis(out angle, out axis);
            float sqrMag = axis.sqrMagnitude;
            if (sqrMag < 0.9f || sqrMag > 1.1f || float.IsNaN(sqrMag))
            {
                angle = 0;
                axis = new Vector3(0, 1, 0);
            }
            else
            {
                if (angle > 180f)
                    angle -= 360f;
            }
        }

        public static void PredictFutureState(Rigidbody rb, Vector3 startPos, Quaternion startRot, Vector3 velocity, Vector3 angularVelocity, float dt, out Vector3 futurePosition, out Quaternion futureRotation)
        {
            // Get initial properties
            Vector3 x0 = startPos;
            Quaternion R0 = startRot;
            Vector3 V0 = velocity;
            Vector3 Omega0 = angularVelocity;

            float mass = rb.mass;
            float drag = rb.drag;
            float angularDrag = rb.angularDrag;

            // Linear motion prediction
            Vector3 xt;
            Vector3 Vt;
            if (drag > 0f)
            {
                float k = drag / mass;
                float expFactor = Mathf.Exp(-k * dt);
                Vt = V0 * expFactor;
                xt = x0 + (V0 / k) * (1 - expFactor);
            }
            else
            {
                // No linear drag
                Vt = V0;
                xt = x0 + V0 * dt;
            }

            // Angular motion prediction
            Quaternion deltaRotation;
            if (angularDrag > 0f && Omega0 != Vector3.zero)
            {
                float expFactor = Mathf.Exp(-angularDrag * dt);
                Vector3 Omega_t = Omega0 * expFactor;

                float angle = Omega0.magnitude * (1 - expFactor) / angularDrag;
                Vector3 axis = Omega0.normalized;

                deltaRotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, axis);
            }
            else
            {
                // No angular drag
                Vector3 Omega_t = Omega0;
                float angle = Omega0.magnitude * dt;
                Vector3 axis = Omega0.normalized;

                deltaRotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, axis);
            }

            Quaternion Rt = R0 * deltaRotation;

            // Output future state
            futurePosition = xt;
            futureRotation = Rt;
        }
    }

    public void SetReconciliationRate(float reconciliationRate)
    {
        _reconciliationRate = reconciliationRate;
    }
}
