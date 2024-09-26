using System;
using UnityEngine;

public class PlayerPart : MonoBehaviour
{
    [NonSerialized]
    public int partIndex = -1;

    private void Awake()
    {
        Debug.Log($"partIndex = {partIndex} and parent name is {(transform.parent != null ? transform.parent.name : "none")}");
    }
}
