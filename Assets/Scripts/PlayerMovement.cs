using NUnit.Framework;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float MoveSpeed = 5f;

    private Rigidbody2D Rigidbody;
    private Squish Squish;
    private Vector2 MoveInput;

    private Vector3 PreviousVelocity;
    private bool isEnabled = true;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Squish = GetComponentInChildren<Squish>();
        Squish.onSquishStateChanged.AddListener(Test);
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        MoveInput = new Vector2(moveX, moveY).normalized;
        Debug.Log($"{moveX}, {moveY}");
        Debug.Log(Input.GetKeyDown(KeyCode.A));
    }

    void FixedUpdate()
    {
        if (isEnabled)
        {
            Rigidbody.linearVelocity = MoveInput * MoveSpeed;
        }
        else
        {
            Rigidbody.linearVelocity = Vector3.zero;
        }
    }

    void Test(Squish.SquishState state)
    {
        if (state == Squish.SquishState.Neutral)
        {
            PreviousVelocity = Rigidbody.linearVelocity;
            isEnabled = false;
        }
        else if (state == Squish.SquishState.Immobile)
        {
            isEnabled = true;
        }
    }
}
