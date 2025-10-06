using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    static int EnemiesDead = 0;
    static int EnemiesBetweenMeeps = 20;

    PlayerMovement playerEntity;
    Rigidbody2D rigidbody2D;
    [SerializeField] protected float speed = 5;
    [SerializeField] protected float bodydamage = 1f;
    public bool rooted = false;
    private bool isMiniboss;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        playerEntity = GameObject.FindFirstObjectByType<PlayerMovement>();
        if (playerEntity == null)
        {
            Debug.LogError("Could not find a player");
        }

        GetComponent<Health>().onDied.AddListener(OnEnemyDead);
    }

    public void Root(float duration)
    {
        rooted = true;
        rigidbody2D.linearVelocity = Vector2.zero;
        Helper.Wait(duration, () => rooted = false);
    }

    private void FixedUpdate()
    {
        if (!rooted)
        {
            rigidbody2D.linearVelocity = Vector2.Lerp(rigidbody2D.linearVelocity, (playerEntity.transform.position - transform.position).normalized * speed, Time.fixedDeltaTime);
        }
    }

    public void SetAsMiniboss()
    {
        isMiniboss = true;
        transform.localScale = 2 * Vector2.one;
    }

    private void OnEnemyDead()
    {
        if (isMiniboss)
        {
            Instantiate(DataManager.Instance.LootboxPrefab, transform.position, DataManager.Instance.LootboxPrefab.transform.rotation);
            return;
        }

        EnemiesDead++;

        if (EnemiesDead % EnemiesBetweenMeeps == 0)
        {
            Instantiate(DataManager.Instance.MeepPickupPrefab, transform.position, DataManager.Instance.MeepPickupPrefab.transform.rotation);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.collider.GetComponent<Health>().TakeDamage(bodydamage);
        }
    }
}
