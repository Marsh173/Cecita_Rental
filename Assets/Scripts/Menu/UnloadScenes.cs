using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnloadScenes : MonoBehaviour
{
    public string sceneNameToUnload; // Name of the scene to unload.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if the scene is loaded, then unload it.
            if (SceneManager.GetSceneByName(sceneNameToUnload).isLoaded)
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(sceneNameToUnload).buildIndex);
            }
        }
    }
}
