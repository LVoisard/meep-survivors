using NUnit.Framework;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float MoveSpeed = 5f;

    private Rigidbody2D Rigidbody;
    private Squish Squish;
    private Vector2 MoveInput;

    private Vector2 smushLocation;
    private bool isEnabled = true;
    public bool IsEnabled
    {
        get => isEnabled;
        set
        {
            if (isEnabled == value) return;
            isEnabled = value;

            // Call function when value changes
            OnIsEnabledChanged(isEnabled);
        }
    }

    void Start()
    {
        Rigidbody = GetComponentInChildren<Rigidbody2D>();
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
        if (IsEnabled)
        {
            Rigidbody.linearVelocity = MoveInput * MoveSpeed;
        }
        else
        {

        }
    }

    void OnIsEnabledChanged(bool val)
    {
        if (!val)
            smushLocation = transform.position;
    }

    void Test(Squish.SquishState state)
    {
        if (state == Squish.SquishState.Neutral)
        {
            IsEnabled = true;
        }
        else if (state == Squish.SquishState.Smush)
        {
            IsEnabled = false;
        }
    }
}
