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

    private void Awake()
    {
        _coherenceSync = GetComponent<CoherenceSync>();
        
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
        InstantiateDirectlyInHierarchy();
        
        // Randomize the player's initial pos/rot
        transform.position = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
        transform.localEulerAngles = new Vector3(0f, Random.Range(-30f, 30f), 0f);
    }

    private void InstantiateDirectlyInHierarchy()
    {
        _parts.Add(Instantiate(PrefabRepository.GetPrefab(leftArmItemID), transform.position + Vector3.left * 3f, Quaternion.identity, transform));
        _parts.Add(Instantiate(PrefabRepository.GetPrefab(rightArmItemID), transform.position + Vector3.right * 3f, Quaternion.identity, transform));
        _parts.Add(Instantiate(PrefabRepository.GetPrefab(leftWeaponItemID), _parts[0].transform.position + Vector3.up * 2f, Quaternion.identity, _parts[0].transform));
        _parts.Add(Instantiate(PrefabRepository.GetPrefab(rightWeaponItemID), _parts[1].transform.position + Vector3.up * 2f, Quaternion.identity, _parts[1].transform));
        
        foreach (var part in _parts)
        {
            // for each of the created parts, assign their location on the player (the part-index)
            var playerPart = part.GetComponent<PlayerPart>();
            playerPart.partIndex = _parts.IndexOf(part);

            // When we have authority we can (and should) enable sync right now
            // For remote players, we should wait until we receive the instantiate command from the server and only then enable it (inside PlayerOrPartInstantiator)
            if (_coherenceSync.HasStateAuthority)
                playerPart.GetComponent<CoherenceSync>().enabled = true;
        }
    }

    private void Update()
    {
        // Simple user input move code
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
