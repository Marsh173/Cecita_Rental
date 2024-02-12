using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


/***
 * This Script is applied on each individual button presses, refer to ElevatorController to load/unload scenes.
 * Contains open/close door couroutine.
 * Contains elevator moving couroutine. 
 */
public class Elevator : InteractableItemWithEvent
{
    //elevator door
    public GameObject outsideDoor;
    public GameObject insideDoor;
    public Transform openDoorPos;
    public Transform closeDoorPos;
    public TMP_Text monologue;

    //button
    private Animator anim;
    public static bool elevatorIsMoving;
    private string activeButtonName;

    private bool isEnteringElevator = false;

    public ElevatorController elevatorController;

    private void Start()
    {
        anim = this.GetComponent<Animator>();
        elevatorIsMoving = false;
    }


    public void TakeElevator()
    {
        if (InventoryManager.ThirdFloorElevatorCardCollected) //if has a elevator card
        {

            //Play button animation
            anim.SetBool("isPressed", true);

            //Audio: Tenant in 303 is accesing the elevator. 


            //play elevator open sequence
            StartCoroutine(ElevatorArrivalSequence());

            monologue.enabled = false;
            

        }
        else
        {
            //animated text, tells player you don't have an elevator card. 
            monologue.text = "I need to pick up my elevator card first.";
        }

    }

    public void OpenElevator()
    {
        anim.SetBool("isPressed", true);

        //audio: elevator door opening

        //play elevator open sequence
        StartCoroutine(ElevatorDoorOpenSequence(outsideDoor, closeDoorPos.position.z, false));
        StartCoroutine(ElevatorDoorOpenSequence(insideDoor, closeDoorPos.position.z, true));
    }

    public void CloseElevator()
    {
        anim.SetBool("isPressed", true);

        //audio: elevator door is closing

        //play elevator close sequence
        StartCoroutine(ElevatorDoorCloseSequence(outsideDoor, openDoorPos.position.z, false));
        StartCoroutine(ElevatorDoorCloseSequence(insideDoor, openDoorPos.position.z, true));
    }

    public IEnumerator ElevatorArrivalSequence()
    {

        StartCoroutine(elevatorController.ElevatorArrive());

        while (!elevatorController.FinishArrival())
        {
            yield return null;
        }


        isEnteringElevator = true; //when arrived, player is entering the elevator

        //elevator door opens

        StartCoroutine(ElevatorDoorOpenSequence(outsideDoor, closeDoorPos.position.z, false));
        StartCoroutine(ElevatorDoorOpenSequence(insideDoor, closeDoorPos.position.z, true));
        
        
    }

    IEnumerator ElevatorDoorOpenSequence(GameObject door, float target, bool inside)
    {
        //play elevator open audio


        //Start open sequence
        float slowSpeed = (inside)? 0.2f : 1.5f;
        float fastSpeed = (inside) ? 1.9f: 1.5f;
        float progress = 0f;

        while (door.transform.position.z > target)
        {
            float t = Mathf.Clamp01(progress);
            float speed = Mathf.Lerp(slowSpeed, fastSpeed, BezierCurve(t, 0.0f, 0.3f, 0.7f, 1.0f));

            door.transform.position = Vector3.MoveTowards(door.transform.position, new Vector3(door.transform.position.x, door.transform.position.y, target), Time.deltaTime * speed);

            progress += Time.deltaTime;

            yield return null;
        }

        //reset button to be original color
        anim.SetBool("isPressed", false);

        // close the door automatically after opening
        if (isEnteringElevator) //player pressed button, wait for elevator to arrive. 
        {
            //yield return new WaitForSeconds(2f);

            while (!elevatorController.isInsideElevator)
            {
                yield return new WaitForSeconds(1f);
            }

            Debug.Log("Player inside, close door now");
            Debug.Log("Am I inside?" + elevatorController.isInsideElevator);
            yield return new WaitForSeconds(1f);
            StartCoroutine(ElevatorDoorCloseSequence(outsideDoor, openDoorPos.position.z, false));
            StartCoroutine(ElevatorDoorCloseSequence(insideDoor, openDoorPos.position.z, true));

        }
        else //player took the elevator, reach destination
        {
            while(elevatorController.isInsideElevator)
            {
                yield return new WaitForSeconds(1f);
            }

            Debug.Log("Player outside, close door now");
            Debug.Log("Exit: am I still inside?" + elevatorController.isInsideElevator);
            yield return new WaitForSeconds(1f);
            StartCoroutine(ElevatorDoorCloseSequence(outsideDoor, openDoorPos.position.z, false));
            StartCoroutine(ElevatorDoorCloseSequence(insideDoor, openDoorPos.position.z, true));

        }

       
    }

