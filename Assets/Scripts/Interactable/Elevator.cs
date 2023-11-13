using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : InteractableItemWithEvent
{
    public GameObject elevatordoor;
    public static bool UseThirdFloorElevator = false;


    public void UseElevator()
    {
        if (InventoryManager.ThirdFloorElevatorCardCollected)
        {
            //Play player use card animation
            //play elevator sound, going up or down then open
            //play elevator open sequence
            elevatordoor.SetActive(false);
            UseThirdFloorElevator = true;
        }
        else
        {
            //Tell player you don't have the card
            promptMessage = "Do you have the elevator card?";
        }
    }
}
