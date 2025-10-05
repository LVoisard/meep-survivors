using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lifetime = 5f;
    private float damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public void SetStats(float dmg)
    {
        damage = dmg;
    }
    void Start()
    {
        Destroy(gameObject, lifetime);
        transform.GetChild(0).transform.rotation = Quaternion.Euler(-50, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = transform.position + transform.forward * speed * Time.fixedDeltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            var hp = collision.GetComponent<Health>();
            if (hp != null)
            {
                hp.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
