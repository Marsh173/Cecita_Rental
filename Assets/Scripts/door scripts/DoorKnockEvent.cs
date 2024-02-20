using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKnockEvent : MonoBehaviour
{
    
    private GameObject knockAudio;
    [SerializeField] public float TriggerAfterXSeconds = 10f;
    private Door door_script;

    [SerializeField] private float minTimeBetweenKnocks = 10f;
    [SerializeField] private float maxTimeBetweenKnocks = 20f;
    private bool playerInsideTrigger = false;
    public bool playerOpenedDoor = false;

    // Start is called before the first frame update
    void Start()
    {
        EnclosedRoomDetector[] enclosedRooms = FindObjectsOfType<EnclosedRoomDetector>();

        // Subscribe to the event of each enclosed room
        foreach (var room in enclosedRooms)
        {
            room.onPlayerEnteredRoom.AddListener(PlayerEnteredRoomHandler);
        }

        foreach (var room in enclosedRooms)
        {
            room.onPlayerLeaveRoom.AddListener(PlayerLeaveRoomHandler);
        }
            
        GameObject door = this.transform.parent.gameObject.transform.GetChild(0).GetChild(0).gameObject;
        door_script = door.GetComponent<Door>();
        knockAudio = this.transform.GetChild(0).gameObject;
        knockAudio.SetActive(false);

        Respawn.dead = false;
    }


    bool isInRoom = false;
    private void PlayerEnteredRoomHandler(GameObject roomObject)
    {
        if (roomObject.name == "saferoom1" && !isInRoom)
        {
            isInRoom = true;
            Debug.Log("Player has entered saferoom");
            StartCoroutine(Knock());
        }
        else
        {
            isInRoom = false;
        }
    }

    private void PlayerLeaveRoomHandler(GameObject roomObject)
    {
        if (roomObject.name == "saferoom1")
        {
            Debug.Log("player left the room");
            playerInsideTrigger = false;
        }
    }

    public void OpenedDoor()
    {        
        if(door_script.promptMessage == "Close the Door")
        {
            playerOpenedDoor = true;
        }
        else
        {
            playerOpenedDoor = false;
        }
        
    }

    /**
     * If player enters the saferoom:
     *      if he stay there for at least 10 seconds, the event is triggered 
     *      during knock sequence, if player opens the door. He dies 
     */

    /**
     * Randomize between X seconds and X+10 seconds
     * Check if player goes out during safe time 
     * Check if player goes out during knock event 
     */
    IEnumerator Knock()
    {
        playerInsideTrigger = true;

        // The first time it triggers
        yield return new WaitForSeconds(TriggerAfterXSeconds);

        while (playerInsideTrigger)
        {
            Debug.Log("Player is still inside the saferoom. Knock sequence started.");

            // Start knock event 
            knockAudio.SetActive(true);

            float audioLength = 10f; // Assuming audio length of this knock sequence is 15 seconds
            float elapsedTime = 0f;

            
            while (elapsedTime < audioLength)
            {
                if (playerOpenedDoor)
                {
                    Debug.Log("Player open the door on KNOCK.");
                    door_script.ChangeBehavior(); //call door script, open the door. 
                    Respawn.dead = true; //dead
                    break;
                }

                elapsedTime += Time.deltaTime;
                yield return null; 
            }

            Debug.Log("Player has died. Need to reset things");
            playerOpenedDoor = false;  //reset value, player hasn't open door yet 
            knockAudio.SetActive(false); //close the knock sequence
            yield return new WaitForSeconds(1f);
            Respawn.dead = false; //player is alive right now 

            // End of knock event
            //knockAudio.SetActive(false);
            //playerOpenedDoor = false;
            //Respawn.dead = false;

            // Randomize time before the next knock event
            float randomDelay = Random.Range(minTimeBetweenKnocks, maxTimeBetweenKnocks);
            yield return new WaitForSeconds(randomDelay);
        }
    }

}
