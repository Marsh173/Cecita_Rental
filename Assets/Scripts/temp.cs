using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class temp : MonoBehaviour
{
    public TMP_Text monologue;

   
    private void EnterCutScene()
    {
        foreach (Scene scene in ElevatorController.loadedScenes)
        {
            if (scene.name != "Day_General_System" && scene.name != "Elevator")
            {
                SceneManager.UnloadSceneAsync(scene);
                Debug.Log("Unloaded scene: " + scene.name);
            }
        }

        SceneManager.LoadScene("Third_Floor", LoadSceneMode.Additive);
        SceneManager.LoadScene("Protagonist_Room", LoadSceneMode.Additive);
        StartCoroutine(End());
    }

    IEnumerator End()
    {
        CutSceneScript.enteredEndSequen = true;
        monologue.text = "You hear the delivery being made to each apartment who has a package. " +
            "Suddenly, a wave of dizziness overflows you, it is as if your body is not your own anymore. You stumble toward the bed room and fell to your knees. " +
            "Something is definitly not right.";

        yield return new WaitForSeconds(8f);
        SceneManager.LoadScene("Night_One");
    }
}
