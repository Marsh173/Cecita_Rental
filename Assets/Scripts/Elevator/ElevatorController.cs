using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

/***
 * This Script stores scene load and button functions related to scene loading.
 * Elevator script will use this script to control its buttons.
 */
public class ElevatorController : MonoBehaviour
{
    
    public List<Scene> loadedScenes = new List<Scene>();
    private int sceneCount;

    //buttons
    [SerializeField]
    public List<GameObject> buttons = new List<GameObject>();

    private bool elevatorWasMoving = false;

    [Header("Inner Elevator Floor Indicator")]
    private TextMeshProUGUI inner_floor_indicator;
    private Image inner_down_sign;
    private Image inner_up_sign;
    private bool finishMoving = false;

    private GameObject down_button;
    private GameObject up_button;


    [Header("Outer Elevator Floor Indicator")]
    private Image outer_down_sign;
    private Image outer_up_sign;
    private bool finishArrival = false;


    public bool isInsideElevator = false;

    public int currentFloor = 3;
    public int targetFloor = 1;


    private void Start()
    {
        sceneCount = SceneManager.sceneCount;

        for (int i = 0; i < sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            loadedScenes.Add(scene);

            Debug.Log("Loaded Scene: " + scene.name);
        }

        setFloorIndicator_inner();
        setFloorIndicator_outer();
        setDownUpButtons();

    }

    private void setFloorIndicator_inner()
    {
        Transform ElevatorCanvasTransform = this.transform.GetChild(0);

        //get the floor num 
        Transform floorNumTransform = ElevatorCanvasTransform.GetChild(0);


        GameObject floorNumObj = floorNumTransform.gameObject;

        inner_floor_indicator = floorNumObj.GetComponent<TextMeshProUGUI>();
        inner_floor_indicator.text = "";

        //get the two arrow images
        Transform arrowDownTransform = floorNumTransform.GetChild(0);
        GameObject downObj = arrowDownTransform.gameObject;

        inner_down_sign = downObj.GetComponent<Image>();
        inner_down_sign.enabled = false;

        Transform arrowUpTransform = floorNumTransform.GetChild(1);
        GameObject upObj = arrowUpTransform.gameObject;

        inner_up_sign = upObj.GetComponent<Image>();
        inner_up_sign.enabled = false;
    }


    private void setFloorIndicator_outer()
    {
        Transform ElevatorCanvasTransform = this.transform.GetChild(0);

        //get the floor num 
        Transform floorNumTransform = ElevatorCanvasTransform.GetChild(1);
        GameObject floorNumObj = floorNumTransform.gameObject;

        //get the two arrow images
        Transform arrowDownTransform = floorNumTransform.GetChild(2);
        GameObject downObj = arrowDownTransform.gameObject;

        outer_down_sign = downObj.GetComponent<Image>();
        outer_down_sign.enabled = false;

        Transform arrowUpTransform = floorNumTransform.GetChild(3);
        GameObject upObj = arrowUpTransform.gameObject;

        outer_up_sign = upObj.GetComponent<Image>();
        outer_up_sign.enabled = false;
    }


    public void setDownUpButtons()
    {
        Transform down_transform = this.transform.GetChild(1);
        down_button = down_transform.gameObject;

        Transform up_transform = this.transform.GetChild(2);
        up_button = up_transform.gameObject;
    }

    public void Update()
    {
        bool elevatorIsMovingNow = Elevator.elevatorIsMoving;

        if (elevatorIsMovingNow != elevatorWasMoving)
        {
            // Elevator state has changed, update button layers
            UpdateButtonLayers(elevatorIsMovingNow);

            // Remember the current elevator state for the next frame
            elevatorWasMoving = elevatorIsMovingNow;
        }
    }
    public void UpdateButtonLayers(bool elevatorIsMoving)
    {
        foreach (GameObject button in buttons)
        {
            button.layer = elevatorIsMoving ? 0 : 7;
        }
    }

    public void GotoLobby()
    {
        targetFloor = 1;

        //check if lobby is the current scene
        bool isSceneLoaded = false;
        foreach (Scene scene in loadedScenes)
        {
            switch (scene.name)
            {
                case "lobby":
                    isSceneLoaded = true;
                    currentFloor = 1;
                    
                    break;

                case "Third_Floor":
                    currentFloor = 3;
                    break;

                default:
                    break;
            }

            if (isSceneLoaded)
            {
                break;
            }
        }


        //elevator moving sequence
        StartCoroutine(ElevatorMove(currentFloor, targetFloor));

        if (!isSceneLoaded)
        {
            StartCoroutine(UpdateSceneListAndLoadLobby()); 
        }
    }

    public IEnumerator UpdateSceneListAndLoadLobby()
    {
        // Unload scenes asynchronously
        foreach (Scene scene in loadedScenes)
        {
            if (scene.name != "Day_General_System" && scene.name != "Elevator")
            {
                yield return SceneManager.UnloadSceneAsync(scene);
                Debug.Log("Unloaded Scene: " + scene.name);
            }
        }

        // Load lobby scene additively asynchronously
        yield return SceneManager.LoadSceneAsync("Lobby", LoadSceneMode.Additive);
        currentFloor = targetFloor;

        // Start another coroutine for additional tasks after unloading and loading scenes
        StartCoroutine(UpdateListAfterUnload());
    }

