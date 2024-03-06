using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEye : MonoBehaviour
{
    [SerializeField] private GameObject eyeIcon, MonsterZone;

    private AudioSource earBudVoice;

    private bool MonsterZoneHasPlayed, firstEyeWarning;
    public static bool enteredCantOpen;

    private void Start()
    {
        eyeIcon.SetActive(false);
        MonsterZoneHasPlayed = firstEyeWarning = false;
        earBudVoice = GetComponent<AudioSource>();
    }

    private void Update()
    {
         if (Respawn.dead)
        {
            eyeIcon.SetActive(false);
            FirstPersonAIO.instance.cameraInputMethod = FirstPersonAIO.CameraInputMethod.Traditional;
        }

        //tutorial warning about eye amount
        if (!firstEyeWarning && NoSightAllowed.CurrentEyeBarAmount <= 25f && eyeIcon.activeSelf && InventoryManager.EquipmentCollected)
        {
            earBudVoice.Stop();
            earBudVoice.clip = Resources.Load<AudioClip>("Night 0/" + "Night 0 - Close!");
            earBudVoice.PlayOneShot(earBudVoice.clip);
            firstEyeWarning = true;
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
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hallway") || other.CompareTag("CantOpen"))
        {
            FirstPersonAIO.instance.cameraInputMethod = FirstPersonAIO.CameraInputMethod.Traditional;
            eyeIcon.SetActive(false);
        }

        if (other.CompareTag("CantOpen"))
        {
            enteredCantOpen = false;
        }

        if (other.CompareTag("Enclosed"))
        {
            FirstPersonAIO.instance.cameraInputMethod = FirstPersonAIO.CameraInputMethod.TraditionalWithConstraints;
            eyeIcon.SetActive(true);
        }
    }

}
