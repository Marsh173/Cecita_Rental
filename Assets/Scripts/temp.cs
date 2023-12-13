using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && CutSceneScript.talkedtoNPC)
        {
            CutSceneScript.enteredEndSequen = true;
        }
    }
}
