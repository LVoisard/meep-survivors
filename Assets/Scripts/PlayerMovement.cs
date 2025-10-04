using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float MoveSpeed = 5f;

    private Rigidbody2D Rigidbody;
    private Vector2 MoveInput;

    private Vector2 smushLocation;
    private bool isEnabled = true;
    public bool IsEnabled {
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
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        MoveInput = new Vector2(moveX, moveY).normalized;
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
}
