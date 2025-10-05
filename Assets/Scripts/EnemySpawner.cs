using System;
using System.Collections;
using UnityEngine;
public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private SpriteRenderer arenaSprite;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private GameObject enemyContainer;

    [SerializeField] private float spawnCooldown;
    [SerializeField] private float spawnAmount;

    private bool spawnCooldownReady = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(arenaSprite.bounds.size);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnCooldownReady)
        {
            SpawnEnemies();
        }
    }

    void SpawnEnemies()
    {
        spawnCooldownReady = false;
        for (int i = 0; i < spawnAmount; i++)
        {
            bool side = Convert.ToBoolean(UnityEngine.Random.Range(0, 2));

            float mult = (UnityEngine.Random.Range(0, 2) - 0.5f) * 2;

            float xPos = side ? UnityEngine.Random.Range(-arenaSprite.bounds.extents.x, arenaSprite.bounds.extents.x) : mult * arenaSprite.bounds.extents.x;
            float yPos = !side ? UnityEngine.Random.Range(-arenaSprite.bounds.extents.y, arenaSprite.bounds.extents.y) : mult * arenaSprite.bounds.extents.y;


            Enemy go = Instantiate(enemyPrefab, new Vector3(xPos, yPos, 0), enemyPrefab.transform.rotation, enemyContainer.transform);
        }

        Helper.Wait(spawnCooldown, () =>
        {
            spawnCooldownReady = true;
        });
    }

}
