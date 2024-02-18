using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;
using System;


public class TriggerDoorController : MonoBehaviour
{
    [SerializeField] private Animator myDoorAnimator;
    [SerializeField] public bool openTrigger = false, closeTrigger = false, playedOpen = false;
    private string doorOpen = "DoorOpen";
    private string doorClose = "DoorClose";
    private string Idle = "Idle";
    public static bool playedCloseAnim = false;
    private Door loungeDoor;

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

        playedOpen = false;

        findDoorScript();

    }

    void findDoorScript()
    {
        //get parent - interactive door
        //get first child - door
        //get first child - door_handle that has the script attached
        Transform door_handle_transform = this.transform.parent.gameObject.transform.GetChild(0).GetChild(0);
        //Debug.Log("Get the sibling: " + door_handle_transform.gameObject.name);
        try
        {
            loungeDoor = door_handle_transform.gameObject.GetComponent<Door>();
            //Debug.Log("Door script?" + loungeDoor);
        }
        catch(NullReferenceException e)
        {
            
        }

    }

    private void Update()
    {
        if(Respawn.dead) gameObject.SetActive(true);

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
                
                if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING && !playedOpen)
                {
                    
                    doorOpenSound.start();
                    playedOpen = true;
                }

            }
            else if (closeTrigger)
            {
                myDoorAnimator.SetBool("Opened", true);
                myDoorAnimator.SetBool("Closed", false);
               
                FMOD.Studio.PLAYBACK_STATE fmodPbState;
                doorCloseSound.getPlaybackState(out fmodPbState);

                if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                {
                    playedOpen = false;
                    doorCloseSound.start();
                }

                if (gameObject.CompareTag("LoungeDoorInside"))
                {
                    GameObject taggedObject = GameObject.FindWithTag("LoungeDoor");
                    if (taggedObject != null)
                    {
                        //Debug.Log("change to Close trigger!");
                        TriggerDoorController trigScript = taggedObject.GetComponent<TriggerDoorController>();

                        if (trigScript == null) Debug.Log("more than one object has this LoungeDoor tag");

                        trigScript.closeTrigger = true;
                        trigScript.openTrigger = false;

                    }

                    //change prompt message of this door to open the door
                    if(loungeDoor != null)
                        loungeDoor.promptMessage = "Open the Door";
                }

    
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
                
                closeTrigger = false;
                //myDoorAnimator.SetBool("Opened", false);
                myDoorAnimator.SetBool("Closed", true);

            }

            //change the outside trigger from open door to close door.
            //check if open door trigger has been used (close door animation is played) 
          
            //change the outside trigger from close door to open door.
            else if (gameObject.CompareTag("LoungeDoor") && closeTrigger)
            {
                
                openTrigger = true;
                closeTrigger = false;

                //if the trigger outside the lounge is used as a open trigger, then reenable the closetrigger.
                GameObject taggedObject = GameObject.FindWithTag("LoungeDoorInside");
                if(taggedObject != null)
                {
                    
                    TriggerDoorController trigScript = taggedObject.GetComponent<TriggerDoorController>();
                    trigScript.closeTrigger = true;
                    
                }
            }

            else if (!gameObject.CompareTag("LoungeDoor") && closeTrigger)
            {
                gameObject.SetActive(false);
                myDoorAnimator.SetBool("Opened", false);
                myDoorAnimator.SetBool("Closed", false);
            }

           
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
