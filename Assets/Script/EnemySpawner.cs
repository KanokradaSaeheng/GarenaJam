using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject swarmerPrefab;
    [SerializeField]
    private GameObject bigSwarmerPrefab;

    [SerializeField]
    private float swarmerInterval = 3.5f;
    [SerializeField]
    private float bigSwarmerInterval = 10f;

    [SerializeField]
    private int maxEnemies = 20;

    private List<GameObject> activeEnemies = new List<GameObject>();
    private int totalEnemiesSpawned = 0;
    private bool stopSpawning = false;

    void Start()
    {
        StartCoroutine(SpawnEnemy(swarmerInterval, swarmerPrefab));
        StartCoroutine(SpawnEnemy(bigSwarmerInterval, bigSwarmerPrefab));
    }

    private IEnumerator SpawnEnemy(float interval, GameObject enemy)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            // Remove dead enemies from the list
            activeEnemies.RemoveAll(e => e == null);

            // Stop spawning if all enemies have been spawned and defeated
            if (stopSpawning) yield break;

            // Only spawn if under max limit
            if (totalEnemiesSpawned < maxEnemies)
            {
                Vector3 spawnPos = new Vector3(Random.Range(-5f, 5f), 1f, 0f);
                GameObject newEnemy = Instantiate(enemy, spawnPos, Quaternion.identity);
                activeEnemies.Add(newEnemy);
                totalEnemiesSpawned++;
            }
            else if (activeEnemies.Count == 0)
            {
                stopSpawning = true;
                Debug.Log("All enemies defeated. Spawning stopped!");
                yield break;
            }
        }
    }
}