using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameController : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject inventory;
    public GameObject inventory_manager;
    public bool invent_open;

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
        inventory_manager.SetActive(false);
        pauseMenuUI.SetActive(true);
        FirstPersonAIO.instance.ControllerPause();
        if (inventory.activeSelf)
        {
            invent_open = true;
        }
        else
        {
            invent_open = false;
        }
        inventory.SetActive(false);
        Physics.autoSimulation = false;

        Time.timeScale = 0f; // Freeze the game
    }

    public void ResumeGame()
    {
        inventory_manager.SetActive(true);
        pauseMenuUI.SetActive(false);
        FirstPersonAIO.instance.ControllerPause();
        inventory.SetActive(invent_open);
        
        Physics.autoSimulation = true;

        Time.timeScale = 1f; // Unfreeze the game
    }
}
