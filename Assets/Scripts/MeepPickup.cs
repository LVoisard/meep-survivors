using UnityEngine;

public class MeepPickup : MonoBehaviour
{
    [SerializeField] SpriteRenderer MeepSprite;

    [SerializeField] BaseMeep.MeepType MeepType;

    private void Start()
    {
        MeepType = GetRandomEnumValue<BaseMeep.MeepType>();
        MeepSprite.sprite = DataManager.Instance.MeepSpriteMap[(int)MeepType];
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            OnPickup();
            Destroy(gameObject);
        }
    }

    void OnPickup()
    {
        Transform train = GameObject.FindGameObjectWithTag("PlayerTrain").transform;
        GameObject newMeep = Instantiate(DataManager.Instance.MeepPrefab, train);

        newMeep.transform.localPosition = Vector3.zero;
        newMeep.transform.localScale = Vector3.one;
        newMeep.transform.localRotation = Quaternion.identity;

        BaseMeep baseMeep = newMeep.GetComponent<BaseMeep>();

        baseMeep.SetType(MeepType);
        baseMeep.SetLevel(1);

        TrainManager trainManager = train.GetComponent<TrainManager>();
        trainManager.OnMeepAdded(baseMeep);
    }

    T GetRandomEnumValue<T>() where T : System.Enum
    {
        var values = System.Enum.GetValues(typeof(T));
        return (T)values.GetValue(Random.Range(0, values.Length));
    }
}
