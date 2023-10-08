using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractedEvent : InteractableItem
{
    public UnityEvent EventOnInteraction;
    [SerializeField] int holdTime;
    Coroutine coroutineRef;

    public void Update()
    {
        if (interacted)
        {
            if (Input.GetKeyDown(KeyCode.R)) coroutineRef = StartCoroutine(RunRecordEvents());
            if (Input.GetKeyUp(KeyCode.R)) StopCoroutine(coroutineRef);
        }
        else if (coroutineRef != null) StopCoroutine(coroutineRef);
    }

    // Update is called once per frame
    public void RunEvent()
    {
        EventOnInteraction.Invoke();
    }

    IEnumerator RunRecordEvents()
    {
        yield return new WaitForSeconds(holdTime);
        RunEvent();
        RecordMe();
    }
}
