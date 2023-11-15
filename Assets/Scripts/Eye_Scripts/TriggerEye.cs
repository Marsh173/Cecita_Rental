using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEye : MonoBehaviour
{
    [SerializeField] private GameObject eyeIcon, MonsterZone, DeadEndZone;
    private bool MonsterZoneHasPlayed, DeadEndZoneHasPlayed = false;
    public static bool enteredCantOpen;

    private void Start()
    {
        eyeIcon.SetActive(false);
    }

    private void Update()
    {
         if (Respawn.dead)
        {
            eyeIcon.SetActive(false);
            FirstPersonAIO.instance.cameraInputMethod = FirstPersonAIO.CameraInputMethod.Traditional;
        }
    }

    //check if the mechanic needs to be activated
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hallway"))
        {
            //enteredCantOpen = false;
            eyeIcon.SetActive(true);
            FirstPersonAIO.instance.cameraInputMethod = FirstPersonAIO.CameraInputMethod.TraditionalWithConstraints;
        }

        if (other.CompareTag("CantOpen"))
        {
            eyeIcon.SetActive(true);
            enteredCantOpen = true;
            FirstPersonAIO.instance.cameraInputMethod = FirstPersonAIO.CameraInputMethod.TraditionalWithConstraints;
        }
        else
        {
            enteredCantOpen = false;
        }

        if (other.CompareTag("Enclosed"))
        {
            FirstPersonAIO.instance.cameraInputMethod = FirstPersonAIO.CameraInputMethod.Traditional;
            //enteredCantOpen = false;
            //needs to play open eye animation then false

            eyeIcon.SetActive(false);
        }

        //tutorial stuff start
        if (other.CompareTag("EnterMonster"))
        {
            if (!MonsterZoneHasPlayed)
            {
                MonsterZone.GetComponent<Animation>().Play();
                MonsterZoneHasPlayed = true;
            }
        }
        //tutorial stuff end

        /*if (other.CompareTag("EnterDeadEnd"))
        {
            if (!DeadEndZoneHasPlayed)
            {
                DeadEndZone.GetComponent<Animation>().Play();
                DeadEndZoneHasPlayed = true;
            }
        }*/


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hallway") || other.CompareTag("CantOpen"))
        {
            FirstPersonAIO.instance.cameraInputMethod = FirstPersonAIO.CameraInputMethod.Traditional;
        }

        if (other.CompareTag("CantOpen"))
        {
            enteredCantOpen = false;
        }
    }

}
