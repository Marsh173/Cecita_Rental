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

        if(promptMessage == "Open the Door")
        {
            promptMessage = "Close the Door";
            anim.SetBool("Closed", true);
            anim.SetBool("Opened", false);
            //anim.Play(DoorOpenClip);

            openSound.SetActive(true);
            closeSound.SetActive(false);

            Debug.Log("opened" + anim.GetBool("Opened"));
            Debug.Log("close" + anim.GetBool("Closed"));
        }
        else if(promptMessage == "Close the Door")
        {
            promptMessage = "Open the Door";
            anim.SetBool("Closed", false);
            anim.SetBool("Opened", true);
            //anim.Play(DoorCloseClip);

            openSound.SetActive(false);
            closeSound.SetActive(true);

        }
    }
}
