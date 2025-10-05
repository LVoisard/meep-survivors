using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Skills", menuName = "Skill/LightningStrike")]
public class LightingStikeSkill : Skill
{
    [SerializeField] private float damage = 1f;
    [SerializeField] private float cooldown = 1f;
    public override void Perform()
    {
        ready = false;
        StrikeTargets();
        Helper.Wait(cooldown / (1f + (effectors.CooldownReduction / 100f)), () => ready = true);
    }

    private void StrikeTargets()
    {
        var targets = FindTargets<Enemy>(owner.transform.position);
        foreach (var targ in targets)
        {
            // TODO ADD VFX
            targ.GetComponent<Health>().TakeDamage(damage * (100f + effectors.Damage) / 100f);
        }
    }

    protected override Transform[] FindTargets<T>(Vector3 pos)
    {
        var enemies = FindObjectsByType<T>(FindObjectsSortMode.None)
            .Select(x => x.transform).ToList();

        Transform[] targs = new Transform[this.targetCount + effectors.AdditionalTargets];
        for (int i = 0; i < this.targetCount + effectors.AdditionalTargets; i++)
        {
            targs[i] = enemies[Random.Range(0, enemies.Count())];
        }

        return targs;
    }
}