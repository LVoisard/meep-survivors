using UnityEngine;

public class DrHouseBoss : Enemy
{
    private Squish squish;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private int spawnOnStompCount;

    private GameObject player;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        squish = GetComponentInChildren<Squish>();
        squish.onSquishStateChanged.AddListener(StompListen);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void StompListen(Squish.SquishState state)
    {
        if (state == Squish.SquishState.SecondSmush)
        {
            Stomp();
        }
    }

    void Stomp()
    {
        for (int i = 0; i < spawnOnStompCount; i++)
        {
            Enemy e = Instantiate(enemyPrefab);
            e.transform.position = transform.position;
        }
    }

    private void FixedUpdate()
    {
        if (!rooted)
        {
            rigidbody2D.linearVelocity = Vector2.Lerp(rigidbody2D.linearVelocity, (player.transform.position - transform.position).normalized * speed, Time.fixedDeltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            squish.RequestBounce();
        }
    }
}
