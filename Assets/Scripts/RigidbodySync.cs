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

    private void OnPositionUpdated(Vector3 oldPos, Vector3 newPos)
    {
        if (posBinding != null && posBinding.Interpolator.Buffer.Last != null)
        {
            double sampleTime = posBinding.Interpolator.Buffer.Last.Value.Time;
            var clientPositionAtTimeOfSample = _positionHistory.Evaluate(sampleTime);
            if (debug)
                Debug.Log($"{Time.fixedTime}: Got RBPosUpdate (in Fixed = {Time.inFixedTimeStep})");
            float invFixedDT = 1f / Time.fixedDeltaTime;
            Vector3 deltaPos = position - clientPositionAtTimeOfSample;
            Vector3 animatedVelocity = deltaPos * invFixedDT - _rigidbody.velocity;
		
            // Quaternion deltaRotation = RigidbodyPredictor.ShortWorldSpaceDeltaTo(_rigidbody.rotation, futureRot);
            // RigidbodyPredictor.SafeToAngleAxis(deltaRotation, out float angleDegrees, out Vector3 axis);
            // Vector3 animatedAngularVelocity = axis * (Mathf.Deg2Rad * angleDegrees * invFixedDT) - _rigidbody.angularVelocity;
            //
            // float rate = baseReconciliationRate * _reconciliationRate;
            // _rigidbody.AddForce(animatedVelocity * rate, ForceMode.VelocityChange);
            // _rigidbody.AddTorque(animatedAngularVelocity * rate, ForceMode.VelocityChange);
            
        }
    }
    private void OnRotationUpdated(Quaternion oldRot, Quaternion newRot) { }

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

        double clientTime = _bridge.Client.NetworkTime.TimeAsDouble;
        _positionHistory.AddSample(clientTime, _rigidbody.position);
        _rotationHistory.AddSample(clientTime, _rigidbody.rotation);
        
        if (_coherenceSync.HasStateAuthority)
        {
            position = _rigidbody.position;
            velocity = _rigidbody.velocity;
            rotation = _rigidbody.rotation;
            angularVelocity = _rigidbody.angularVelocity;
        }
        else
        {
            Vector3 futurePos = position;
            Quaternion futureRot = rotation;

            // TODO: Can we do something with extrapolation here, how far do we need to extrapolate?
            //float dt = (_bridge.NetworkTime.ClientSimulationFrame - _bridge.NetworkTime.ServerSimulationFrame) * Time.fixedDeltaTime;
            //if (dt > 0f)
                //RigidbodyPredictor.PredictFutureState(_rigidbody, position, rotation, velocity, angularVelocity, dt, out futurePos, out futureRot);

            _lastPredictedPos = futurePos;
            _lastPredictedRot = futureRot;

            Vector3 myPositionAtTimeOfSample = _positionHistory.Evaluate(_bridge.Client.NetworkTime.TimeAsDouble - _bridge.Client.Ping.LatestLatencyMs * 0.001f);
            Quaternion myRotationAtTimeOfSample = _rotationHistory.Evaluate(_bridge.Client.NetworkTime.TimeAsDouble - _bridge.Client.Ping.LatestLatencyMs * 0.001f);

            float invFixedDT = 1f / Time.fixedDeltaTime;
            Vector3 deltaPos = futurePos - myPositionAtTimeOfSample;
            Vector3 animatedVelocity = deltaPos * invFixedDT - _rigidbody.velocity;
		
            Quaternion deltaRotation = RigidbodyPredictor.ShortWorldSpaceDeltaTo(myRotationAtTimeOfSample, futureRot);
            RigidbodyPredictor.SafeToAngleAxis(deltaRotation, out float angleDegrees, out Vector3 axis);
            Vector3 animatedAngularVelocity = axis * (Mathf.Deg2Rad * angleDegrees * invFixedDT) - _rigidbody.angularVelocity;

            float rate = baseReconciliationRate;// * _reconciliationRate;
            _rigidbody.AddForce(animatedVelocity * rate, ForceMode.VelocityChange);
            _rigidbody.AddTorque(animatedAngularVelocity * rate, ForceMode.VelocityChange);
        }
    }

    private Vector3 _lastPredictedPos;
    private Quaternion _lastPredictedRot;
    private void OnDrawGizmos()
    {
        // Gizmos.color = Color.green;
        // Gizmos.DrawWireSphere(_lastPredictedPos, 0.025f);
        // Gizmos.color = Color.blue;
        // Gizmos.DrawLine(_lastPredictedPos, _lastPredictedPos + _lastPredictedRot * Vector3.forward);
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
