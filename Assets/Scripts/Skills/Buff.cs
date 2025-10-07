using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Skills", menuName = "Skill/Buff")]
public class Buff : Skill
{
    [SerializeField] private float boostAmount = 1f;
    [SerializeField] private float cooldown = 3f;
    [SerializeField] private float duration = 1.5f;

    public override void Perform()
    {
        ready = false;
        BuffTargets();
        Helper.Wait(cooldown / (1f + (effectors.CooldownReduction / 100f)), () => ready = true);
    }

    private void BuffTargets()
    {
        var targets = FindTargets<MeepAttack>(owner.transform.position);
        var effect = boostAmount * effectors.Damage;
        foreach (var targ in targets)
        {
            targ.GetComponent<DamageEffector>().AddToVal(effect);
            targ.GetComponent<AreaOfEffectEffector>().AddToVal(effect);
            targ.GetComponent<CooldownEffector>().AddToVal(effect);
            targ.GetComponent<SpeedEffector>().AddToVal(effect);
            targ.GetComponent<DurationEffector>().AddToVal(effect);
        }

        Helper.Wait(duration / (1f + (effectors.Duration / 100f)), () =>
            {
                foreach (var targ in targets)
                {
                    if (targ == null) continue;
                    targ.GetComponent<DamageEffector>().AddToVal(-effect);
                    targ.GetComponent<AreaOfEffectEffector>().AddToVal(-effect);
                    targ.GetComponent<CooldownEffector>().AddToVal(-effect);
                    targ.GetComponent<SpeedEffector>().AddToVal(-effect);
                    targ.GetComponent<DurationEffector>().AddToVal(-effect);
                }
            });
    }

    protected override Transform[] FindTargets<T>(Vector3 pos)
    {
        return FindObjectsByType<T>(FindObjectsSortMode.None)
            .Select(x => x.transform)
            .Distinct()
            .Where(x => x.transform != owner.transform && owner.tag != "Enemy")
            .OrderBy(x => Vector3.Distance(pos, x.position))
            .Take(targetCount + effectors.AdditionalTargets)
            .ToArray();
    }
}
