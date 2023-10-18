using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadProximityScene : MonoBehaviour
{
    public string firstSceneName;
    public string secondSceneName; // Name of the additional scene to load.

    private bool isPlayerNear = false;
    private bool isLoading = false;

    private void Update()
    {
        if (isPlayerNear && !isLoading)
        {
            // Prevent further loading attempts until the current one is complete.
            isLoading = true;

            // Check which scene is currently loaded and load the other one.
            if (SceneManager.GetSceneByName(firstSceneName).isLoaded)
            {
                Debug.Log("Loading " + secondSceneName);
                SceneManager.LoadSceneAsync(secondSceneName, LoadSceneMode.Additive);
            }
            //else if (SceneManager.GetSceneByName(secondSceneName).isLoaded)
            //{
            //    Debug.Log("Loading " + firstSceneName);
            //    SceneManager.LoadSceneAsync(firstSceneName, LoadSceneMode.Additive);
            //}
        }
        else if (!isPlayerNear && isLoading)
        {
            // If the player moves away, mark the scene loading as completed.
            isLoading = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
