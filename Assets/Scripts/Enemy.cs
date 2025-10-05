using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    PlayerMovement playerEntity;
    Rigidbody2D rigidbody2D;
    [SerializeField] private float speed = 5;
    public bool rooted = false;



    private void Start()
    {
        playerEntity = GameObject.FindFirstObjectByType<PlayerMovement>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        if (playerEntity == null)
        {
            Debug.LogError("Could not find a player");
        }
    }

    public void Root(float duration)
    {
        rooted = true;
        rigidbody2D.linearVelocity = Vector3.zero;

        Helper.Wait(duration, () => rooted = false);
    }

    private void FixedUpdate()
    {
        if (!rooted)
            rigidbody2D.linearVelocity = Vector2.Lerp(rigidbody2D.linearVelocity, (playerEntity.transform.position - transform.position).normalized * speed, Time.fixedDeltaTime);
    }
}
