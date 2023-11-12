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

        


        if(promptMessage.Contains("Open the Door"))
        {
            promptMessage = "Close the Door";
            anim.Play(DoorOpenClip);
            
            openSound.SetActive(true);
            closeSound.SetActive(false);


        }
        else if(promptMessage.Contains("Close the Door"))
        {
            promptMessage = "Open the Door";
            anim.Play(DoorCloseClip);
            openSound.SetActive(false);
            closeSound.SetActive(true);

        }
    }
}
