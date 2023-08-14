using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioStation : Interactable
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
                SceneManager.LoadScene("UITEST");
            }
        }
    }

    protected override void Interact()
    {
        //Debug.Log("Interacted with Player");
        interacted = true;
    }
}
