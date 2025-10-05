using System;
using System.Collections;
using UnityEngine;
public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private SpriteRenderer arenaSprite;
    [SerializeField] private Enemy[] enemyPrefabs;
    [SerializeField] private Enemy bossPrefab;
    [SerializeField] private GameObject enemyContainer;

    [SerializeField] private float spawnCooldown;
    [SerializeField] private int spawnAmount;
    [SerializeField] private int bossCountdown = 180;

    private bool spawnCooldownReady = true;

    float stageDuration = 0;


    void Start()
    {
        Helper.Wait(bossCountdown, () =>
        {
            Instantiate(bossPrefab);
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnCooldownReady)
        {
            SpawnEnemies();
        }

        stageDuration += Time.deltaTime;
    }

    void SpawnEnemies()
    {
        spawnCooldownReady = false;
        Debug.Log(1 + (int)stageDuration / 30);
        for (int i = 0; i < 1 + (int)stageDuration / 30; i++)
        {
            bool side = Convert.ToBoolean(UnityEngine.Random.Range(0, 2));

            float mult = (UnityEngine.Random.Range(0, 2) - 0.5f) * 2;

            float xPos = side ? UnityEngine.Random.Range(-arenaSprite.bounds.extents.x, arenaSprite.bounds.extents.x) : mult * arenaSprite.bounds.extents.x;
            float yPos = !side ? UnityEngine.Random.Range(-arenaSprite.bounds.extents.y, arenaSprite.bounds.extents.y) : mult * arenaSprite.bounds.extents.y;

            int index = SpawnTable();
            Enemy go = Instantiate(enemyPrefabs[index], new Vector3(xPos, yPos, 0), enemyPrefabs[index].transform.rotation, enemyContainer.transform);
        }

        Helper.Wait(spawnCooldown, () =>
        {
            spawnCooldownReady = true;
        });
    }

    private int SpawnTable()
    {
        int val = UnityEngine.Random.Range(0 + (int)stageDuration / 30, 100);
        switch (val)
        {
            case < 30:
                return 0;
            case < 50:
                return 1;
            case < 70:
                return 2;
            case < 85:
                return 3;
            case < 95:
                return 4;
            case <= 100:
                return 5;

            default: return 0;
        }
    }

}
