using UnityEngine;

public class DamageOverTime : MonoBehaviour
{
    private float damage;
    private float duration;
    [SerializeField] private float radius;
    public void Setup(float dmg, float duration)
    {
        damage = dmg;
        this.duration = duration;

        Destroy(gameObject, 3);
    }

    private void FixedUpdate()
    {

        Collider2D[] hits = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), radius);

        foreach (var hit in hits)
        {
            if (hit.tag == "Enemy")
            {
                var hp = hit.GetComponent<Health>();
                if (hp != null)
                {
                    hp.TakeDamage(damage / duration * Time.fixedDeltaTime);
                }
            }
        }
    }
}