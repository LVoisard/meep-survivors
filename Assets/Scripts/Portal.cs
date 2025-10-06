using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject currentStage;
    [SerializeField] private GameObject nextStageSpawn;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.transform.position = nextStageSpawn.transform.position;
            Destroy(currentStage);
        }
    }
}
