using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Skills", menuName = "Skill/FireProjectile")]
public class FireProjectileSkill : Skill
{
    [SerializeField] private BasicBullet projectile;
    [SerializeField] private float cooldown;
    [SerializeField] private float dmg;

    private BasicBullet projCopy;
    void Awake()
    {
        projCopy = projectile;
    }

    public void AddChainToAmmo()
    {
        projCopy.AddComponent<Chain>();
    }

    public void AddPierceToAmmo()
    {
        projCopy.AddComponent<Chain>();
    }

    public void AddExplodeToAmmo()
    {
        projCopy.AddComponent<ExplodeOnContact>();
    }

    public override void Perform()
    {
        var pos = owner.transform.position;
        foreach (var target in FindTargets<Enemy>(pos))
        {
            Vector3 ab = target.position - pos;
            Vector3 dir = ab.normalized;
            BasicBullet projInst = Instantiate(projCopy);
            projInst.transform.position = pos + dir;
            projInst.transform.forward = dir;
            projInst.SetStats("Enemy", dmg, effectors);
        }

        ready = false;
        Helper.Wait(cooldown / (1f + (effectors.CooldownReduction / 100f)), () => ready = true);
    }
}