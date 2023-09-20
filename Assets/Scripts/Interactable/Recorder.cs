using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder : Interactable
{
    public bool interacted;
    public NormalItems NItem;

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
                PickupMe();
                interacted = false;
            }
        }
    }

    private void PickupMe()
    {
        Debug.Log("collected");
        InventoryManager.Instance.AddNormal(NItem);
        //playlistManager.Instance.Add(NItem);
        Destroy(gameObject);
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
