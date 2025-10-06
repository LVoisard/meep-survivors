using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    void Awake()
    {
        // If another instance already exists, destroy this one
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Otherwise, set and persist
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public GameObject LootboxPrefab;
    public GameObject MeepPickupPrefab;
    public GameObject MeepPrefab;

    public Sprite[] MeepSpriteMap;

    public Skill[] Skills;
}
