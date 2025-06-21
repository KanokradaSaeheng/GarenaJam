using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GrowSystem growSystem;
    public GameObject spawnPrefab;
    public GameObject spawnCenter;
    public float spawnRange = 5f;
    public int maxEnemies = 10;
    public int waitSeconds = 3;

    private int enemiesCount = 0;

    private void Start()
    {
        StartCoroutine(StartEnemiesSpawner());
    }

    public void DecreaseEnemiesCount()
    {
        if (enemiesCount > 0)
        {
            enemiesCount--;
        }
    }

    private IEnumerator StartEnemiesSpawner()
    {
        while (true)
        {
            if (growSystem != null && growSystem.Isfullygrown && enemiesCount < maxEnemies)
            {
                Vector3 pos = GetRandomSpawnPosition();
                Instantiate(spawnPrefab, pos, Quaternion.identity);
                enemiesCount++;

                // Reset crop
                growSystem.ResetGrowth();
            }

            yield return new WaitForSeconds(waitSeconds);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float offsetX = UnityEngine.Random.Range(-spawnRange, spawnRange);
        float offsetZ = UnityEngine.Random.Range(-spawnRange, spawnRange);
        return spawnCenter.transform.position + new Vector3(offsetX, 0f, offsetZ);
    }
}