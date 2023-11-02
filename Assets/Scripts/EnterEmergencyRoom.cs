using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterEmergencyRoom : MonoBehaviour
{
    public GameObject doorChase;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorChase.SetActive(false);
        }
    }
}
