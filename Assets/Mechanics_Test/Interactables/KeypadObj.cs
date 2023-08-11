using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadObj : Interactable
{
    public GameObject keypadCanvas;
    

    // Update is called once per frame
    void Update()
    {
        //LMB to open up keypad
    }

    protected override void Interact()
    {
        Debug.Log("Interacted with Player");
    }
}
