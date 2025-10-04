using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Sprite))]
[RequireComponent(typeof(Rigidbody2D))]
public class Squish : MonoBehaviour
{
    private Rigidbody2D Rigidbody;
    private SquishState State;

    public UnityEvent<SquishState> onSquishStateChanged;

    [SerializeField] float StretchAmount = 1f;
    [SerializeField] float SmushAmount = 1f;
    [SerializeField] float StretchTime = 1f;
    [SerializeField] float SmushTime = 1f;
    [SerializeField] float NeutralTime = 1f;
    [SerializeField] float VerticalJumpHeight = 1f;
    [SerializeField] float VerticalJumpSpeed = 0.3f;
    [SerializeField] float DirectionalRotationDegrees = 15;
    [SerializeField] float DirectionalRotationSpeed = 3;

    bool isAnimationPlaying = false;
    float timeInAnimation = 0f;

    private Quaternion initialRot;

    public enum SquishState
    {
        Immobile,
        Neutral,
        Squish,
        Smush
    }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        initialRot = transform.rotation;
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
                SetState(SquishState.Squish);
            }

            float targetZ = Mathf.Sign(velocity.x) * Mathf.Sin(timeInAnimation) * DirectionalRotationDegrees;
            Quaternion targetRot = Quaternion.Euler(initialRot.x, 0, targetZ);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, DirectionalRotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, initialRot, DirectionalRotationSpeed * Time.deltaTime);
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
                    if (timeInAnimation > StretchTime + SmushTime + NeutralTime)
                        SetState(SquishState.Immobile);
                    break;
                case SquishState.Squish:
                    if (timeInAnimation > StretchTime)
                        SetState(SquishState.Smush);

                    z = Mathf.Lerp(transform.position.z, VerticalJumpHeight, VerticalJumpSpeed * Time.deltaTime);
                    break;
                case SquishState.Smush:
                    if (timeInAnimation > StretchTime + SmushTime)
                        SetState(SquishState.Neutral);

                    z = Mathf.Lerp(transform.position.z, 0f, VerticalJumpSpeed * Time.deltaTime);
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

    private void SetState(SquishState state)
    {
        this.State = state;
        onSquishStateChanged?.Invoke(state);
    }
}
