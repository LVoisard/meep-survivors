using UnityEngine;

public class DamageOverTime : MonoBehaviour
{
    private float damage;
    private float duration;
    [SerializeField] private float radius;

    Skill.SkillEffectors effectors;
    public void Setup(float dmg, float duration, Skill.SkillEffectors effectors)
    {
        damage = dmg;
        this.duration = duration;

        Destroy(gameObject, 3);
    }

    private void FixedUpdate()
    {

        Collider2D[] hits = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), radius * (100f + effectors.AreaOfEffect) / 100f);

        foreach (var hit in hits)
        {
            if (hit.tag == "Enemy")
            {
                var hp = hit.GetComponent<Health>();
                if (hp != null)
                {
                    hp.TakeDamage((damage * (1f + effectors.Damage / 100f) / (duration * (1f + effectors.Duration / 100f)) * Time.fixedDeltaTime));
                }
            }
        }
    }
}