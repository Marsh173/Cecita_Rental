using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerCantOpen : MonoBehaviour
{
    public static bool Enter = false;

    //when play enters can't open eye area
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Enter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enter = false;
        Destroy(gameObject);
    }
}
