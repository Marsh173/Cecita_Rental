using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadObj : Interactable
{
    public GameObject keypadCanvas;
    public bool interacted;

    private void Start()
    {
        interacted = false;
        keypadCanvas.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        //LMB to open up keypad
        if (interacted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                keypadCanvas.SetActive(true);
            }
            interacted = false;
        }
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
