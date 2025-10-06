using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject nextStageSpawn;

    void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.position = nextStageSpawn.transform.position;
    }
}
