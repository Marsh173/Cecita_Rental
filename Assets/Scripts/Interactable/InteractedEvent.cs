using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractedEvent : InteractableItem
{
    PlayerInteract playerInteract;

    [SerializeField] int waitTime;
    Coroutine coroutineRef;
    public UnityEvent EventPreRecording;

    private void Start()
    {
        playerInteract = FindObjectOfType<PlayerInteract>();
    }

    public void Update()
    {
        if (playerInteract.hasTurnedOn)
        {
            if (interacted)
            {
                if (Input.GetMouseButtonDown(0)) coroutineRef = StartCoroutine(RunRecordEvents());
                //if (Input.GetMouseButtonUp(0)) StopCoroutine(coroutineRef);                           //In case we want a hold to record input
            }
        }
    }



    IEnumerator RunRecordEvents()
    {
        EventPreRecording.Invoke();
        RecordMe();
        yield return new WaitForSeconds(waitTime);                                                       //Put this line on top or middle of this section of code in case we want a hold input to complete the recording
    }
}
