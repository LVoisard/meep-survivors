using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Skills", menuName = "Skill/Heal")]
public class Heal : Skill
{
    [SerializeField] private float healAmount = 1f;
    [SerializeField] private float cooldown = 1f;

    public override void Perform()
    {
        ready = false;
        HealTargets();
        Helper.Wait(cooldown / (1f + (effectors.CooldownReduction / 100f)), () => ready = true);
    }

    private void HealTargets()
    {
        var targets = FindTargets<BaseMeep>(owner.transform.position);
        foreach (var targ in targets)
        {
            // TODO ADD VFX
            targ.GetComponent<Health>().Heal(healAmount * (100f + effectors.Damage) / 100f);
        }
    }

    protected override Transform[] FindTargets<T>(Vector3 pos)
    {
        return FindObjectsByType<T>(FindObjectsSortMode.None)
            .Select(x => x.transform)
            .OrderBy(x => Vector3.Distance(pos, x.position))
            .Take(targetCount + effectors.AdditionalTargets)
            .ToArray();
    }
}
