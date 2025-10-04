using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MeepFollow : MonoBehaviour
{
    [Header("Follow Settings")]
    public float followSpeed = 10f;
    public float followDistance = 0.5f;
    public float timeBetweenHops = 0.4f;

    private Transform target;
    private Rigidbody2D rb;
    private Squish ParentSquish;

    void Awake()
    {
        target = transform.parent;
        rb = GetComponent<Rigidbody2D>();
        ParentSquish = GetComponentInParent<PlayerMovement>().GetComponentInChildren<Squish>();
        ParentSquish.onSquishStateChanged.AddListener(Bounce);
    }

    void FixedUpdate()
    {
        if (target == null) return;

        Vector2 direction = (Vector2)target.position - rb.position;
        float distance = direction.magnitude;

        if (distance > followDistance)
        {
            Vector2 targetPosition = (Vector2)target.position - direction.normalized * followDistance * (transform.GetSiblingIndex() + 1);
            Vector2 newPosition = Vector2.Lerp(rb.position, targetPosition, Time.fixedDeltaTime * followSpeed);
            rb.MovePosition(newPosition);

            //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //rb.MoveRotation(Mathf.LerpAngle(rb.rotation, angle, Time.fixedDeltaTime * followSpeed));
        }
    }

    void Bounce(Squish.SquishState state)
    {
        if (state == Squish.SquishState.Squish)
        {
            Invoke("RequestBounce", transform.GetSiblingIndex() * timeBetweenHops);
        }
    }

    void RequestBounce()
    {
        GetComponentInChildren<Squish>().RequestBounce();
    }
}
