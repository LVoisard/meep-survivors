using System;
using System.Collections;
using UnityEngine;
public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private SpriteRenderer arenaSprite;
    [SerializeField] private Enemy[] enemyPrefabs;
    [SerializeField] private Enemy bossPrefab;
    [SerializeField] private GameObject enemyContainer;

    [SerializeField] private float minibossCooldown;
    [SerializeField] private float spawnCooldown;
    [SerializeField] private int spawnAmount;
    [SerializeField] private int bossCountdown = 180;
    [SerializeField] private AudioSource bossAudio;

    [SerializeField] private TMPro.TextMeshProUGUI countDownText;

    private bool spawnCooldownReady = true;
    private bool miniBossSpawnCooldownReady = true;

    float stageDuration = 0;

    Level lvl;


    void Start()
    {
        foreach (AudioSource audio in bossAudio.transform.parent.GetComponentsInChildren<AudioSource>())
        {
            audio.volume = 0f;
        }

        lvl = GetComponent<Level>();

        Helper.Wait(bossCountdown, () =>
        {
            Enemy go = Instantiate(bossPrefab);
            go.SetAsBoss(GetComponent<Level>());
            go.transform.position = arenaSprite.transform.position = arenaSprite.bounds.center + Vector3.down * 5f;
            foreach (AudioSource audio in bossAudio.transform.parent.GetComponentsInChildren<AudioSource>())
            {
                audio.volume = 0f;
            }
            bossAudio.volume = 0.2f;

        });
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnCooldownReady)
        {
            SpawnEnemies();
        }

        if (miniBossSpawnCooldownReady)
        {
            SpawnMiniBoss();
        }

        stageDuration += Time.deltaTime;

        if (bossCountdown - (int)stageDuration <= 1)
        {
            countDownText.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            countDownText.text = $"{bossCountdown - (int)stageDuration} second(s)";
        }

    }

    private void SpawnMiniBoss()
    {
        miniBossSpawnCooldownReady = false;
        Enemy miniBoss = SpawnEnemy();
        miniBoss.SetAsMiniboss();

        Helper.Wait(minibossCooldown, () =>
        {
            miniBossSpawnCooldownReady = true;
        });
    }

    void SpawnEnemies()
    {
        spawnCooldownReady = false;
        for (int i = 0; i < 1 + ((lvl.level - 1) * 5) + (int)stageDuration / 30; i++)
        {
            SpawnEnemy();
        }

        Helper.Wait(spawnCooldown, () =>
        {
            spawnCooldownReady = true;
        });
    }

    private Enemy SpawnEnemy()
    {
        bool side = Convert.ToBoolean(UnityEngine.Random.Range(0, 2));
        float mult = (UnityEngine.Random.Range(0, 2) - 0.5f) * 2;
        float xPos = side ? UnityEngine.Random.Range(-arenaSprite.bounds.extents.x, arenaSprite.bounds.extents.x) : mult * arenaSprite.bounds.extents.x;
        float yPos = !side ? UnityEngine.Random.Range(-arenaSprite.bounds.extents.y, arenaSprite.bounds.extents.y) : mult * arenaSprite.bounds.extents.y;
        int index = SpawnTable();
        return Instantiate(enemyPrefabs[index], arenaSprite.transform.position + new Vector3(xPos, yPos, 0), enemyPrefabs[index].transform.rotation, enemyContainer.transform);
    }

    private int SpawnTable()
    {
        int val = UnityEngine.Random.Range(0 + (int)stageDuration / 30, 100);
        switch (val)
        {
            case < 50:
                return 0;
            case < 58:
                return 1;
            case < 68:
                return 2;
            case < 83:
                return 3;
            case < 95:
                return 4;
            case <= 100:
                return 5;

            default: return 0;
        }
    }

}
