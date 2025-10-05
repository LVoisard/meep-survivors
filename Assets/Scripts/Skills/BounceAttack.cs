using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Skills", menuName = "Skill/BounceAttack")]
public class BounceAttack : Skill
{
    [SerializeField] private float radius;
    [SerializeField] private float damage;

    [SerializeField] private GameObject particles;

    ParticleSystem partSys;
    public override void SetOwner(GameObject go)
    {
        base.SetOwner(go);
        go.GetComponentInChildren<Squish>().onSquishStateChanged.AddListener(CheckForAttackReady);
        partSys = Instantiate(particles, go.transform).GetComponent<ParticleSystem>();
        partSys.transform.localPosition = Vector3.zero;
    }

    public override void Perform()
    {
        ready = false;
        partSys.Play();
        Helper.Wait(0.1f, DealDamageInRadius);
    }

    public override void ApplyEffectors(SkillEffectors effectors)
    {
        base.ApplyEffectors(effectors);
        partSys.transform.localScale = Vector3.one * (1f + effectors.AreaOfEffect / 100f);
    }


    private void DealDamageInRadius()
    {
        var pos = owner.transform.position;
        Collider2D[] hits = Physics2D.OverlapCircleAll(new Vector2(pos.x, pos.y), radius * (1f + effectors.AreaOfEffect / 100f));

        foreach (var hit in hits)
        {
            if (hit.tag == "Enemy")
            {
                var hp = hit.GetComponent<Health>();
                if (hp != null)
                {
                    hp.TakeDamage(damage * (100f + effectors.Damage) / 100f);
                }
            }
        }
    }


    private void CheckForAttackReady(Squish.SquishState state)
    {
        if (state == Squish.SquishState.SecondSmush)
        {
            ready = true;
        }
    }
}