    public void GotoThirdFloor()
    {
        //check if it is the current scene
        //if it is, do nothing. 
        //else: unload then load scene.

        targetFloor = 3;
        
        bool isSceneLoaded = false;
        foreach (Scene scene in loadedScenes)
        {
            switch (scene.name)
            {
                case "lobby":
                    currentFloor = 1;
                    break;

                case "Third_Floor":
                    currentFloor = 3;
                    isSceneLoaded = true;
                    break;

                default:
                    break;
            }

            if (isSceneLoaded)
            {
                break;
            }
        }

        //elevator moving sequence
        StartCoroutine(ElevatorMove(currentFloor, targetFloor));


        if (!isSceneLoaded)
        {
            foreach (Scene scene in loadedScenes)
            {
                if (scene.name != "Day_General_System" && scene.name != "Elevator")
                {
                    SceneManager.UnloadSceneAsync(scene);
                    Debug.Log("Unloaded scene: " + scene.name);
                }
            }


            SceneManager.LoadScene("Third_Floor", LoadSceneMode.Additive);
            SceneManager.LoadScene("Protagonist_Room", LoadSceneMode.Additive);

            currentFloor = targetFloor;

            // Update the list after loading the scenes
            StartCoroutine(UpdateListAfterUnload());
        }
       
    }


    public IEnumerator UpdateListAfterUnload()
    {
        // Wait for the next frame
        yield return null;

        // Clear the loadedScenes list
        loadedScenes.Clear();

        // Add the currently loaded scenes to the list
        int count = SceneManager.sceneCount;
        for (int i = 0; i < count; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            loadedScenes.Add(scene);

            Debug.Log("Update SceneList: " + scene.name);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the elevator");
            isInsideElevator = true;

            if (InventoryManager.ThirdFloorElevatorCardCollected)
            {
                ChangeButtonMaterial(3); //third floor
                ChangeButtonMaterial(5); //lobby
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player existed the elevator");
            isInsideElevator = false;
        }

        StartCoroutine(ChangeTargetFloor());
    }

    public IEnumerator ChangeTargetFloor()
    {
        yield return new WaitForSeconds(Random.Range(3, 8));
        targetFloor = Random.Range(1, 7);
    }

    private void ChangeButtonMaterial(int buttonIndex)
    {
        Animator anim = buttons[buttonIndex].GetComponent<Animator>();
        anim.SetBool("isBright", true);
    }

    public IEnumerator ElevatorMove(int start, int dest)
    {
        finishMoving = false;

        //moving sound

        //camera shake

        //get the current floor, get the destination floor
        if (start == dest)
        {
            Debug.Log("Same Floor");
            yield return null;
        }
        else if (start > dest)
        {
            Debug.Log("Going down");

            inner_floor_indicator.text = start.ToString();
            inner_down_sign.enabled = true;

            //change arrow button
            Animator anim = down_button.gameObject.GetComponent<Animator>();
            anim.SetBool("isBright", true);

            while (start > dest)
            {
                yield return new WaitForSeconds(1.0f);
                start--;
                inner_floor_indicator.text = start.ToString();
            }


            yield return new WaitForSeconds(3.0f);
            inner_down_sign.enabled = false;
            inner_floor_indicator.text = "";
            anim.SetBool("isBright", false);
        }
        else
        {
            Debug.Log("Going up");

            inner_floor_indicator.text = start.ToString();
            inner_up_sign.enabled = true;

            //change arrow button
            Animator anim = up_button.gameObject.GetComponent<Animator>();
            anim.SetBool("isBright", true);

            while (start < dest)
            {
                yield return new WaitForSeconds(1.0f);
                start++;
                inner_floor_indicator.text = start.ToString();
            }

            yield return new WaitForSeconds(3.0f);
            inner_up_sign.enabled = false;
            inner_floor_indicator.text = "";
            anim.SetBool("isBright", false);
        }

        finishMoving = true;
        Debug.Log("has finished moving? " + finishMoving);

        yield return new WaitForSeconds(1f);

    }


    public IEnumerator ElevatorArrive()
    {
        finishArrival = false;

        //elevator arrive sound

        //get the current floor
        Debug.Log("The current target floor?" + currentFloor + " or " + targetFloor);


        int dest = 0;
        bool finishCheck = false;
        foreach (Scene scene in loadedScenes)
        {
            switch (scene.name)
            {
                case "lobby":
                    dest = 1;
                    finishCheck = true;
                    break;

                case "Third_Floor":
                    dest = 3;
                    finishCheck = true;
                    break;

                default:
                    break;
            }

            if (finishCheck)
            {
                break;
            }
        }


        //get a random number from 1 - 6, this is the elevator floor
        int start = 0;
        if(currentFloor == targetFloor)
        {
            start = currentFloor;
        }
        else
        {
            start = Random.Range(1, 7);
        }
        
        Debug.Log("Elevator is coming from floor " + start);

        //get the current floor, get the destination floor
        if (start == dest)
        {
            Debug.Log("Same Floor");
            yield return null;
        }
        else if (start > dest)
        {
            Debug.Log("Coming down");
            outer_down_sign.enabled = true;

            while (start > dest)
            {
                yield return new WaitForSeconds(1.0f);
                start--;
                
            }

            yield return new WaitForSeconds(1.0f);
            outer_down_sign.enabled = false;
        }
        else
        {
            Debug.Log("Coming up");
            outer_up_sign.enabled = true;

            while (start < dest)
            {
                yield return new WaitForSeconds(1.0f);
                start++;
              
            }

            
            yield return new WaitForSeconds(1.0f);
            outer_up_sign.enabled = false;

        }


        finishArrival = true;
    }


    public bool FinishArrival()
    {
        return finishArrival;
    }

    public bool FinishMoving()
    {
        return finishMoving;
    }

}
