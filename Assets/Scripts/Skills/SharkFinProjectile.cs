using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SharkFinProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifetime = 5f;
    [SerializeField] private float radius = 2f;
    private float damage;
    private float angle;
    private Skill.SkillEffectors effectors;
    private Transform center;

    private string targetTag = "Enemy";

    void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
        Destroy(gameObject, lifetime);
    }

    void FixedUpdate()
    {
        angle += speed * Time.fixedDeltaTime;

        float rad = angle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * radius * (1f + effectors.AreaOfEffect / 100f);
        if (transform == null || center == null) return;
        transform.position = center.position + offset;
    }

    public void SetStats(string tag, float dmg, Skill.SkillEffectors eff)
    {
        this.targetTag = tag;
        damage = dmg;
        effectors = eff;
    }

    public void SetTranformCenter(Transform c)
    {
        center = c;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == targetTag)
        {
            var pierce = GetComponent<Pierce>();
            var chain = GetComponent<Chain>();
            if (pierce != null)
            {
                if (pierce.AlreadyPierced(collision.gameObject)) return;
                pierce.Pierced(collision.gameObject);

            }
            else if (chain != null)
            {
                if (chain.AlreadyChained(collision.gameObject)) return;
                chain.Chained(collision.gameObject);
            }

            var hp = collision.GetComponent<Health>();
            if (hp != null)
            {
                hp.TakeDamage(damage * (100f + effectors.Damage) / 100f);
            }

            var explode = GetComponent<ExplodeOnContact>();
            if (explode != null)
            {
                var pos = transform.position;
                Collider2D[] hits = Physics2D.OverlapCircleAll(new Vector2(pos.x, pos.y), explode.radius * (1f + effectors.AreaOfEffect / 100f));

                foreach (var hit in hits)
                {
                    if (hit.tag == targetTag)
                    {
                        var hp2 = hit.GetComponent<Health>();
                        if (hp2 != null)
                        {
                            hp2.TakeDamage(damage * (100f + effectors.Damage) / 100f);
                        }
                    }
                }
            }

            if (pierce != null)
            {
                if (pierce.CanPierce())
                {
                    return;
                }

            }
            else if (chain != null)
            {
                if (chain.CanChain())
                {
                    var target = chain.ChainToNextTarget(collision.gameObject);
                    Vector3 dir = target - transform.position;
                    transform.forward = dir.normalized;
                    return;
                }
            }
        }
    }
}
