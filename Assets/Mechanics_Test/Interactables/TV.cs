using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : Interactable
{
    public GameObject recorder;
    public bool interacted;

    private void Start()
    {
        interacted = false;
        recorder.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        //LMB to open up keypad
        if (interacted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                recorder.SetActive(true);
            }
        }
    }

    protected override void Interact()
    {
        //Debug.Log("Interacted with Player");
        interacted = true;
    }
}
