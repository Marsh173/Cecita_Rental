using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyCat : MonoBehaviour
{
    public float speed;
    public Transform stop, origin;
    public GameObject copycat, player;
    public float startDelay = 0.3f; // Delay before CopyCat starts
    public float exitDelay = 0.3f; // Delay before CopyCat stops

    private Vector3 followpos;
    private Vector3 originalOffset;
    private Vector3 previousPlayerPosition;
    bool trigger, exit, isMoving;
    private bool hasStarted = false;
    private bool hasExited = false;
    private float startTime;
    private float exitTime;


    private bool playerFacingDoor = false;


    private void Start()
    {
        copycat.SetActive(false);
        origin = gameObject.transform;
    }

    private void Update()
    {
        if (trigger && hasStarted)
        {
            copycat.transform.rotation = player.transform.rotation;
            Vector3 playerRotation = player.transform.rotation.eulerAngles;

            // Check if the player's rotation falls within the range: player is 167 degree when facing the door.
            if (playerRotation.y >= 90f && playerRotation.y <= 270f)
            {
                if (!playerFacingDoor) // Check if player is facing the door for the first time
                {
                    Debug.Log("Player is facing the emergency door");
                    // Check for Player position relative to the wall. Copycat turn 180 around and run behind the player.
                    StartCoroutine(TurnAroundAndRunPastPlayer(3f));
                    playerFacingDoor = true; 
                }

                StartCoroutine(TurnAroundAndRunPastPlayer(3f));
                followpos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 3f);
                copycat.transform.position = Vector3.MoveTowards(copycat.transform.position, followpos, speed * Time.deltaTime * 2f);


            }
            else
            {
                if (playerFacingDoor) // Check if player is no longer facing the door
                {
                    Debug.Log("Player is looking at the wall.");
                    
                    playerFacingDoor = false;
                }

                StartCoroutine(TurnAroundAndRunPastPlayer(-3f));
                followpos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 3f);
                copycat.transform.position = Vector3.MoveTowards(copycat.transform.position, followpos, speed * Time.deltaTime * 2f);
               
            }
            
        }
        else if (exit && hasExited)
        {
            copycat.transform.position = Vector3.MoveTowards(copycat.transform.position, origin.position, speed * Time.deltaTime * 2f);
            if (copycat.transform.position == origin.position)
            {
                copycat.SetActive(false);
            }
        }
    }

    private IEnumerator TurnAroundAndRunPastPlayer(float offset)
    {
        yield return new WaitForSeconds(3f);
        // Turn CopyCat around by 180 degrees
        Quaternion targetRotation = Quaternion.LookRotation(-copycat.transform.forward);
        float turnDuration = 0.5f; // Duration for turning around
        float elapsedTime = 0f;

        while (elapsedTime < turnDuration)
        {
            copycat.transform.rotation = Quaternion.Slerp(copycat.transform.rotation, targetRotation, elapsedTime / turnDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Debug.Log("Copycat looks at player.");

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!hasStarted)
            {
                startTime = Time.time;
                hasStarted = true;
                Invoke("ActivateCopyCat", startDelay);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!hasExited)
            {
                exitTime = Time.time;
                hasExited = true;
                Invoke("DeactivateCopyCat", exitDelay);
            }
        }
    }

    private void ActivateCopyCat()
    {
        if (Time.time - startTime >= startDelay)
        {
            trigger = true;
            copycat.SetActive(true);
        }
    }

    private void DeactivateCopyCat()
    {
        if (Time.time - exitTime >= exitDelay)
        {
            trigger = false;
            exit = true;
        }
    }
}
