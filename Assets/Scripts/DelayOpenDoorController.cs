using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayOpenDoorController : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;
    private bool OpenDoorNow = false;

    [SerializeField] private string doorOpen = "DoorOpen";
    

    [SerializeField] private float delay = 0.0f;

    public GameObject doorOpenSound;

    private void Start()
    {
        doorOpenSound.SetActive(false);
    }


    private void Update()
    {
        if (!OpenDoorNow)
        {
            Debug.Log("to delay");
            StartCoroutine(delayOpen());
        }
    
    }

    IEnumerator delayOpen()
    {
        OpenDoorNow = true;
        yield return new WaitForSeconds(delay);
        Debug.Log("finish delay");
        myDoor.Play(doorOpen, 0, 0.0f);
        doorOpenSound.SetActive(true);
       
    }

}
