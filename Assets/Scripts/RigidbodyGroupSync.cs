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
    
    private bool _initialized;
    private CoherenceSync _sync;
    private List<Rigidbody> _rigidbodies = new();
    private List<RigidbodySync> _rigidbodySyncs = new();

    private float _reconciliationRate = 1f;

    private List<Impact> _impacts = new(); 

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
        _initialized = true;
    }

    [Command]
    public void SendImpulse(int rigidbodyIndex, Vector3 localPos, Vector3 worldImpulse, int numFrames)
    {
        Debug.Log($"Adding impact");
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
            var otherGroupSync = Player.other.GetComponent<RigidbodyGroupSync>();
            otherGroupSync._sync.SendCommand<RigidbodyGroupSync>(nameof(SendImpulse),
                MessageTarget.All,
                UnityEngine.Random.Range(0, _rigidbodies.Count),
                UnityEngine.Random.insideUnitSphere, 
                UnityEngine.Random.onUnitSphere * impactScale, 
                10);
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
            
            if (_sync.HasStateAuthority)
                rb.AddForceAtPosition(impact.worldImpulse/impact.numFrames, worldPos);
            
            impact.curFrame++;

            if (impact.curFrame <= impact.numFrames)
                _reconciliationRate = Mathf.Min(0f, _reconciliationRate);
            else
            {
                float rate = (float)(impact.curFrame - impact.numFrames) / impact.numFrames;
                _reconciliationRate = Mathf.Min(rate, _reconciliationRate);
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
