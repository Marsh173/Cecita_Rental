using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class EnclosedRoomDetector : MonoBehaviour
{
    // Create a custom event for when the player enters the room
    public PlayerEnteredRoomEvent onPlayerEnteredRoom = new PlayerEnteredRoomEvent();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Trigger the custom event when player enters the room
            Debug.Log("In enclosed space!");
            onPlayerEnteredRoom.Invoke(gameObject);
        }
    }
}
