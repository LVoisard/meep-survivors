using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Chain : MonoBehaviour
{
    private int chainCount = 2;
    private HashSet<GameObject> chained = new();

    public bool AlreadyChained(GameObject go)
    {
        return chained.Contains(go);
    }

    public void Chained(GameObject go)
    {
        chained.Add(go);
    }

    public bool CanChain()
    {
        return chained.Count < chainCount;
    }

    public Vector3 ChainToNextTarget(GameObject current)
    {
        var enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None)
            .OrderBy(x => Vector3.Distance(x.transform.position, current.transform.position))
            .ToList();


        foreach (var enemy in enemies)
        {
            if (chained.Contains(enemy.gameObject)) continue;
            return enemy.transform.position;
        }

        return Vector3.zero;
    }
}
