using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    PlayerMovement playerEntity;
    Rigidbody2D rigidbody2D;
    [SerializeField] private float speed = 5;



    private void Start()
    {
        playerEntity = GameObject.FindFirstObjectByType<PlayerMovement>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        if (playerEntity == null)
        {
            Debug.LogError("Could not find a player");
        }
    }

    private void FixedUpdate()
    {
        rigidbody2D.linearVelocity = Vector2.Lerp(rigidbody2D.linearVelocity, (playerEntity.transform.position - transform.position).normalized * speed, Time.fixedDeltaTime);
    }
}
