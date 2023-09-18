using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public playlistItems AItem;
    public NormalItems NItem;

    void CollectSound()
    {
        Debug.Log("collected");
        playlistManager.Instance.AddPlaylist(AItem);
        //playlistManager.Instance.Add(NItem);
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        //CollectSound();
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
