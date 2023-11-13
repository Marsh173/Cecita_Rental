using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorController : MonoBehaviour
{
    //check the current scene
    //check current elevator card used
    //Go to the according scene

    private List<Scene> loadedScenes = new List<Scene>();
    private int sceneCount;

    private void Start()
    {
        sceneCount = SceneManager.sceneCount;

        // Loop through all loaded scenes
        for (int i = 0; i < sceneCount; i++)
        {
            // Get the scene at index i
            Scene scene = SceneManager.GetSceneAt(i);
            loadedScenes.Add(scene);

            // Print or use the scene name
            Debug.Log("Loaded Scene: " + scene.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Elevator.UseThirdFloorElevator)
        {
            //unload third_floor and protagonist_room
            //load lobby after elevator cutscene

            foreach (Scene scene in loadedScenes)
            {
                if (scene.name != "General_System_Day")
                {
                    SceneManager.UnloadSceneAsync(scene);
                    Debug.Log("Unloaded Scene: " + scene.name);
                }
            }

            SceneManager.LoadScene("Lobby", LoadSceneMode.Additive);

            Elevator.UseThirdFloorElevator = false;
        }

        //else if use Lobby Elevator
    }
}