    IEnumerator ElevatorDoorCloseSequence(GameObject door, float target, bool inside)
    {
        //play elevator close audio

        float slowSpeed = (inside) ? 1.5f : 0.2f;
        float fastSpeed = (inside) ? 1.5f : 1.9f;
        float progress = 0f;

        while (door.transform.position.z < target)
        {
            float t = Mathf.Clamp01(progress);
            float speed = Mathf.Lerp(slowSpeed, fastSpeed, BezierCurve(t, 0.0f, 0.3f, 0.7f, 1.0f));

            door.transform.position = Vector3.MoveTowards(door.transform.position, new Vector3(door.transform.position.x, door.transform.position.y, target), Time.deltaTime * speed);

            progress += Time.deltaTime;

            yield return null;
        }

        //reset button to be original color
        anim.SetBool("isPressed", false);

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


    public void HelpButton()
    {
        //audio: no help is available at this moment

        StartCoroutine(buttonPressReturnNormal());
        Debug.Log("Help button clicked");

    }


    public void Lobby()
    {
        //as long as we have this elevator card, we can go to the Lobby
        if (InventoryManager.ThirdFloorElevatorCardCollected)
        {
            anim.SetBool("isBright", false);
            anim.SetBool("isPressed", true);
            Debug.Log("Go to Lobby");

            activeButtonName = "lobby";

            StartCoroutine(ElevatorMoving());
            

        }
        else
        {
            UnavailableButtons();
        }
    }

    public void SecondFloor()
    {
        UnavailableButtons();
        Debug.Log("SecondFloor");
    }

    public void ThirdFloor()
    {
        if (InventoryManager.ThirdFloorElevatorCardCollected)
        {
            anim.SetBool("isBright", false);
            anim.SetBool("isPressed", true);
            Debug.Log("Go to Third Floor");

            activeButtonName = "Third_Floor";

            StartCoroutine(ElevatorMoving());
        }
        else
        {
            UnavailableButtons();
        }
    }
    
    public void FourthFloor()
    {
        UnavailableButtons();
        Debug.Log("FourthFloor");
    }

    public void FifthFloor()
    {
        UnavailableButtons();
        Debug.Log("FifthFloor");
    }

    public void SixthFloor()
    {
        UnavailableButtons();
        Debug.Log("SixthFloor");
    }

    public void UnavailableButtons()
    {
        //audio: this button is unaccessible.
        StartCoroutine(buttonPressReturnUnavailable());
        Debug.Log("unavailable button clicked");
    }


    IEnumerator buttonPressReturnNormal()
    {
        anim.SetBool("isPressed", true);
        yield return new WaitForSeconds(1.0f);
        anim.SetBool("isPressed", false);
    }

    IEnumerator buttonPressReturnUnavailable()
    {
        anim.SetBool("isUnavailable", true);
        yield return new WaitForSeconds(3.0f);
        anim.SetBool("isUnavailable", false);
    }

    IEnumerator ElevatorMoving()
    {
        switch (activeButtonName)
        {
            case "lobby":
                Debug.Log("Going to Lobby");
                elevatorController.GotoLobby();
                
                break;

            case "Third_Floor":
                Debug.Log("Going to Third Floor");
                elevatorController.GotoThirdFloor();
                break;
            default:
                break;
        }


        while (!elevatorController.FinishMoving())
        {
            yield return null;
        }


        StartCoroutine(ElevatorDoorOpenSequence(outsideDoor, closeDoorPos.position.z, false));
        StartCoroutine(ElevatorDoorOpenSequence(insideDoor, closeDoorPos.position.z, true));

        anim.SetBool("isPressed", false);

        isEnteringElevator = false; //after moving finished, player will exist the elevator

    }
}
