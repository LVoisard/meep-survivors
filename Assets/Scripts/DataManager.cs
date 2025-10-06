using UnityEngine;
using UnityEngine.SceneManagement;

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

        FindFirstObjectByType<PlayerManager>().GetComponent<Health>().onDied.AddListener(() =>
        {
            Reset();
            SceneManager.LoadScene("MainMenu");
        });
    }

    public GameObject LootboxPrefab;
    public GameObject MeepPickupPrefab;
    public GameObject MeepPrefab;

    public Sprite[] MeepSpriteMap;

    public Skill[] Skills;

    public Sprite[] LootboxDropUI;
    public string[] LootboxDropTitles;

    private void Reset()
    {
        Instance = null;
        Destroy(gameObject);
    }
}
