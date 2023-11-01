using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerDoorController : MonoBehaviour
{
    [SerializeField] private Animator myDoorAnimator;
    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;
    private string doorOpen = "DoorOpen";
    private string doorClose = "DoorClose";
    private string Idle = "Idle";

    private void Start()
    {
       openTrigger = closeTrigger = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (openTrigger)
            {
                myDoorAnimator.SetBool("Opened", false);
                myDoorAnimator.SetBool("Closed", true);

                Debug.Log("Closed" + myDoorAnimator.GetBool("Closed"));
                Debug.Log("Opened" + myDoorAnimator.GetBool("Opened"));

            }
            else if (closeTrigger)
            {
                myDoorAnimator.SetBool("Opened", true);
                myDoorAnimator.SetBool("Closed", false);

                Debug.Log("Closed" + myDoorAnimator.GetBool("Closed"));
                Debug.Log("Opened" + myDoorAnimator.GetBool("Opened"));

                if (!gameObject.CompareTag("startRoomDoor"))
                {
                    gameObject.SetActive(false);
                }
               
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            /*if (gameObject.CompareTag("LoungeDoor") && !myDoorAnimator.GetBool("Opened") && myDoorAnimator.GetBool("Closed"))
            {
                myDoorAnimator.SetBool("Opened", true);
                myDoorAnimator.SetBool("Closed", false);
            }
            else
            {
                myDoorAnimator.SetBool("Opened", false);
                myDoorAnimator.SetBool("Closed", false);

                Debug.Log("Closed" + myDoorAnimator.GetBool("Closed"));
                Debug.Log("Opened" + myDoorAnimator.GetBool("Opened"));
            }*/
        }
    }
    IEnumerator ReturnToClose ()
    {
        yield return new WaitForSeconds(1f); 
        myDoorAnimator.SetBool("Opened", true);
        myDoorAnimator.SetBool("Closed", false);
    }
}
