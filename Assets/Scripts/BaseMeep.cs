using UnityEngine;

public class BaseMeep : MonoBehaviour
{

    private Vector3 initialpos;
    private Rigidbody2D Rigidbody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialpos = transform.position;
        Rigidbody = GetComponentInChildren<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        Rigidbody.linearVelocity = new Vector3(Mathf.Sin(Time.time), Mathf.Cos(Time.time)) * 5;
    }

    public enum MeepType
    {
        Melee,
        Ranged,
        Sorcerer
    }
}
