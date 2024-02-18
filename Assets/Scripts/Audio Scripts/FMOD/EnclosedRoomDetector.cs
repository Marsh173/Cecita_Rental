using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class EnclosedRoomDetector : MonoBehaviour
{
    // Create a custom event for when the player enters the room
    public PlayerEnteredRoomEvent onPlayerEnteredRoom = new PlayerEnteredRoomEvent();
    public PlayerLeaveRoomEvent onPlayerLeaveRoom = new PlayerLeaveRoomEvent();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Trigger the custom event when player enters the room
            //Debug.Log("In enclosed space!");
            onPlayerEnteredRoom.Invoke(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Trigger the custom event when player enters the room
            onPlayerLeaveRoom.Invoke(gameObject);
        }
    }
}
