using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkers : MonoBehaviour
{
    public float speed;
    public Transform stop;
    public GameObject walker;

    bool trigger;

    private void Update()
    {
        if(trigger)
        {
            walker.transform.position = Vector3.MoveTowards(walker.transform.position, stop.position, speed * Time.deltaTime * 2f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trigger = true;
            //walker.transform.position = Vector3.MoveTowards(walker.transform.position, stop.position, speed * Time.deltaTime * 2f);
        }
    }
}
