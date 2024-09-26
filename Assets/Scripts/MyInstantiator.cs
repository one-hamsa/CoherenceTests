using Coherence.Toolkit;
using UnityEngine;

public class MyInstantiator : INetworkObjectInstantiator
{
    public void OnUniqueObjectReplaced(ICoherenceSync instance)
    {
    }

    public ICoherenceSync Instantiate(SpawnInfo spawnInfo)
    {
        if ((spawnInfo.prefab as MonoBehaviour)?.gameObject == PrefabRepository.GetPlayerPrefab())
        {
            // We can instantiate the root player normally
            return Object.Instantiate(PrefabRepository.GetPlayerPrefab()).GetComponent<ICoherenceSync>();
        }
        
        // Detect instantiate inside a player
        var connectedEntityGO = spawnInfo.bridge.EntityIdToGameObject(spawnInfo.connectedEntity);
        if (connectedEntityGO != null)
            Debug.Log($"Connected Entity is: {connectedEntityGO.name}");
        else
            Debug.Log($"Connected Entity not found for ID {spawnInfo.connectedEntity.Index}");

        Player player = connectedEntityGO.GetComponentInParent<Player>();
        var partIndex = spawnInfo.GetBindingValue<int>("partIndex");

        Debug.Log($"Returning existing part with index {partIndex}");

        var part = player.GetPart(partIndex).GetComponent<CoherenceSync>();
        
        // We had to load disabled, so it doesn't try to initialize before we get the network command to instantiate it
        part.enabled = true;
        
        return part;
    }

    public void WarmUpInstantiator(CoherenceBridge bridge, CoherenceSyncConfig config, INetworkObjectProvider assetLoader)
    {
    }

    public void Destroy(ICoherenceSync obj)
    {
        if (obj is Component c)
            Object.Destroy(c.gameObject);
    }

    public void OnApplicationQuit()
    {
    }
}
