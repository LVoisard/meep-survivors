using UnityEngine;

[CreateAssetMenu(fileName = "Skills", menuName = "Skill/SharkFins")]
public class SharkFins : Skill
{
    [SerializeField] private SharkFinProjectile projectile;
    [SerializeField] private float cooldown;
    [SerializeField] private float dmg;

    public override void Perform()
    {
        var pos = owner.transform.position;
        foreach (var target in FindTargets(pos))
        {
            Vector3 ab = target.position - pos;
            Vector3 dir = ab.normalized;
            SharkFinProjectile projInst = Instantiate(projectile);
            projInst.transform.position = pos + dir;
            projInst.transform.forward = dir;
            projInst.SetStats(dmg, effectors);
            projInst.SetTranformCenter(owner.transform);
        }

        ready = false;
        Helper.Wait(cooldown / (1f + (effectors.CooldownReduction / 100f)), () => ready = true);
    }
}
