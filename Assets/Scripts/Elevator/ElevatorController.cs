using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorController : MonoBehaviour
{
    
    public static List<Scene> loadedScenes = new List<Scene>();
    private int sceneCount;

    //buttons
    [SerializeField]
    public List<GameObject> buttons = new List<GameObject>();

    private bool elevatorWasMoving = false;

    private void Start()
    {
        sceneCount = SceneManager.sceneCount;

        for (int i = 0; i < sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            loadedScenes.Add(scene);

            Debug.Log("Loaded Scene: " + scene.name);
        }
    }

    private void Update()
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
    private void UpdateButtonLayers(bool elevatorIsMoving)
    {
        foreach (GameObject button in buttons)
        {
            button.layer = elevatorIsMoving ? 0 : 7;
        }
    }

    public static void GotoLobby()
    {
        //check if lobby is the current scene
        bool isSceneLoaded = false;
        foreach (Scene scene in loadedScenes)
        {
            if (scene.name == "Lobby")
            {
                isSceneLoaded = true;
                break;
            }
        }

        
        if (!isSceneLoaded)
        {
            foreach (Scene scene in loadedScenes)
            {
                if (scene.name != "General_System_Day" && scene.name != "Elevator")
                {
                    SceneManager.UnloadSceneAsync(scene);
                    Debug.Log("Unloaded Scene: " + scene.name);
                }
            }

            

            SceneManager.LoadScene("Lobby", LoadSceneMode.Additive);

            CoroutineManager.StartStaticCoroutine(UpdateListAfterUnload());
        }
       
    }

    public static void GotoThirdFloor()
    {
        //check if it is the current scene
        //if it is, do nothing. 
        //else: unload then load scene.
        bool isSceneLoaded = false;
        foreach (Scene scene in loadedScenes)
        {
            if (scene.name == "Third_Floor")
            {
                isSceneLoaded = true;
                break;
            }
        }


        if (!isSceneLoaded)
        {
            foreach (Scene scene in loadedScenes)
            {
                if (scene.name != "General_System_Day" && scene.name != "Elevator")
                {
                    SceneManager.UnloadSceneAsync(scene);
                    Debug.Log("Unloaded scene: " + scene.name);
                }
            }

            SceneManager.LoadScene("Third_Floor", LoadSceneMode.Additive);
            SceneManager.LoadScene("Protagonist_Room", LoadSceneMode.Additive);

            // Update the list after loading the scenes
            CoroutineManager.StartStaticCoroutine(UpdateListAfterUnload());
        }
       
    }


    private static IEnumerator UpdateListAfterUnload()
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
            if (InventoryManager.ThirdFloorElevatorCardCollected)
            {
                ChangeButtonMaterial(3); //third floor
                ChangeButtonMaterial(5); //lobby
            }
        }
    }

    private void ChangeButtonMaterial(int buttonIndex)
    {
        Animator anim = buttons[buttonIndex].GetComponent<Animator>();
        anim.SetBool("isBright", true);
    }

   

}
