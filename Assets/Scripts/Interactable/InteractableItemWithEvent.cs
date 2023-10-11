using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableItemWithEvent : InteractableItem
{
    PlayerInteract playerInteract;

    [SerializeField] int recordWaitTime;
    [SerializeField] int interactWaitTime;
    Coroutine coroutineRef;
    public UnityEvent EventPreRecording;
    public UnityEvent EventOnInteraction;

    private void Start()
    {
        playerInteract = FindObjectOfType<PlayerInteract>();
    }

    public void Update()
    {
        if (playerInteract.hasRecorderTurnedOn)
        {
            if (interacted)
            {
                if (Input.GetMouseButtonDown(0)) coroutineRef = StartCoroutine(RunRecordEvents());
                //if (Input.GetMouseButtonUp(0)) StopCoroutine(coroutineRef);                           //In case we want a hold to record input
            }
        }
        else
        {
            if (interacted)
            {
                if (Input.GetMouseButtonDown(0)) coroutineRef = StartCoroutine(RunInteractEvents());
                //if (Input.GetMouseButtonUp(0)) StopCoroutine(coroutineRef);                           //In case we want a hold to record input
            }
        }
    }


    IEnumerator RunInteractEvents()
    {
        EventOnInteraction.Invoke();
        yield return new WaitForSeconds(interactWaitTime);                                                       //Put this line on top or middle of this section of code in case we want a hold input to complete the recording
    }

    IEnumerator RunRecordEvents()
    {
        EventPreRecording.Invoke();
        RecordMe();
        yield return new WaitForSeconds(recordWaitTime);                                                       //Put this line on top or middle of this section of code in case we want a hold input to complete the recording
    }
}
