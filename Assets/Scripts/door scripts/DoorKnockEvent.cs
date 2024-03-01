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

    public GameObject knockPerson;
    public Transform startPos;
    public Transform endPos;

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
        knockPerson.SetActive(false);

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
    IEnumerator Knock() //This couroutine actual means "things happen after enter the room"
    {
        playerInsideTrigger = true;

        // The first time it triggers
        yield return new WaitForSeconds(TriggerAfterXSeconds);

        while (playerInsideTrigger)
        {
            Respawn.dead = false;
            playerOpenedDoor = false;

            Debug.Log("Player is still inside the saferoom. Triggers RULE 4");

            if(door_script.promptMessage == "Close the Door")
            {
                Debug.Log("door is already open");
                yield return new WaitForSeconds(3f);

                Debug.Log("Monster appears.");
                //monster appear on the corner, start moving in
                knockPerson.SetActive(true);
                yield return StartCoroutine(MoveMonster(startPos, endPos, 6f));

                if (door_script.promptMessage == "Open the Door")
                {
                    Debug.Log("Player closed the door in time, monster diidn't get in.");
                    knockPerson.SetActive(false);
                    yield return new WaitForSeconds(2f);
                    continue;
                }
                else
                {
                    Debug.Log("Player didn't close the door when monster gets close.");
                    Respawn.dead = true; // Dead
                    knockPerson.SetActive(false);
                    yield return new WaitForSeconds(2f);
                    continue;
                }
            }

            else
            {
                Debug.Log("door is not open, start knocking.");

                // Start knock event 
                knockAudio.SetActive(true);

                knockPerson.transform.position = endPos.position;
                knockPerson.transform.rotation = endPos.rotation;
                knockPerson.SetActive(true);

                float audioLength = 10f; // Assuming audio length of this knock sequence is 10 seconds
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

                Debug.Log("Reset knock related things.");
                playerOpenedDoor = false;  //reset value, player hasn't open door yet 
                knockAudio.SetActive(false); //close the knock sequence
                knockPerson.SetActive(false);

                yield return new WaitForSeconds(1f);
                Respawn.dead = false; //player is alive right now 
                


                // Randomize time before the next knock event
                float randomDelay = Random.Range(minTimeBetweenKnocks, maxTimeBetweenKnocks);
                yield return new WaitForSeconds(randomDelay);
            }

        }

        Debug.Log("Player is out of the trigger");

    }


    IEnumerator MoveMonster(Transform start, Transform end, float moveDuration)
    {

        if (playerInsideTrigger)
        {
            Vector3 startPos = start.position;
            Vector3 endPos = end.position;
            Quaternion startRot = start.rotation;
            Quaternion endRot = end.rotation;

            float elapsedTime = 0f;
            while (elapsedTime < moveDuration)
            {
                if (Respawn.dead) //if player is dead in the process
                {
                    break;
                }

                float t = elapsedTime / moveDuration;
                knockPerson.transform.position = Vector3.Lerp(startPos, endPos, t);
                knockPerson.transform.rotation = Quaternion.Lerp(startRot, endRot, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            knockPerson.SetActive(!Respawn.dead); //if player is dead, disable Monster; else, monster at end pos.

            knockPerson.transform.position = endPos;
            knockPerson.transform.rotation = endRot;
        }
        else
        {
            knockPerson.SetActive(false);
        }
        
    }
}
