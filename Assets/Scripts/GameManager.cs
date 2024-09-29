using Coherence.Toolkit;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        if (CoherenceBridgeStore.TryGetBridge(gameObject.scene, out var bridge))
        {
            bridge.onLiveQuerySynced.AddListener(CreateMyPlayer);
        }
    }

    private void CreateMyPlayer(CoherenceBridge bridge)
    {
        Instantiate(PrefabRepository.GetPlayerPrefab());
    }
}
