using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAdditionalSceneOnStart : MonoBehaviour
{
    public string currentSceneName;
    public List<string> additionalSceneNames;

    private void OnEnable()
    {
        // Subscribe to the sceneLoaded event.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from the sceneLoaded event to avoid memory leaks.
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the currently loaded scene is the one that triggers the loading.
        if (scene.name == currentSceneName)
        {
            // Load each additional scene additively.
            foreach (string sceneName in additionalSceneNames)
            {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            }
        }
    }
}
