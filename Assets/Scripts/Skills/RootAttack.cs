using UnityEngine;

[CreateAssetMenu(fileName = "Skills", menuName = "Skill/RootAttack")]
public class RootAttack : Skill
{
    [SerializeField] private float duration;
    [SerializeField] private float cooldown;
    [SerializeField] private float damage;
    public override void Perform()
    {
        ready = false;
        RootEnemies();

        Helper.Wait(cooldown / (1f + (effectors.CooldownReduction / 100f)), () => ready = true);
    }

    private void RootEnemies()
    {
        var targets = FindTargets<Enemy>(owner.transform.position);

        foreach (var target in targets)
        {
            // TODO ADD VISUAL
            target.GetComponent<Enemy>().Root(duration * (1f + effectors.Duration / 100f));
            target.GetComponent<Health>().TakeDamage(damage * (1f + effectors.Damage / 100f));
        }
    }
}