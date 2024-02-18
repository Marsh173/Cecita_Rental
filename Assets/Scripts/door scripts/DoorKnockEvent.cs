using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKnockEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject knockAudio;
    public float TriggerAfterXSeconds = 10f;
    private Door door_script;

    [SerializeField] private float minTimeBetweenKnocks = 5f;
    [SerializeField] private float maxTimeBetweenKnocks = 15f;
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
    }


    private void PlayerEnteredRoomHandler(GameObject roomObject)
    {
        if (roomObject.name == "saferoom1")
        {
            StartCoroutine(Knock());

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
            Debug.Log("Knock event triggered.");

            // Start knock event 
            knockAudio.SetActive(true);

            float audioLength = 15f; // Assuming audio length of this knock sequence is 15 seconds
            float elapsedTime = 0f;

            
            while (elapsedTime < audioLength)
            {
               
                if (playerOpenedDoor)
                {
                    Debug.Log("Player disobeyed the rules.");
                    Respawn.dead = true;
                    door_script.ChangeBehavior();

                    playerOpenedDoor = false; 
                    knockAudio.SetActive(false); 
                    yield break; 
                }

                elapsedTime += Time.deltaTime;
                yield return null; 
            }

            // End of knock event
            knockAudio.SetActive(false);
            playerOpenedDoor = false;

            // Randomize time before the next knock event
            float randomDelay = Random.Range(minTimeBetweenKnocks, maxTimeBetweenKnocks);
            yield return new WaitForSeconds(randomDelay);
        }
    }


}
