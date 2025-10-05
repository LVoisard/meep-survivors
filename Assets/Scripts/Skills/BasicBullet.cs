using Mono.Cecil.Cil;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lifetime = 5f;
    private float damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Skill.SkillEffectors effectors;
    public void SetStats(float dmg, Skill.SkillEffectors eff)
    {
        damage = dmg;
        effectors = eff;
    }
    void Start()
    {
        Destroy(gameObject, lifetime);
        transform.GetChild(0).transform.rotation = Quaternion.Euler(-50, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = transform.position + transform.forward * speed * (100f + effectors.Speed) / 100f * Time.fixedDeltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
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

            Destroy(gameObject);
        }
    }
}
