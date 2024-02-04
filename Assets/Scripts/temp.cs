using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class temp : MonoBehaviour
{
    public TMP_Text monologue;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")/* && CutSceneScript.talkedtoNPC*/)
        {
            CutSceneScript.enteredEndSequen = true;
            monologue.text = "You hear the delivery being made to each apartment who has a package. " +
                "Suddenly, a wave of dizziness overflows you, it is as if your body is not your own anymore. You stumble toward the bed room and fell to your knees. " +
                "Something is definitly not right.";
            StartCoroutine(wait());
            SceneManager.LoadScene("Night_One");
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(8f);
    }
}
