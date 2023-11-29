using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOntriggerActivate : MonoBehaviour
{
    public GameObject UI;

    private void Start()
    {
        UI.SetActive(false);
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            UI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UI.SetActive(false);
        }
    }
}
