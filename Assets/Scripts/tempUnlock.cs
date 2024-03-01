using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempUnlock : MonoBehaviour
{
    public void Unlock()
    {
        if(InventoryManager.keyCollected)
        {
            Debug.Log("Unlocked");
            Destroy(gameObject);
        }
    }
}
