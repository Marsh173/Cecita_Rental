using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSound : MonoBehaviour
{
    public PlaylistItems Myitem;

    private void OnEnable()
    {
        if (NoSightAllowed.instance != null)
        {
            NoSightAllowed.instance.SoundCollecting.SetActive(true);
        }
    }

    private void OnDisable()
    {
        if (NoSightAllowed.instance != null)
        {
            NoSightAllowed.instance.SoundCollecting.SetActive(false);
            InventoryManager.Instance.AddPlaylist(Myitem);
            NoSightAllowed.instance.itemAdded.SetActive(true);
        }
    }

}
