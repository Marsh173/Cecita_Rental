using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEye : MonoBehaviour
{
    public GameObject eyeIcon, MonsterZone, DeadEndZone;
    private bool MonsterZoneHasPlayed, DeadEndZoneHasPlayed = false;
    public static bool enteredCantOpen;

    private void Start()
    {
        eyeIcon.SetActive(false);
    }

    private void Update()
    {

    }

    //check if the mechanic needs to be activated
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hallway"))
        {
            enteredCantOpen = false;
            eyeIcon.SetActive(true);
            FirstPersonAIO.instance.cameraInputMethod = FirstPersonAIO.CameraInputMethod.TraditionalWithConstraints;

        }

        if (other.CompareTag("Enclosed"))
        {
            enteredCantOpen = false;
            //play open eye animation then false

            //NoSightAllowed.instance.ResetChargeBar();
            eyeIcon.SetActive(false);
        }

        if (other.CompareTag("CantOpen"))
        {
            eyeIcon.SetActive(true);
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

        if (other.CompareTag("EnterDeadEnd"))
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

}
