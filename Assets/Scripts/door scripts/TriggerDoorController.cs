using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;


public class TriggerDoorController : MonoBehaviour
{
    [SerializeField] private Animator myDoorAnimator;
    [SerializeField] public bool openTrigger = false;
    [SerializeField] public bool closeTrigger = false;
    private string doorOpen = "DoorOpen";
    private string doorClose = "DoorClose";
    private string Idle = "Idle";


    [Header("Door Sounds")]
    [SerializeField] EventReference doorOpenEventPath;
    [SerializeField] EventReference doorCloseEventPath;

    FMOD.Studio.EventInstance doorOpenSound; 
    FMOD.Studio.EventInstance doorCloseSound;
    FMOD.ChannelGroup mcg;
    FMOD.Studio.Bus Masterbus;

    private void Start()
    {
        //openTrigger = closeTrigger = false;
        doorOpenSound = FMODUnity.RuntimeManager.CreateInstance(doorOpenEventPath);
        doorCloseSound = FMODUnity.RuntimeManager.CreateInstance(doorCloseEventPath);
        doorOpenSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        doorCloseSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        Masterbus = FMODUnity.RuntimeManager.GetBus("Bus:/");
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || (other.CompareTag("Monster") && this.CompareTag("MonsterDoor")))
        { 
            if (openTrigger)
            {
                myDoorAnimator.SetBool("Opened", false);
                myDoorAnimator.SetBool("Closed", true);

                //Debug.Log("Closed" + myDoorAnimator.GetBool("Closed"));
                //Debug.Log("Opened" + myDoorAnimator.GetBool("Opened"));


                FMOD.Studio.PLAYBACK_STATE fmodPbState;
                doorOpenSound.getPlaybackState(out fmodPbState);
                if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                {
                    Debug.Log("this");
                    doorOpenSound.start();
                    Debug.Log("that");
                }

            }
            else if (closeTrigger)
            {
                myDoorAnimator.SetBool("Opened", true);
                myDoorAnimator.SetBool("Closed", false);
               
                FMOD.Studio.PLAYBACK_STATE fmodPbState;
                doorCloseSound.getPlaybackState(out fmodPbState);
                if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING) doorCloseSound.start();

                //Debug.Log("Closed" + myDoorAnimator.GetBool("Closed"));
                //Debug.Log("Opened" + myDoorAnimator.GetBool("Opened"));

                //if (!gameObject.CompareTag("startRoomDoor"))
                //{
                //    gameObject.SetActive(false);
                //}              
            }
            
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //disble close trigger when player is inside
            if (gameObject.CompareTag("LoungeDoorInside") && closeTrigger)
            {
                Debug.Log("inside the lounge");
                closeTrigger = false;
                myDoorAnimator.SetBool("Opened", false);
            }

            //change the outside trigger from open door to close door.
            else if(gameObject.CompareTag("LoungeDoor") && openTrigger)
            {
                openTrigger = false;
                closeTrigger = true;
            }

            //change the outside trigger from close door to open door.
            else if (gameObject.CompareTag("LoungeDoor") && closeTrigger)
            {
                openTrigger = true;
                closeTrigger = false;

                //if the trigger outside the lounge is used as a open trigger, then reenable the closetrigger.
                GameObject taggedObject = GameObject.FindWithTag("LoungeDoorInside");
                if(taggedObject != null)
                {
                    Debug.Log("enable close trigger!");
                    TriggerDoorController trigScript = taggedObject.GetComponent<TriggerDoorController>();
                    trigScript.closeTrigger = true;
                    
                }

            }

            else if (!gameObject.CompareTag("LoungeDoor"))
            {
                myDoorAnimator.SetBool("Opened", false);
                myDoorAnimator.SetBool("Closed", false);
            }

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


    public void OnSceneUnloaded(Scene unloadedScene)
    {
        // Stop the audio playback here
        doorOpenSound.release();
        doorCloseSound.release();
        Debug.Log("Unloaded");
        FMODUnity.RuntimeManager.CoreSystem.getMasterChannelGroup(out mcg);
        mcg.stop();
        Masterbus.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
