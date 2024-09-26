using System;
using System.Collections;
using System.Collections.Generic;
using Coherence.Toolkit;
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

    public GameObject GetPart(int index) => _parts[index];

    private Transform _inactiveRoot;
    
    private void Awake()
    {
        _coherenceSync = GetComponent<CoherenceSync>();
        if (_coherenceSync.HasStateAuthority)
        {
            int numOptions = PrefabRepository.GetPrefabCount();
            leftArmItemID = Random.Range(0, numOptions);
            rightArmItemID = Random.Range(0, numOptions);
            leftWeaponItemID = Random.Range(0, numOptions);
            rightWeaponItemID= Random.Range(0, numOptions);
        }
        else
        {
            GameObject inactiveParent = new GameObject("Inactive Parent");
            inactiveParent.transform.SetParent(transform);
            inactiveParent.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            inactiveParent.transform.localScale = Vector3.one;
            inactiveParent.SetActive(false);
            _inactiveRoot = inactiveParent.transform;
        }
        
        InstantiateDirectlyInHierarchy();
    }

    private void InstantiateDirectlyInHierarchy()
    {
        _parts.Add(Instantiate(PrefabRepository.GetPrefab(leftArmItemID), transform.position + Vector3.left * 3f, Quaternion.identity, transform));
        _parts.Add(Instantiate(PrefabRepository.GetPrefab(rightArmItemID), transform.position + Vector3.right * 3f, Quaternion.identity, transform));
        _parts.Add(Instantiate(PrefabRepository.GetPrefab(leftWeaponItemID), _parts[0].transform.position + Vector3.up * 2f, Quaternion.identity, _parts[0].transform));
        _parts.Add(Instantiate(PrefabRepository.GetPrefab(rightWeaponItemID), _parts[1].transform.position + Vector3.up * 2f, Quaternion.identity, _parts[1].transform));
        
        foreach (var part in _parts)
        {
            var playerPart = part.GetComponent<PlayerPart>();
            playerPart.partIndex = _parts.IndexOf(part);

            // When we have authority we should enable sync right now, otherwise the instantiator will enable it upon hooking it up
            if (_coherenceSync.HasStateAuthority)
                playerPart.GetComponent<CoherenceSync>().enabled = true;
        }
    }

    private void Update()
    {
        if (_coherenceSync.HasStateAuthority)
        {
            if (Input.GetKey(KeyCode.A))
                transform.position += -transform.right * Time.deltaTime;
            if (Input.GetKey(KeyCode.D))
                transform.position += transform.right * Time.deltaTime;
            if (Input.GetKey(KeyCode.W))
                transform.position += transform.forward * Time.deltaTime;
            if (Input.GetKey(KeyCode.S))
                transform.position += -transform.forward * Time.deltaTime;
        }
    }
}
