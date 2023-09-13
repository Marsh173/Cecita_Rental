using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEye : MonoBehaviour
{
    public GameObject eye;
    public static bool enteredCantOpen;

    private void Start()
    {
        eye.SetActive(false);
    }

    //check if the mechanic needs to be activated
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Outside"))
        {
            enteredCantOpen = false;
            eye.SetActive(true);
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
        }
    }
}
