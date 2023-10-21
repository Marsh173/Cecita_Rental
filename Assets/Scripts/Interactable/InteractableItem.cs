using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : Interactable
{
    [HideInInspector] public bool interacted;
    protected private bool interactedAndClicked = false;
    public NormalItems NItem;
    public PlaylistItems AItem;

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
        if (!InventoryManager.Instance.NItems.Contains(NItem))
            InventoryManager.Instance.AddNormal(NItem);
        //playlistManager.Instance.Add(NItem);
        Destroy(gameObject);
    }

    public void RecordMe()
    {
        Debug.Log("Recorded");
        if (!RecorderInventoryManager.Instance.AItems.Contains(AItem))
            RecorderInventoryManager.Instance.AddPlaylist(AItem);
        //playlistManager.Instance.Add(NItem);
        //Destroy(gameObject);
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
