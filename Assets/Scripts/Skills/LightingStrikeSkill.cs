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
        var targets = FindTargets(owner.transform.position);
        foreach (var targ in targets)
        {
            // TODO ADD VFX
            targ.GetComponent<Health>().TakeDamage(damage * (100f + effectors.Damage) / 100f);
        }
    }

    protected override Transform[] FindTargets(Vector3 pos)
    {
        var enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None)
            .Select(x => x.transform).ToList();

        int[] indices = new int[this.targetCount + effectors.AdditionalTargets];
        for (int i = 0; i < this.targetCount + effectors.AdditionalTargets; i++)
        {
            int trial = Random.Range(0, enemies.Count());
            if (!indices.Contains(trial))
            {
                indices[i] = trial;
            }
            else
            {
                i--;
            }
        }

        Transform[] targs = new Transform[this.targetCount + effectors.AdditionalTargets];
        for (int i = 0; i < this.targetCount; i++)
        {
            targs[i] = enemies[indices[i]];
        }

        return targs;
    }
}