using UnityEngine;
using System.Linq;


public abstract class Skill : ScriptableObject
{
    [SerializeField] protected int targetCount = 1;
    [SerializeField] protected bool ready = true;

    protected SkillEffectors effectors;
    protected GameObject owner;

    public virtual void SetOwner(GameObject go)
    {
        owner = go;
    }
    public abstract void Perform();
    public virtual void ApplyEffectors(SkillEffectors effectors)
    {
        this.effectors = effectors;
    }
    public bool Ready()
    {
        return ready;
    }

    protected virtual Transform[] FindTargets(Vector3 pos)
    {
        return FindObjectsByType<Enemy>(FindObjectsSortMode.None)
            .Select(x => x.transform)
            .OrderBy(x => Vector3.Distance(pos, x.position))
            .Take(targetCount + effectors.AdditionalTargets)
            .ToArray();
    }

    public struct SkillEffectors // in %
    {
        public float AreaOfEffect; // in 100%
        public float Damage; // in 50%
        public float CooldownReduction; // in %
        public float Duration; // in %
        public float Speed; // in %
        public int AdditionalTargets; // in 2#
    }
}