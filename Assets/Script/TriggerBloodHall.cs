using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBloodHall : MonoBehaviour
{
    BoxCollider box;
    public static bool setEyeActive;
    void Start()
    {
        box = GetComponent<BoxCollider>();
        setEyeActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            setEyeActive = true;
        }
    }
}
