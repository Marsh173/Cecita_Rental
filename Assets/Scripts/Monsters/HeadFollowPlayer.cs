using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadFollowPlayer : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 180f;
    public float headHeightOffset = 1.5f;

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            

            Vector3 directionToPlayer = (player.position + Vector3.up * headHeightOffset) - transform.position;

            // Calculate the target rotation to look at the player
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);

            // Smoothly rotate the head towards the player's direction
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
