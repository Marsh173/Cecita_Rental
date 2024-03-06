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
                Debug.Log("player is facing the emergency door");
                //Copycat goes near the opposite wall. run past player until position is set.
                followpos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 4f);
                copycat.transform.position = Vector3.MoveTowards(copycat.transform.position, followpos, speed * Time.deltaTime * 2f);

            }
            else
            {
                Debug.Log("player is looking at the wall.");
                followpos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 4f);
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
