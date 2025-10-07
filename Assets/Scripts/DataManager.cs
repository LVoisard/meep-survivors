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
            Enemy.EnemiesDead = 0;
            Enemy.EnemiesBetweenMeeps = 20;
            SceneManager.LoadScene("MainMenu");
        });
    }

    void Start()
    {
        stages[2].levelFinished.AddListener(() =>
        {
            Reset();
            Enemy.EnemiesDead = 0;
            Enemy.EnemiesBetweenMeeps = 20;
            SceneManager.LoadScene("WinScene");
        });
    }

    public GameObject LootboxPrefab;
    public GameObject MeepPickupPrefab;
    public GameObject MeepPrefab;

    public Sprite[] MeepSpriteMap;
    public Sprite[] UpgradedMeepSpriteMap;
    public Sprite[] MegaUpgradedMeepSpriteMap;

    public Skill[] Skills;

    public Sprite[] LootboxDropUI;
    public string[] LootboxDropTitles;

    private int currentStage = 0;
    [SerializeField] private Level[] stages;

    private void Reset()
    {
        Instance = null;
        Destroy(gameObject);
    }
}
