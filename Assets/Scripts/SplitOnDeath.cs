using UnityEngine;

public class SplitOnDeath : MonoBehaviour
{
    [SerializeField] private GameObject splitPrefab;
    [SerializeField] private int splitCount = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Health>().onDied.AddListener(Split);
    }

    void Split()
    {
        for (int i = 0; i < splitCount; i++)
        {
            var go = Instantiate(splitPrefab);
            var offset = Random.insideUnitCircle.normalized;
            go.transform.position = transform.position + new Vector3(offset.x, offset.y, 0);
        }

    }


}
