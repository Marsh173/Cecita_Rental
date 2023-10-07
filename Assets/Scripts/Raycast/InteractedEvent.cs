using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractedEvent : MonoBehaviour
{
    public UnityEvent EventOnInteraction;

    // Update is called once per frame
    public void RunEvent()
    {
        EventOnInteraction.Invoke();
    }
}
