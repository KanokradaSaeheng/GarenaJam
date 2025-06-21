using UnityEngine;
using UnityEngine.SceneManagement;

public class TeamSplash : MonoBehaviour
{
    public float waitTime = 2f;
    public int nextSceneIndex = 1; // Set this in the Inspector

    void Start()
    {
        Invoke("LoadNextScene", waitTime);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneIndex);
    }
}
