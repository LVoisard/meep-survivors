using UnityEngine;

[RequireComponent(typeof(Sprite))]
[RequireComponent(typeof(Rigidbody2D))]
public class Squish : MonoBehaviour
{
    private Rigidbody2D Rigidbody;
    private SquishState State;

    [SerializeField] float StretchAmount = 1f;
    [SerializeField] float SmushAmount = 1f;
    [SerializeField] float StretchTime = 1f;
    [SerializeField] float SmushTime = 1f;
    [SerializeField] float NeutralTime = 1f;
    [SerializeField] float VerticalJumpHeight = 1f;
    [SerializeField] float VerticalJumpSpeed = 0.3f;
    [SerializeField] float DirectionalRotationDegrees = 15;

    bool isAnimationPlaying = false;
    float timeInAnimation = 0f;

    enum SquishState
    { 
        Immobile,
        Neutral,
        Squish,
        Smush
    }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = Rigidbody.linearVelocity;

        transform.localScale = Vector3.one;

        //Movement
        if (velocity.sqrMagnitude > 0.1f)
        {
            if (!isAnimationPlaying)
            {
                isAnimationPlaying = true;
                State = SquishState.Squish;
            }


            float rotation = Mathf.Lerp(transform.rotation.eulerAngles.z, Quaternion.FromToRotation(Vector3.right, velocity).eulerAngles.z, 0.01f);
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotation);
        }

        if (isAnimationPlaying)
        {
            timeInAnimation += Time.deltaTime;

            float z = 0f;
            switch (State)
            {
                case SquishState.Immobile:
                    isAnimationPlaying = false;
                    timeInAnimation = 0f;
                    z = 0f;
                    break;
                case SquishState.Neutral:
                    if (timeInAnimation > StretchTime + SmushTime)
                        State = SquishState.Immobile;
                    break;
                case SquishState.Squish:
                    if (timeInAnimation > StretchTime)
                        State = SquishState.Smush;

                    z = Mathf.Lerp(transform.position.z, VerticalJumpHeight, VerticalJumpSpeed);
                    break;
                case SquishState.Smush:
                    if (timeInAnimation > StretchTime + SmushTime)
                        State = SquishState.Neutral;

                    z = Mathf.Lerp(transform.position.z, 0f, VerticalJumpSpeed);
                    break;
            }

            transform.position = new Vector3(transform.position.x, transform.position.y, z);
        }

        float scaleX, scaleY = 1f;
        switch (State)
        {
            case SquishState.Squish:
                scaleY = 1f + velocity.magnitude * StretchAmount / 10;
                scaleX = 1f / scaleY;
                break;
            case SquishState.Smush:
                scaleX = 1f + velocity.magnitude * SmushAmount / 10;
                scaleY = 1f / scaleX;
                break;
            case SquishState.Immobile:
            case SquishState.Neutral:
            default:
                scaleX = 1f;
                scaleY = 1f;
                break;
        }

        transform.localScale = new Vector3(scaleX, scaleY, 1);
    }
}
