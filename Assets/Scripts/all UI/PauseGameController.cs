using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGameController : MonoBehaviour
{
    public GameObject pauseMenuUI, inventory, inventory_manager;
    public bool invent_open;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 1f;
        Physics.autoSimulation = true;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    void TogglePauseMenu()
    {
        if (pauseMenuUI.activeSelf)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    void PauseGame()
    {
        Cursor.visible = true;
        inventory_manager.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Freeze the game
        Physics.autoSimulation = false;
        FirstPersonAIO.instance.enableCameraMovement = false;
        FirstPersonAIO.instance.playerCanMove = false;

        if (inventory.activeSelf)
        {
            invent_open = true;
        }
        else
        {
            invent_open = false;
        }

        //inventory.SetActive(false);
        Debug.Log("inventory state: " + invent_open);
    }

    public void ResumeGame()
    {
        Cursor.visible = false;
        inventory_manager.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Unfreeze the game
        Physics.autoSimulation = true;

        if(!InspectionCameraTransition.isInCam && !invent_open)
        {
            FirstPersonAIO.instance.enableCameraMovement = true;
            FirstPersonAIO.instance.playerCanMove = true;
        }
        else if(InspectionCameraTransition.isInCam && invent_open)
        {
            FirstPersonAIO.instance.enableCameraMovement = false;
            FirstPersonAIO.instance.playerCanMove = false;
        }

        //inventory.SetActive(invent_open);

    }
}
