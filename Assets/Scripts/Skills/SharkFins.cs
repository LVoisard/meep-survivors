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
        for (int i = 0; i < targetCount + effectors.AdditionalTargets; i++)
        {
            int ind = i;
            Helper.Wait(0.1f * ind, () =>
            {
                Vector3 dir = Vector3.up;
                SharkFinProjectile projInst = Instantiate(projectile);
                projInst.transform.position = pos + dir;
                projInst.transform.forward = dir;
                projInst.SetStats(dmg, effectors);
                projInst.SetTranformCenter(owner.transform);
            });

        }

        ready = false;
        Helper.Wait(cooldown / (1f + (effectors.CooldownReduction / 100f)), () => ready = true);
    }
}
