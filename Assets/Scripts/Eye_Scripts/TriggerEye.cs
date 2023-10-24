using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


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
            NoSightAllowed.instance.TimerActive = true;
            NoSightAllowed.instance.F.GetComponent<TMP_Text>().text = "Press F to close your eyes";
            NoSightAllowed.instance.countdownText.enabled = true;

        }

        if (other.CompareTag("Enclosed"))
        {
            //play open eye animation then false
            enteredCantOpen = false;
            eye.SetActive(false);


            //Reset the charge bar and disable the red aura effect                                       --Eric's latest
            if (NoSightAllowed.instance != null)
            {
                Debug.Log("enter enclosed");
                NoSightAllowed.instance.JustEyeOpenAnimation();
                NoSightAllowed.instance.TimerActive = false;
                NoSightAllowed.countDown = NoSightAllowed.instance.countDownTime;
                Color tempcolor = NoSightAllowed.instance.RedAura.color;
                NoSightAllowed.instance.RedAura.color = new Color(tempcolor.r, tempcolor.g, tempcolor.b, 0);
                NoSightAllowed.instance.countdownText.text = "5";
                NoSightAllowed.instance.EyeChargeBar = 100;
                NoSightAllowed.instance.ChargeBarUI.GetComponent<Image>().fillAmount = 1;
                NoSightAllowed.instance.F.GetComponent<TMP_Text>().text = "Press F to open your eyes";

            }
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
