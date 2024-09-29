using Coherence.Toolkit;
using UnityEngine;

public class PlayerOrPartInstantiator : INetworkObjectInstantiator
{
    public ICoherenceSync Instantiate(SpawnInfo spawnInfo)
    {
        // Is this a request to spawn a root player?
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
        {
            // For some reason when Rigidbody interpolation mode is Manual, the spawInfo.connectedEntity is zero.
            // Is this a bug?
            Debug.LogError($"Connected Entity not found for ID {spawnInfo.connectedEntity.Index}");
            return null;
        }

        // Locate the player 
        Player player = connectedEntityGO.GetComponentInParent<Player>();

        // Get the part index of the instantiated part ...
        var partIndex = spawnInfo.GetBindingValue<int>("partIndex");
        Debug.Log($"Returning existing part with index {partIndex}");

        // Retrieve the pre-existing part from the player
        var part = player.GetPart(partIndex).GetComponent<CoherenceSync>();
        
        // enable its CoherenceSync now (so it doesn't try to initialize before we get the network command to instantiate it)
        part.enabled = true;
        
        // Return it as the new 'instance'
        return part;
    }

    public void Destroy(ICoherenceSync obj)
    {
        if (obj is Component c)
            Object.Destroy(c.gameObject);
    }

    public void WarmUpInstantiator(CoherenceBridge bridge, CoherenceSyncConfig config, INetworkObjectProvider assetLoader) { }
    public void OnUniqueObjectReplaced(ICoherenceSync instance) { }
    public void OnApplicationQuit() { }
}
