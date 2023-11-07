using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Locked : InteractableItemWithEvent
{
    public GameObject lockedSounds;
    private void Update()
    {
        if (interacted )
        {
            if( Input.GetMouseButtonDown(0)) lockedSounds.SetActive(true);

        }
        else
        {
            lockedSounds.SetActive(false);
        }
    }
}
