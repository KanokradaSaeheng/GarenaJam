using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AlliesgrownSystem : MonoBehaviour
{
    public List<GameObject> allyStages;
    public float timeBetweenStages = 2f;
    public int currentStage = 0;
    private GameObject currentInstance;

    public bool IsFullyGrown { get; private set; } = false;
    public bool HasSpawned { get; set; } = false;

    private Coroutine growRoutine;

    private void Start()
    {
        if (allyStages.Count == 0)
        {
            Debug.LogWarning("No ally stages assigned!");
            return;
        }

        StartGrowing();
    }

    public void StartGrowing()
    {
        if (growRoutine != null)
            StopCoroutine(growRoutine);

        growRoutine = StartCoroutine(GrowRoutine());
    }

    IEnumerator GrowRoutine()
    {
        IsFullyGrown = false;
        HasSpawned = false;
        currentStage = 0;

        while (currentStage < allyStages.Count)
        {
            if (currentInstance != null)
                Destroy(currentInstance);

            currentInstance = Instantiate(allyStages[currentStage], transform.position, Quaternion.identity, transform);
            yield return new WaitForSeconds(timeBetweenStages);
            currentStage++;
        }

        IsFullyGrown = true;
        Debug.Log("Ally fully grown!");
    }

    public void ResetGrowth()
    {
        if (currentInstance != null)
            Destroy(currentInstance);

        IsFullyGrown = false;
        HasSpawned = false;
        currentStage = 0;

        StartGrowing();
    }
}