using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkers : MonoBehaviour
{
    //AI patrol 
    public Transform[] patrolPoints;
    int currentPointIndex = 0;

    //
    public float speed;
    public Transform stop;
    public GameObject walker;
    bool stopMoving = false;


    //[Header("Audio Detection")]
    //public GameObject monster_sound;
    //private GameObject playerObj;
    //[SerializeField] private Vector3 initial_pos = new Vector3(0, 0, 0);
    //[SerializeField] private float close_distance = 2.0f;
    //[SerializeField] private float sound_speed = 3.0f;

    bool trigger;

    private void Start()
    {
        walker.SetActive(false);
        //monster_sound.SetActive(false);
        //playerObj = GameObject.FindGameObjectWithTag("Player");

        //monster_sound.transform.localPosition = initial_pos;
        //Debug.Log("Initial: " + initial_pos);
        //Debug.Log(monster_sound.transform.localPosition);

        

    }

    private void Update()
    {
        //MovingSoundPosition();

        if (trigger)
        {
            if (walker.transform.position != patrolPoints[currentPointIndex].position)
            {
                walker.transform.position = Vector3.MoveTowards(walker.transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime * 2f);
                Vector3 direction = patrolPoints[currentPointIndex].position - walker.transform.position;
                walker.transform.forward = Vector3.Lerp(walker.transform.forward, direction, 0.08f);
            }
            else
            {
                currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
            }
        }



        if(walker.transform.position == stop.position)
        {
            Debug.Log("stopped");
            //monster_sound.SetActive(false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            trigger = true;
           // monster_sound.SetActive(true);
            walker.SetActive(true);
            //walker.transform.position = Vector3.MoveTowards(walker.transform.position, stop.position, speed * Time.deltaTime * 2f);
        }

    }
/*
    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Monster"))
        {
            stopMoving = true;
            //walker.SetActive(false);
        }
    }*/


    //private void MovingSoundPosition()
    //{
    //    /*  Idea: detect the distance between monster and player
    //        move closer to monster itself when player is closer within a certain distance

    //        I'm going to use Vector3.distance for now, if there will be a case of corner or adjacent hallways, let's consider raycasting.
    //    */

    //    Vector3 player_loc = playerObj.transform.position;
    //    Vector3 monster_loc = walker.transform.position;

    //    //Debug.Log("player" + player_loc);
    //    //Debug.Log("monster" + monster_loc);

    //    float distance = (monster_loc - player_loc).magnitude;

    //    //Debug.Log(distance);

    //    if(distance <= close_distance)
    //    {
    //        monster_sound.transform.localPosition = Vector3.MoveTowards(monster_sound.transform.localPosition, Vector3.zero, sound_speed);
    //    }


    //}
}
