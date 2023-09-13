using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public playlistItems Item;
   
    void CollectSound()
    {
        Debug.Log("collected");
        playlistManager.Instance.Add(Item);
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        CollectSound();
    }

    
    /*
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            CollectSound();
        }
    }*/
}
