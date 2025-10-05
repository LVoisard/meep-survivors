using UnityEngine;
using System.Linq;


public abstract class Skill : ScriptableObject
{
    [SerializeField] private int targetCount = 1;
    [SerializeField] protected bool ready = true;
    protected GameObject owner;

    public virtual void SetOwner(GameObject go)
    {
        owner = go;
    }
    public abstract void Perform();
    public bool Ready()
    {
        return ready;
    }
    protected virtual Vector3[] FindTargets(Vector3 pos)
    {
        return FindObjectsByType<Enemy>(FindObjectsSortMode.None)
            .Select(x => x.transform.position)
            .OrderBy(x => Vector3.Distance(pos, x))
            .Take(targetCount)
            .ToArray();
    }

}