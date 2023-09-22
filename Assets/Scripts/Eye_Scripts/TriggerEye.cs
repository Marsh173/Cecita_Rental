using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEye : MonoBehaviour
{
    public GameObject eye;
    public static bool enteredCantOpen;
    public GameObject MonsterZone;
    public GameObject DeadEndZone;
    private bool MonsterZoneHasPlayed = false;
    private bool DeadEndZoneHasPlayed = false;

    private void Start()
    {
        eye.SetActive(false);
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
}
