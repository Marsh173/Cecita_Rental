using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroadcastManager : MonoBehaviour
{
    public List<FMODStudioChangeMusic> speakerList;

    void Start()
    {   
        speakerList = new List<FMODStudioChangeMusic>();
        int childCount = this.transform.childCount - 1; //all speaker children except the next trigger

        for(int i = 0; i < childCount; i++)
        {
            Transform child = this.transform.GetChild(i);
            speakerList.Add(child.gameObject.GetComponent<FMODStudioChangeMusic>());
            //Debug.Log(speakerList[i].gameObject.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach(FMODStudioChangeMusic speaker  in speakerList)
            {
                speaker.ActivateSpeaker();
                Debug.Log("speaker list:" + speaker.gameObject.name);
            }
            
        }
    }
}
