using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableItemWithEvent
{

    public Animator anim;
    public string DoorOpenClip;
    public string DoorCloseClip;
    public GameObject openSound;
    public GameObject closeSound;




    public void ChangeBehavior()
    {

        //Issue: when you exit lounge, door closes behind you on trigger.
        //When you reenter the lounge on trigger, this interact door state is set to "close the door". 
        //so you have to interact once - close the door, then interact the second time to open door.


        if(promptMessage.Contains("Open the Door"))
        {
            promptMessage = "Close the Door";
            anim.Play(DoorOpenClip);
            openSound.SetActive(true);
            closeSound.SetActive(false);
          
        }
        if(promptMessage.Contains("Close the Door"))
        {
            promptMessage = "Open the Door";
            anim.Play(DoorCloseClip);
            openSound.SetActive(false);
            closeSound.SetActive(true);

        }
    }
}
