using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerEye : MonoBehaviour
{
    public GameObject eye, MonsterZone, DeadEndZone, player, checkpoint;
    private bool MonsterZoneHasPlayed, DeadEndZoneHasPlayed = false;
    public static bool enteredCantOpen, dead, restarted;

    private void Start()
    {
        eye.SetActive(false);
        dead = restarted = false;
    }

    private void Update()
    {
        //check death status
        deathSituations();
    }

    //check if the mechanic needs to be activated
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hallway"))
        {
            enteredCantOpen = false;
            eye.SetActive(true);
            FirstPersonAIO.instance.cameraInputMethod = FirstPersonAIO.CameraInputMethod.TraditionalWithConstraints;

        }

        if (other.CompareTag("Enclosed"))
        {
            //play open eye animation then false
            enteredCantOpen = false;
            eye.SetActive(false);
        }

        if (other.CompareTag("CantOpen"))
        {
            eye.SetActive(true);
            enteredCantOpen = true;
            FirstPersonAIO.instance.cameraInputMethod = FirstPersonAIO.CameraInputMethod.TraditionalWithConstraints;
        }


        if (other.CompareTag("EnterMonster"))
        {
            if (!MonsterZoneHasPlayed)
            {
                MonsterZone.GetComponent<Animation>().Play();
                MonsterZoneHasPlayed = true;
            }
        }

        if(other.CompareTag("EnterDeadEnd"))
        {
            if (!DeadEndZoneHasPlayed)
            {
                DeadEndZone.GetComponent<Animation>().Play();
                DeadEndZoneHasPlayed = true;
            }
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hallway") || other.CompareTag("CantOpen"))
        {
            FirstPersonAIO.instance.cameraInputMethod = FirstPersonAIO.CameraInputMethod.Traditional;
        }
    }

    public void deathSituations()
    {
        //instant restart when in tutorial level
        if (dead)
        {
            if (SceneManager.GetActiveScene().name == "TutorialLevel" || SceneManager.GetActiveScene().name == "TutorialLevel restart")
            {
                SceneManager.LoadScene("TutorialLevel restart");
                //Respawn();
            }
            else SceneManager.LoadScene("Death");
        }

    }
    IEnumerator Respawn()
    {
        Destroy(gameObject, 1f);
        yield return new WaitForSeconds(1f);
        Instantiate(player, checkpoint.transform);
        Debug.Log("respawned");
        restarted = true;
        //player.transform.position = checkpoint.transform.position;
    }
}
