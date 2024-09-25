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
            // // Trying to remotely instantiate a player, set the loadout
            // Player._useLoadout = true;
            // Player._loadout_leftArmItemID = spawnInfo.GetBindingValue<int>("leftArmItemID");
            // Player._loadout_rightArmItemID = spawnInfo.GetBindingValue<int>("rightArmItemID");
            // Player._loadout_leftWeaponItemID = spawnInfo.GetBindingValue<int>("leftWeaponItemID");
            // Player._loadout_rightWeaponItemID = spawnInfo.GetBindingValue<int>("rightWeaponItemID");
            
            // Now we can instantiate
            return Object.Instantiate(PrefabRepository.GetPlayerPrefab()).GetComponent<ICoherenceSync>();
        }
        
        // Detect instantiate inside a player
        var connectedEntityGO = spawnInfo.bridge.EntityIdToGameObject(spawnInfo.connectedEntity);
        if (connectedEntityGO != null)
            Debug.Log($"Connected Entity is: {connectedEntityGO.name}");
        else
            Debug.Log($"Connected Entity not found for ID {spawnInfo.connectedEntity.Index}");

        var prefabGO = (spawnInfo.prefab as MonoBehaviour)?.gameObject;
        return Object.Instantiate(prefabGO).GetComponent<ICoherenceSync>();
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
