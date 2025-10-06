using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Skills", menuName = "Skill/Buff")]
public class Buff : Skill
{
    [SerializeField] private float boostAmount = 1f;
    [SerializeField] private float cooldown = 1f;

    public override void Perform()
    {
        ready = false;
        BuffTargets();
        Helper.Wait(cooldown / (1f + (effectors.CooldownReduction / 100f)), () => ready = true);
    }

    private void BuffTargets()
    {
        var targets = FindTargets<BaseMeep>(owner.transform.position);
        foreach (var targ in targets)
        {
            //get effectors, upgrade them.
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
