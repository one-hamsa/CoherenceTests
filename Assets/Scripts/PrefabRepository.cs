using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TEST/Prefab Repository")]
public class PrefabRepository : ScriptableObject
{
    public GameObject playerPrefab;
    public List<GameObject> prefabs;

    private static bool GetRepo(out PrefabRepository repo)
    {
        repo = Resources.Load<PrefabRepository>("Prefab Repo");
        return repo != null;
    }
    
    public static GameObject GetPlayerPrefab()
    {
        if (GetRepo(out var repo))
            return repo.playerPrefab;

        return null;
    }
    
    public static GameObject GetPrefab(int index)
    {
        if (GetRepo(out var repo))
        {
            if (index >= 0 && index < repo.prefabs.Count)
                return repo.prefabs[index];
        }
        
        return null;
    }

    public static int GetPrefabCount()
    {
        if (GetRepo(out var repo))
            return repo.prefabs.Count;
        
        return 0;
    }
}
