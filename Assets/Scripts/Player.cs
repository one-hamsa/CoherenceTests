using System;
using System.Collections;
using System.Collections.Generic;
using Coherence.Toolkit;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public static bool _useLoadout;
    public static int _loadout_leftArmItemID;
    public static int _loadout_rightArmItemID;
    public static int _loadout_leftWeaponItemID;
    public static int _loadout_rightWeaponItemID;

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

        if (_useLoadout)
            return;
                
        if (_coherenceSync.HasStateAuthority && !_useLoadout)
        {
            int numOptions = PrefabRepository.GetPrefabCount();
            leftArmItemID = Random.Range(0, numOptions);
            rightArmItemID = Random.Range(0, numOptions);
            leftWeaponItemID = Random.Range(0, numOptions);
            rightWeaponItemID= Random.Range(0, numOptions);
            _loadout_leftArmItemID = leftArmItemID; 
            _loadout_rightArmItemID = rightArmItemID;
            _loadout_leftWeaponItemID = leftWeaponItemID;
            _loadout_rightWeaponItemID = rightWeaponItemID;
        }

        _leftArm = Instantiate(PrefabRepository.GetPrefab(_loadout_leftArmItemID), transform.position + Vector3.left * 3f, Quaternion.identity, transform);
        _rightArm = Instantiate(PrefabRepository.GetPrefab(_loadout_rightArmItemID), transform.position + Vector3.right * 3f, Quaternion.identity, transform);
        _leftWeapon  = Instantiate(PrefabRepository.GetPrefab(_loadout_leftWeaponItemID), _leftArm.transform.position + Vector3.up * 2f, Quaternion.identity, _leftArm.transform);
        _rightWeapon  = Instantiate(PrefabRepository.GetPrefab(_loadout_rightWeaponItemID), _rightArm.transform.position + Vector3.up * 2f, Quaternion.identity, _rightArm.transform);
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
