using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrowSystem : MonoBehaviour
{
    public List<GameObject> growStages;
    public float timeBetweenGrows;
    public int currentStage = 0;
    private GameObject currentInstance;

    public bool IsFullyGrown { get; private set; } = false;
    public bool HasSpawned { get; set; } = false;

    private Coroutine growRoutine;

    private void Start()
    {
        if (growStages.Count == 0)
        {
            Debug.LogWarning("No grow stages assigned!");
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

        while (currentStage < growStages.Count)
        {
            if (currentInstance != null)
                Destroy(currentInstance);

            currentInstance = Instantiate(growStages[currentStage], transform.position, Quaternion.identity, transform);
            yield return new WaitForSeconds(timeBetweenGrows);
            currentStage++;
        }

        IsFullyGrown = true;
        Debug.Log("Fully Grown!");
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