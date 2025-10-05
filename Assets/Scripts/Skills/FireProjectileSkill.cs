using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Skills", menuName = "Skill/FireProjectile")]
public class FireProjectileSkill : Skill
{
    [SerializeField] private BasicBullet projectile;
    [SerializeField] private float cooldown;
    [SerializeField] private float dmg;

    public override void Perform()
    {
        var pos = owner.transform.position;
        foreach (var target in FindTargets<Enemy>(pos))
        {
            Vector3 ab = target.position - pos;
            Vector3 dir = ab.normalized;
            BasicBullet projInst = Instantiate(projectile);
            projInst.transform.position = pos + dir;
            projInst.transform.forward = dir;
            projInst.SetStats("Enemy", dmg, effectors);
        }

        ready = false;
        Helper.Wait(cooldown / (1f + (effectors.CooldownReduction / 100f)), () => ready = true);
    }
}