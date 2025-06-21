using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrowSystem : MonoBehaviour
{
    public List<GameObject> growstages;
    public float timeBetweenGrows;
    public int currentStage = 0;
    private GameObject currentInstance;
    public bool Isfullygrown { get; private set; } = false;


    private Coroutine growRoutine;

    private void Start()
    {
        if (growstages.Count == 0)
        {
            Debug.LogWarning("No growstages assigned");
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
        Isfullygrown = false;
        currentStage = 0;

        while (currentStage < growstages.Count)
        {
            if (currentInstance != null)
                Destroy(currentInstance);

            currentInstance = Instantiate(growstages[currentStage], transform.position, Quaternion.identity, transform);
            yield return new WaitForSeconds(timeBetweenGrows);
            currentStage++;
        }

        Debug.Log("Fully Grown");
        Isfullygrown = true;
    }

    // ✅ Reset method to be called from outside
    public void ResetGrowth()
    {
        if (currentInstance != null)
            Destroy(currentInstance);

        Isfullygrown = false;
        currentStage = 0;

        StartGrowing(); // Restart growth
    }
}