using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float MoveSpeed = 5f;

    private Rigidbody2D Rigidbody;
    private Vector2 MoveInput;

    void Start()
    {
        Rigidbody = GetComponentInChildren<Rigidbody2D>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        MoveInput = new Vector2(moveX, moveY).normalized;
    }

    void FixedUpdate()
    {
        Rigidbody.linearVelocity = MoveInput * MoveSpeed;
    }
}
