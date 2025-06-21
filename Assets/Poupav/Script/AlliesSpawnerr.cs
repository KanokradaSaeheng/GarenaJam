using System.Collections;
using UnityEngine;

public class AlliesSpawnerr : MonoBehaviour
{
    public AlliesgrownSystem growSystem;
    public GameObject allyPrefab;
    public GameObject spawnCenter;
    public float spawnRange = 5f;
    public int maxAllies = 10;
    public int waitSeconds = 3;

    private int allyCount = 0;

    private void Start()
    {
        StartCoroutine(StartAllySpawner());
    }

    public void DecreaseAllyCount()
    {
        if (allyCount > 0)
        {
            allyCount--;
        }
    }

    private IEnumerator StartAllySpawner()
    {
        while (true)
        {
            if (growSystem != null && growSystem.IsFullyGrown && !growSystem.HasSpawned && allyCount < maxAllies)
            {
                Vector3 pos = GetRandomSpawnPosition();
                GameObject ally = Instantiate(allyPrefab, pos, Quaternion.identity);
                allyCount++;

                growSystem.HasSpawned = true;
                growSystem.ResetGrowth();

                // Optional: Activate Ally AI
                AllyAI ai = ally.GetComponent<AllyAI>();
                if (ai != null)
                {
                    ai.ActivateAI();
                }
            }

            yield return new WaitForSeconds(waitSeconds);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float offsetX = Random.Range(-spawnRange, spawnRange);
        float offsetZ = Random.Range(-spawnRange, spawnRange);
        return spawnCenter.transform.position + new Vector3(offsetX, 0f, offsetZ);
    }
}