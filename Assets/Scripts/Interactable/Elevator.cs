using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : InteractableItemWithEvent
{
    public GameObject outsideDoor;
    public GameObject insideDoor;
    public Transform openDoorPos;
    public Transform closeDoorPos;
    public static bool UseThirdFloorElevator = false;
    private Animator anim;

    private void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    public void OpenElevator()
    {
        if (InventoryManager.ThirdFloorElevatorCardCollected) //if has a elevator card
        {

            //Play button animation
            anim.SetBool("isPressed", true);

            //play elevator open sequence
            StartCoroutine(ElevatorDoorOpenSequence(outsideDoor, closeDoorPos.position.z, false));
            StartCoroutine(ElevatorDoorOpenSequence(insideDoor, closeDoorPos.position.z, true));

           
            UseThirdFloorElevator = true; //for inside elevator functions.
        }
        else
        {
            //animated text, tells player you don't have an elevator card. 
            promptMessage = "Do you have the elevator card?";
        }
    }

    IEnumerator ElevatorDoorOpenSequence(GameObject door, float target, bool inside)
    {
        //play elevator sound, going up or down then open
        yield return new WaitForSeconds(1.0f); 


        //Start open sequence
        float slowSpeed = (inside)? 0.2f : 1.5f;
        float fastSpeed = (inside) ? 1.9f: 1.5f;
        float progress = 0f;

        while (door.transform.position.z > target)
        {
            // Use a cubic Bezier curve to interpolate speed
            float t = Mathf.Clamp01(progress);
            float speed = Mathf.Lerp(slowSpeed, fastSpeed, BezierCurve(t, 0.0f, 0.3f, 0.7f, 1.0f));

            door.transform.position = Vector3.MoveTowards(door.transform.position, new Vector3(door.transform.position.x, door.transform.position.y, target), Time.deltaTime * speed);

            // Increase progress over time
            progress += Time.deltaTime;

            yield return null;
        }

        //reset button to be original color
        anim.SetBool("isPressed", false);

        //close the door
        StartCoroutine(ElevatorDoorCloseSequence(door, openDoorPos.position.z, inside));
       
    }

    IEnumerator ElevatorDoorCloseSequence(GameObject door, float target, bool inside)
    {
        //wait several seconds before door closes
        yield return new WaitForSeconds(3.0f);


        float slowSpeed = (inside) ? 1.5f : 0.2f;
        float fastSpeed = (inside) ? 1.5f : 1.9f;
        float progress = 0f;

        while (door.transform.position.z < target)
        {
            // Use a cubic Bezier curve to interpolate speed
            float t = Mathf.Clamp01(progress);
            float speed = Mathf.Lerp(slowSpeed, fastSpeed, BezierCurve(t, 0.0f, 0.3f, 0.7f, 1.0f));

            door.transform.position = Vector3.MoveTowards(door.transform.position, new Vector3(door.transform.position.x, door.transform.position.y, target), Time.deltaTime * speed);

            // Increase progress over time
            progress += Time.deltaTime;

            yield return null;
        }



    }

    float BezierCurve(float t, float p0, float p1, float p2, float p3)
    {
        float u = 1.0f - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        float p = uuu * p0 + 3.0f * uu * t * p1 + 3.0f * u * tt * p2 + ttt * p3;

        return p;
    }
}
