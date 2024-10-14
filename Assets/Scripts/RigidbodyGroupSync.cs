using System;
using System.Collections;
using System.Collections.Generic;
using Coherence;
using Coherence.Toolkit;
using UnityEngine;

public class RigidbodyGroupSync : MonoBehaviour
{
    [Sync]
    public int frame;

    public float impactScale = 200f;
    
    private CoherenceSync _sync;
    private List<Rigidbody> _rigidbodies = new();
    private List<RigidbodySync> _rigidbodySyncs = new();

    private float _reconciliationRate = 1f;

    private List<Impact> _impacts = new(); 
    
    private const int InternalReconcileFrames = 40;

    private CoherenceBridge _bridge;

    private class Impact
    {
        public int rigidbodyIndex;
        public Vector3 localPos;
        public Vector3 worldImpulse;
        public int numFrames;
        public int curFrame;
    }

    private void Awake()
    {
        _sync = GetComponent<CoherenceSync>();
    }

    void Start()
    {
        GetComponentsInChildren(_rigidbodies);
        foreach (var rb in _rigidbodies)
            _rigidbodySyncs.Add(rb.GetComponent<RigidbodySync>());
        
        if (!CoherenceBridgeStore.TryGetBridge(gameObject.scene, out _bridge))
        {
            Debug.LogWarning("Couldn't find CoherenceBridge in the scene.");
            return;
        }
    }

    [Command]
    public void SendImpulse(int rigidbodyIndex, Vector3 localPos, Vector3 worldImpulse, int numFrames)
    {
        Debug.Log($"Adding impact");
        
        // Impacts when I don't have authority a smeared across more frames to account for the time it would take 
        // the remote authority to actually start moving things (he won't add extra frames)
        // NOTE:
        //  we need 2 round trip times, 1st for the impact to arrive - 2nd for the affect of the impact to return
        //  this way, when the local impulse should end when the remote impulse ends and the data arrives
        float latencyDT = _bridge.Client.Ping.LatestLatencyMs * 0.001f * 4f; 
        if (!_sync.HasStateAuthority)
            numFrames += Mathf.CeilToInt(latencyDT / Time.fixedDeltaTime);
        
        _impacts.Add(new Impact
        {
            curFrame = 0, 
            numFrames = numFrames, 
            localPos = localPos, 
            worldImpulse = worldImpulse, 
            rigidbodyIndex = rigidbodyIndex
        });
    }

    private void Update()
    {
        if (_sync.HasStateAuthority && Input.GetKeyDown(KeyCode.Space) && Player.other != null)
        {
            Vector3 impactDir = UnityEngine.Random.onUnitSphere;
            impactDir.y = 0f;
            impactDir.Normalize();
            
            var otherGroupSync = Player.other.GetComponent<RigidbodyGroupSync>();
            otherGroupSync._sync.SendCommand<RigidbodyGroupSync>(nameof(SendImpulse),
                MessageTarget.All,
                0,
                Vector3.right * 4f, 
                impactDir * impactScale, 
                1);
        }
        
        if (_sync.HasStateAuthority && Input.GetKeyDown(KeyCode.J) && Player.other != null)
        {
            Vector3 impactDir = UnityEngine.Random.onUnitSphere + Vector3.up * 2f;
            impactDir.Normalize();

            var otherGroupSync = Player.other.GetComponent<RigidbodyGroupSync>();
            otherGroupSync._sync.SendCommand<RigidbodyGroupSync>(nameof(SendImpulse),
                MessageTarget.All,
                0,
                new Vector3(), 
                impactDir * impactScale, 
                4);
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < _impacts.Count; i++)
        {
            var impact = _impacts[i];
            var rb = _rigidbodies[impact.rigidbodyIndex];
            Vector3 worldPos = rb.transform.TransformPoint(impact.localPos);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(worldPos, worldPos + impact.worldImpulse.normalized);
        }
    }

    private void FixedUpdate()
    {
        _reconciliationRate = 1f;
        for (int i = 0; i < _impacts.Count; i++)
        {
            var impact = _impacts[i];
            var rb = _rigidbodies[impact.rigidbodyIndex];
            Vector3 worldPos = rb.rotation * impact.localPos + rb.position;


            impact.curFrame++;

            if (impact.curFrame <= impact.numFrames)
            {
                // Add the force for all clients (local prediction)
                rb.AddForceAtPosition(impact.worldImpulse/impact.numFrames, worldPos);
                _reconciliationRate = Mathf.Min(0f, _reconciliationRate);
            }
            else
            {
                // Keep the "impact" alive while we're ramping up reconciliation ... 
                float rate = (float)(impact.curFrame - impact.numFrames) / InternalReconcileFrames;
                _reconciliationRate = Mathf.Min(rate * rate, _reconciliationRate);
                if (rate >= 1f)
                {
                    // Remove impact
                    _impacts.RemoveAt(i);
                    i--;
                    continue;
                }
            }
        }

        if (_sync.HasStateAuthority)
            _reconciliationRate = 1f;

        foreach (var rbs in _rigidbodySyncs)
            rbs.SetReconciliationRate(_reconciliationRate);
    }

    IEnumerator MonitorCo()
    {
        //_sync.SendCommand(SendImpulse, MessageTarget.All, Vector3.zero, Vector3.zero, 0);

        while (true)
        {
            yield return new WaitForSeconds(1f);
            CoherenceBridge b = _sync.CoherenceBridge;
            foreach (var c in b.ClientConnections.GetOther())
            {
                Debug.Log($"Other client {c.ClientId} frame is: {c.CoherenceBridge.NetworkTime.ClientFixedSimulationFrame} (mine is: {_sync.CoherenceBridge.ClientFixedSimulationFrame}");
            }
            
        }
    }
}
