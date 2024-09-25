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
    public GameObject _leftArm;
    [NonSerialized]
    public GameObject _rightArm;
    [NonSerialized]
    public GameObject _leftWeapon;
    [NonSerialized]
    public GameObject _rightWeapon;
    
    private CoherenceSync _coherenceSync;
    
    private void Awake()
    {
        _coherenceSync = GetComponent<CoherenceSync>();

    }

    private IEnumerator Start()
    {
        yield return null;
        yield return null;
        yield return null;
        
        if (_coherenceSync.HasStateAuthority)
        {
            int numOptions = PrefabRepository.GetPrefabCount();
            leftArmItemID = Random.Range(0, numOptions);
            rightArmItemID = Random.Range(0, numOptions);
            leftWeaponItemID = Random.Range(0, numOptions);
            rightWeaponItemID= Random.Range(0, numOptions);
            
            _leftArm = Instantiate(PrefabRepository.GetPrefab(leftArmItemID), transform.position + Vector3.left * 3f, Quaternion.identity);
            _leftArm.transform.SetParent(transform);
            yield return null;
            yield return null;
            _rightArm = Instantiate(PrefabRepository.GetPrefab(rightArmItemID), transform.position + Vector3.right * 3f, Quaternion.identity);
            _rightArm.transform.SetParent(transform);
            yield return null;
            yield return null;
            _leftWeapon  = Instantiate(PrefabRepository.GetPrefab(leftWeaponItemID), _leftArm.transform.position + Vector3.up * 2f, Quaternion.identity);
            _leftWeapon.transform.SetParent(_leftArm.transform);
            yield return null;
            yield return null;
            _rightWeapon  = Instantiate(PrefabRepository.GetPrefab(rightWeaponItemID), _rightArm.transform.position + Vector3.up * 2f, Quaternion.identity);
            _rightWeapon.transform.SetParent(_rightArm.transform);
        }
    }

    private void Update()
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
