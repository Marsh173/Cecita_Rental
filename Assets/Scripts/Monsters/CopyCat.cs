using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyCat : MonoBehaviour
{
    public float speed;
    public Transform stop, origin;
    public GameObject copycat, player;
    private Vector3 followpos;
    bool trigger, exit;

    private void Start()
    {
        copycat.SetActive(false);
        origin = gameObject.transform;
    }

    private void Update()
    {

        followpos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 4f);

        if (trigger)
        {
            copycat.transform.position = Vector3.MoveTowards(copycat.transform.position, followpos, speed * Time.deltaTime * 2f);
        }
        else if (exit)
        {
            copycat.transform.position = Vector3.MoveTowards(copycat.transform.position, origin.position, speed * Time.deltaTime * 2f);
            if (copycat.transform.position == origin.position)
            {
                copycat.SetActive(false);
            }
        }

        //Debug.Log(stalker.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trigger = true;
            copycat.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trigger = false;
            exit = true;
        }
    }
}
