using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSound : MonoBehaviour
{
    public playlistItems Myitem;

    private void OnEnable()
    {
        NoSightAllowed.instance.SoundCollecting.SetActive(true);
    }

    private void OnDisable()
    {
        NoSightAllowed.instance.SoundCollecting.SetActive(false);
        playlistManager.Instance.Add(Myitem); 
        NoSightAllowed.instance.itemAdded.SetActive(true);
    }

}
