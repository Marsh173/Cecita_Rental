using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayOpenDoorController : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;
    private bool OpenDoorNow = false;
    private bool delayFinish = false;
    private bool hasPlayed = false;

    [SerializeField] private string doorOpen = "DoorOpen";
    

    [SerializeField] private float delay = 0.0f;

    public GameObject doorOpenSound;

    private void Start()
    {
        doorOpenSound.SetActive(false);
        delayFinish = false;
        hasPlayed = false;
    }


    private void Update()
    {
        if (!OpenDoorNow)
        {
            Debug.Log("start delay");
            StartCoroutine(delayOpen());
        }

        if (delayFinish && InventoryManager.equipmentCollected && !hasPlayed)
        {
            hasPlayed = true;
            Debug.Log("open");
            myDoor.Play(doorOpen, 0, 0.0f);
            doorOpenSound.SetActive(true);
        }

    }

    IEnumerator delayOpen()
    {
        OpenDoorNow = true;
        yield return new WaitForSeconds(delay);
        delayFinish = true;
        Debug.Log("finish delay" + delayFinish);

       /* if(InventoryManager.equipmentCollected)
        {
            myDoor.Play(doorOpen, 0, 0.0f);
            doorOpenSound.SetActive(true);
        }*/
       
       
    }

}
