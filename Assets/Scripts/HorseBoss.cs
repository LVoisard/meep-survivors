using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HorseBoss : Enemy
{
    Rigidbody2D bod;
    // Update is called once per frame

    void Awake()
    {
        bod = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        var newX = Mathf.Cos(Time.time);
        var newY = Mathf.Sin(Time.time);
        bod.linearVelocity = Vector2.Lerp(bod.linearVelocity, new Vector2(newX, newY) * 2f, Time.fixedDeltaTime * 5f);
    }
}
