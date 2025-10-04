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

    private Vector2 smushLocation;
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

        }
    }

    void Test(Squish.SquishState state)
    {
        if (state == Squish.SquishState.Neutral)
        {
            isEnabled = true;
        }
        else if (state == Squish.SquishState.Smush)
        {
            isEnabled = false;
        }
    }
}
