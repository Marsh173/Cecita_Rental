using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableItemWithEvent : InteractableItem
{
    protected PlayerInteract playerInteract;

    [SerializeField] int recordWaitTime;
    [SerializeField] int interactWaitTime;
    Coroutine coroutineRef;
    public UnityEvent EventPreRecording;
    public UnityEvent EventOnInteraction;

    //[SerializeField] private GameObject taskUI;
    private void Start()
    {
        playerInteract = FindObjectOfType<PlayerInteract>();
        //taskUI = GameObject.FindWithTag("TaskManager");
    }

    public void Update()
    {
        if (PlayerInteract.hasRecorderInHand)
        {
            if (ableToInteract)
            {
                if (this.AItem != null)
                {
                    if (Input.GetMouseButtonDown(0)) coroutineRef = StartCoroutine(RunRecordEvents());
                    //if (Input.GetMouseButtonUp(0)) StopCoroutine(coroutineRef);                           //In case we want a hold to record input
                }
            }
        }
        else
        {
            if (ableToInteract)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    coroutineRef = StartCoroutine(RunInteractEvents());
                    interactedAndClicked = true;
                }
                //if (Input.GetMouseButtonUp(0)) StopCoroutine(coroutineRef);                           //In case we want a hold to record input
            }
        }
    }


    protected IEnumerator RunInteractEvents()
    {
        /*if (this.tag == "NPC")
        {
            taskUI.SetActive(false);
        }*/
        EventOnInteraction.Invoke();
        yield return new WaitForSeconds(interactWaitTime);                                                       //Put this line on top or middle of this section of code in case we want a hold input to complete the recording
        //taskUI.SetActive(true);
    }

    protected IEnumerator RunRecordEvents()
    {
        EventPreRecording.Invoke();
        RecordMe();
        yield return new WaitForSeconds(recordWaitTime);                                                       //Put this line on top or middle of this section of code in case we want a hold input to complete the recording
    }
}
