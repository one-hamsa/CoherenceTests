using System;
using System.Collections;
using System.Collections.Generic;
using Coherence.Toolkit;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public int leftArmItemID;
    public int rightArmItemID;
    public int leftWeaponItemID;
    public int rightWeaponItemID;

    [NonSerialized]
    public List<GameObject> _parts = new();
    
    private CoherenceSync _coherenceSync;
    private Rigidbody _rigidbody;

    public GameObject GetPart(int index) => _parts[index];

    private void Awake()
    {
        _coherenceSync = GetComponent<CoherenceSync>();
        _rigidbody = GetComponentInChildren<Rigidbody>();
        
        if (_coherenceSync.HasStateAuthority)
        {
            // Randomize a fake 'load-out' for the local player
            int numOptions = PrefabRepository.GetPrefabCount();
            leftArmItemID = Random.Range(0, numOptions);
            rightArmItemID = Random.Range(0, numOptions);
            leftWeaponItemID = Random.Range(0, numOptions);
            rightWeaponItemID= Random.Range(0, numOptions);

            Debug.Log($"My clientID = {_coherenceSync.CoherenceBridge.Client.ClientID}");
        }
        
        // Create the player according to load-out
        SetupPlayer();
        
        // Randomize the player's initial pos/rot
        transform.position = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
        transform.localEulerAngles = new Vector3(0f, Random.Range(-30f, 30f), 0f);
    }

    private void SetupPlayer()
    {
        var playerWorldJoint = _rigidbody.AddComponent<ConfigurableJoint>();
        playerWorldJoint.angularXMotion = ConfigurableJointMotion.Locked;
        playerWorldJoint.angularZMotion = ConfigurableJointMotion.Locked;
        playerWorldJoint.rotationDriveMode = RotationDriveMode.Slerp;
        playerWorldJoint.slerpDrive = new JointDrive() { positionDamper = 50f, positionSpring = 0f, maximumForce = float.MaxValue };

        _parts.Add(Instantiate(PrefabRepository.GetPrefab(leftArmItemID), transform.position + Vector3.left * 3f, Quaternion.identity, transform));
        _parts.Add(Instantiate(PrefabRepository.GetPrefab(rightArmItemID), transform.position + Vector3.right * 3f, Quaternion.identity, transform));
        _parts.Add(Instantiate(PrefabRepository.GetPrefab(leftWeaponItemID), _parts[0].transform.position + Vector3.up * 2f, Quaternion.identity, _parts[0].transform));
        _parts.Add(Instantiate(PrefabRepository.GetPrefab(rightWeaponItemID), _parts[1].transform.position + Vector3.up * 2f, Quaternion.identity, _parts[1].transform));
        
        foreach (var part in _parts)
        {
            // for each of the created parts, assign their location on the player (the part-index)
            var playerPart = part.GetComponent<PlayerPart>();
            playerPart.partIndex = _parts.IndexOf(part);

            var partRigidbody = part.GetComponentInChildren<Rigidbody>();
            var parentRigidbody = part.transform.parent.GetComponentInChildren<Rigidbody>();

            var joint = parentRigidbody.AddComponent<ConfigurableJoint>();
            joint.connectedBody = partRigidbody;
            joint.anchor = parentRigidbody.transform.InverseTransformPoint(partRigidbody.transform.position);
            joint.xDrive = new JointDrive() { positionDamper = 2f, positionSpring = 2000f, maximumForce = float.MaxValue };
            joint.yDrive = new JointDrive() { positionDamper = 2f, positionSpring = 2000f, maximumForce = float.MaxValue };
            joint.zDrive = new JointDrive() { positionDamper = 2f, positionSpring = 2000f, maximumForce = float.MaxValue };
            joint.rotationDriveMode = RotationDriveMode.Slerp;
            joint.slerpDrive = new JointDrive() { positionDamper = 2f, positionSpring = 15000f, maximumForce = float.MaxValue };

            // When we have authority we can (and should) enable sync right now
            // For remote players, we should wait until we receive the instantiate command from the server and only then enable it (inside PlayerOrPartInstantiator)
            if (_coherenceSync.HasStateAuthority)
                playerPart.GetComponent<CoherenceSync>().enabled = true;
        }
    }

    private void FixedUpdate()
    {
        float forceAmount = 50f;
        float torqueAmount = 500f;
        // Simple user input move code
        if (_coherenceSync.HasStateAuthority)
        {
            if (Input.GetKey(KeyCode.A))
                _rigidbody.AddRelativeForce(-transform.right * forceAmount, ForceMode.Acceleration);
            if (Input.GetKey(KeyCode.D))
                _rigidbody.AddRelativeForce(transform.right * forceAmount, ForceMode.Acceleration);
            if (Input.GetKey(KeyCode.W))
                _rigidbody.AddRelativeForce(transform.forward * forceAmount, ForceMode.Acceleration);
            if (Input.GetKey(KeyCode.S))
                _rigidbody.AddRelativeForce(-transform.forward * forceAmount, ForceMode.Acceleration);
            if (Input.GetKey(KeyCode.Q))
                _rigidbody.AddTorque(-Vector3.up * torqueAmount, ForceMode.Acceleration);
            if (Input.GetKey(KeyCode.E))
                _rigidbody.AddTorque(Vector3.up * torqueAmount, ForceMode.Acceleration);
        }
    }

    private void OnEnable()
    {
        if (_coherenceSync.HasStateAuthority)
            StartCoroutine(Ping());
    }

    IEnumerator Ping()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log($"Ping = {_coherenceSync.CoherenceBridge.Client.Ping}");
        }
    }
}
