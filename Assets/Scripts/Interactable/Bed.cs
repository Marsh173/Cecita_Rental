using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bed : Interactable
{
    public bool interacted;

    private void Start()
    {
        interacted = false;
    }


    // Update is called once per frame
    void Update()
    {
        //LMB to open up keypad
        if (interacted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GoToSleep();
                interacted = false;
            }
        }
    }

    private void GoToSleep()
    {
        Debug.Log("Sleep");
        SceneManager.LoadScene("Day_General_System");

    }

    protected override void Interact()
    {
        //Debug.Log("Interacted with Player");
        interacted = true;
    }

    protected override void DisableInteract()
    {
        //Debug.Log("Interacted with Player");
        interacted = false;
    }
}
