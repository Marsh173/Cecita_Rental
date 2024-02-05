using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class temp : MonoBehaviour
{
    public void EnterEndCutScene()
    {
        foreach (Scene scene in ElevatorController.loadedScenes)
        {
            if (scene.name != "Day_General_System" && scene.name != "Elevator")
            {
                SceneManager.UnloadSceneAsync(scene);
                Debug.Log("Unloaded scene tv: " + scene.name);
            }
        }
        CutSceneScript.enteredEndSequen = true;
        SceneManager.LoadScene("Third_Floor", LoadSceneMode.Additive);
        SceneManager.LoadScene("Protagonist_Room", LoadSceneMode.Additive);
    }
}
