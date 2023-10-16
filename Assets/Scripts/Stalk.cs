using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalk : MonoBehaviour
{
    public float speed;
    public Transform stop, origin;
    public GameObject stalker;

    bool trigger, exit;

    private void Start()
    {
        stalker.SetActive(false);
    }

    private void Update()
    {
        if (trigger)
        {
            stalker.transform.position = Vector3.MoveTowards(stalker.transform.position, stop.position, speed * Time.deltaTime * 2f);
        }
        else if(exit)
        {
            stalker.transform.position = Vector3.MoveTowards(stalker.transform.position, origin.position, speed * Time.deltaTime * 2f);
            if(stalker.transform.position == origin.position)
            {
                stalker.SetActive(false);
            }
        }

        //Debug.Log(stalker.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trigger = true;
            stalker.SetActive(true);
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
