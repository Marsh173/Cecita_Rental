using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Locked : InteractableItemWithEvent
{
    public GameObject lockedSounds;
    public bool isLocked;

    void LockedDoor()
    {
        if(isLocked)
        {
            lockedSounds.SetActive(true);
        }
        else
        {
            lockedSounds.SetActive(false);
        }
    }

}
