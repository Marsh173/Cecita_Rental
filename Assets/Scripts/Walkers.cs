using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkers : MonoBehaviour
{
    //AI patrol 
    public Transform[] patrolPoints;
    int currentPointIndex = 0;


    public float speed;
    public Transform stop;
    public GameObject walker;
    public Vector3 initialPosition;
    bool stopMoving = false;

    public Respawn respawn;

    public bool stopMusic = false;

    bool trigger;
    public bool stalker;

    private void Awake()
    {
        respawn = FindObjectOfType<Respawn>();
    }

    private void Start()
    {
        walker.SetActive(false);
        initialPosition = walker.transform.position;
 
    }

    private void Update()
    {
        if (Respawn.dead)
        {
            trigger = false;
            walker.transform.position = initialPosition;
            stopMusic = true;
        }

        if (trigger && !stalker)
        {
            stopMusic = false;

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
        else if (trigger && stalker)
        {
            walker.transform.position = Vector3.MoveTowards(walker.transform.position, stop.position, speed * Time.deltaTime * 2f);
   
        }

        float threshold = 0.005f;

        if (patrolPoints.Length != 0)
        {
            
            if (Vector3.Distance(walker.transform.position, stop.position) < threshold || walker.transform.position == patrolPoints[currentPointIndex].position)
            {
                Debug.Log("stopped");
                stopMusic = true;
                //walker.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            trigger = true;
           
            walker.SetActive(true);

        }

        if (other.CompareTag("Monster Destroyer"))
        {
            stopMoving = true;
            walker.SetActive(false);

        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (stalker) 
        {
            if (other.CompareTag("Monster"))
            {
                stopMoving = true;
                walker.SetActive(false);
            }
        }
    }


    
}
