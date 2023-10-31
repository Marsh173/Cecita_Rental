using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerDoorController : MonoBehaviour
{
    [SerializeField]
    private Animator myDoorAnimate;
    private bool openTrigger = false;
    private bool closeTrigger = false;

    private string doorOpen = "DoorOpen";
    private string doorClose = "DoorClose";
    private string Idle = "Idle";
    AnimatorClipInfo[] m_CurrentClipInfo;

    private void Start()
    {
        Debug.Log("Animator Test"+myDoorAnimate.GetCurrentAnimatorClipInfo(0)[0].clip.name);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (openTrigger)
            {
                Debug.Log(myDoorAnimate);
                myDoorAnimate.Play(doorOpen, 0,0.0f);
                gameObject.SetActive(false);
            }
            else if (closeTrigger)
            {
                Debug.Log("close");
                StartCoroutine(ReturnToClose());

                if (!gameObject.CompareTag("startRoomDoor"))
                {
                    gameObject.SetActive(false);
                }


            }
        }
    }


    IEnumerator ReturnToClose ()
    {
        myDoorAnimate.Play(doorClose, 0, 0.0f);
        yield return new WaitForSeconds(0.01f);
        myDoorAnimate.Play(Idle, 0, 0.0f);
    }
}
