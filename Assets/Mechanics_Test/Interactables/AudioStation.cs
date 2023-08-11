using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioStation : Interactable
{
    public GameObject AudioCanvas;
    public bool interacted;

    private void Start()
    {
        interacted = false;
        AudioCanvas.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        //LMB to open up keypad
        if (interacted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                AudioCanvas.SetActive(true);
            }
        }
    }

    protected override void Interact()
    {
        //Debug.Log("Interacted with Player");
        interacted = true;
    }
}
