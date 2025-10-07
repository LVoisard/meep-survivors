using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    public static int EnemiesDead = 0;
    public static int EnemiesBetweenMeeps = 20;

    PlayerMovement playerEntity;
    protected Rigidbody2D rigidbody2D;
    [SerializeField] protected float speed = 5;
    [SerializeField] protected float bodydamage = 1f;
    public bool rooted = false;
    private int meepDropCount;
    private bool isMiniboss = false;
    private bool isBoss = false;

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

    public void SetAsBoss(Level level)
    {
        isBoss = true;
        GetComponent<Health>().onDied.AddListener(level.Complete);
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
            meepDropCount++;
            EnemiesDead = 0;
            EnemiesBetweenMeeps += 10;
            Debug.Log(EnemiesDead);
            Debug.Log(EnemiesBetweenMeeps);
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
