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

    [Sync, NonSerialized, OnValueSynced(nameof(OnPositionUpdated))]
    public Vector3 position;
    [Sync, NonSerialized]
    public  Vector3 velocity;
    [Sync, NonSerialized, OnValueSynced(nameof(OnRotationUpdated))]
    public  Quaternion rotation;
    [Sync, NonSerialized] 
    public  Vector3 angularVelocity;

    private Vector3 _lastPositionError;
    private Quaternion _lastRotationError;

    private ValueBinding<Vector3> posBinding;
    private ValueBinding<Quaternion> rotBinding;

    private bool _positionUpdated;
    private bool _rotationUpdated;
    
    private double _positionUpdatedTime;
    private Vector3 _positionUpdateError;

    private void OnPositionUpdated(Vector3 oldPos, Vector3 newPos) => _positionUpdated = true;
    private void OnRotationUpdated(Quaternion oldRot, Quaternion newRot) => _rotationUpdated = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _coherenceSync = GetComponentInParent<CoherenceSync>();
        _coherenceSync.InterpolationLocationConfig = CoherenceSync.InterpolationLoop.UpdateAndFixedUpdate;
        _collider = GetComponentInChildren<Collider>();
    }

    private void Start()
    {
        if (!CoherenceBridgeStore.TryGetBridge(gameObject.scene, out _bridge))
        {
            Debug.LogWarning("Couldn't find CoherenceBridge in the scene.");
            return;
        }
        

        posBinding = (ValueBinding<Vector3>)_coherenceSync.Bindings.Last(b => b.Name == "position");
        rotBinding = (ValueBinding<Quaternion>)_coherenceSync.Bindings.Last(b => b.Name == "rotation");
    }

    private void Update()
    {
        if (debug)
            Debug.Log($"{Time.fixedTime}: Update (in Fixed = {Time.inFixedTimeStep})");
    }

    private void FixedUpdate()
    {
        if (debug)
            Debug.Log($"{Time.fixedTime}: FixedUpdate");

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
        }
        else
        {
            _rigidbody.detectCollisions = false;
            
            Vector3 futurePos = position;
            Quaternion futureRot = rotation;

            _lastPredictedPos = futurePos;
            _lastPredictedRot = futureRot;

            float propertyRefreshInterval = 1f/20f; // 20Hz updates
            float rate = baseReconciliationRate;// * _reconciliationRate;
            float fixedDt = Time.fixedDeltaTime;
            float invFixedDT = 1f / fixedDt;
            float latencyDT = _bridge.Client.Ping.LatestLatencyMs * 0.001f * 2f; // approximation - TODO: figure out if we can make it more accurate (by getting this data from Coherence)
            Vector3 velocityChange = Vector3.zero;
            if (!Input.GetKey(KeyCode.Z))
            {
                if (_positionUpdated)
                {
                    // We have a new (past) position sample - correct the error we had in the past
                    _positionUpdatedTime = currentNetworkTime;
                    Vector3 myPositionAtTimeOfSample = _positionHistory.Evaluate(_positionUpdatedTime - latencyDT);
                    _positionUpdateError = futurePos - myPositionAtTimeOfSample;
                    //_positionUpdated = false;

                    // Vector3 deltaPos = futurePos - myPositionAtTimeOfSample;
                    // animatedVelocity = deltaPos * invFixedDT - _rigidbody.velocity;
                    // _positionUpdated = false;
                    //
                    // _positionHistory.ApplyOffset(deltaPos * rate);
                }
                // use extrapolated positions to fix towards 
                double extrapolationDT = (currentNetworkTime - (_positionUpdatedTime - latencyDT)) * 0.5f;
                if (extrapolationDT > 0) 
                    RigidbodyPredictor.PredictFutureState(_rigidbody, position, rotation, velocity, angularVelocity, (float)extrapolationDT, out futurePos, out futureRot);
                Vector3 deltaPos = futurePos - _rigidbody.position;
                //velocityChange = (deltaPos * invFixedDT - _rigidbody.velocity) * rate;
                
                // Last error correction (smeared across the property refresh interval - which is currently 20Hz)
                double timeSinceLastUpdate = currentNetworkTime - _positionUpdatedTime;
                if (timeSinceLastUpdate <= propertyRefreshInterval)
                {
                    float curDT = Mathf.Min(fixedDt, (float)(propertyRefreshInterval - timeSinceLastUpdate));
                    float currentErrorFix = Mathf.Clamp01(curDT / propertyRefreshInterval);
                    velocityChange += (_positionUpdateError * (currentErrorFix * invFixedDT) - _rigidbody.velocity) * rate;
                    _positionHistory.ApplyOffset(_positionUpdateError * currentErrorFix);
                }
            }
            else
            {
                Vector3 deltaPos = futurePos - _rigidbody.position;
                velocityChange = deltaPos * invFixedDT - _rigidbody.velocity;
            }
            // Vector3 myPositionAtTimeOfSample = _positionHistory.Evaluate(_bridge.Client.NetworkTime.TimeAsDouble - _bridge.Client.Ping.LatestLatencyMs * 0.001f);
            // Quaternion myRotationAtTimeOfSample = _rotationHistory.Evaluate(_bridge.Client.NetworkTime.TimeAsDouble - _bridge.Client.Ping.LatestLatencyMs * 0.001f);

		
            Quaternion deltaRotation = RigidbodyPredictor.ShortWorldSpaceDeltaTo(_rigidbody.rotation, futureRot);
            RigidbodyPredictor.SafeToAngleAxis(deltaRotation, out float angleDegrees, out Vector3 axis);
            Vector3 angularVelocityChange = axis * (Mathf.Deg2Rad * angleDegrees * invFixedDT * rate) - _rigidbody.angularVelocity;

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
