using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Task_Done : MonoBehaviour
{
    public UnityEvent action;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            action.Invoke();
        }
    }
}
