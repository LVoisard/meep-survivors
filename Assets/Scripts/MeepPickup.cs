using UnityEngine;

public class MeepPickup : MonoBehaviour
{
    [SerializeField] GameObject MeepPrefab;
    [SerializeField] BaseMeep.MeepType MeepType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            OnPickup();
        }
    }

    void OnPickup()
    {
        Transform train = GameObject.FindGameObjectWithTag("PlayerTrain").transform;
        GameObject newMeep = Instantiate(MeepPrefab, train);

        newMeep.transform.localPosition = Vector3.zero;
        newMeep.transform.localScale = Vector3.one;
        newMeep.transform.localRotation = Quaternion.identity;

        BaseMeep baseMeep = newMeep.GetComponent<BaseMeep>();

        baseMeep.SetType(MeepType);
        baseMeep.SetLevel(1);

        TrainManager trainManager = train.GetComponent<TrainManager>();
        trainManager.OnMeepAdded(baseMeep);

        Destroy(gameObject);
    }
}
