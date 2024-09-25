using Coherence.Toolkit;
using UnityEngine;

public class NestedInstantiateTest : MonoBehaviour
{
    public GameObject subPrefab;
    public Transform subPrefabRoot;

    private void Awake()
    {
        Instantiate(subPrefab, subPrefabRoot);
    }
}
