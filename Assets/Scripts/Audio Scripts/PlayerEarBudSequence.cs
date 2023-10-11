using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerEarBudSequence : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource earBudVoice;
    public GameObject invisibleWall, tutorialMessageObj, inventory, openDoorTrigger;
    public TMP_Text tutoriaMessage;

    private bool firstAudioPlayed, TabToOpen, FinishInventory;
    private bool DelayedAlready;

    [SerializeField] private float delay = 0.0f;
    void Start()
    {
        openDoorTrigger.SetActive(false);
        tutorialMessageObj.SetActive(false);
        tutoriaMessage = tutorialMessageObj.GetComponent<TMP_Text>();
        earBudVoice = GetComponent<AudioSource>();
        DelayedAlready = firstAudioPlayed = FinishInventory = TabToOpen = false;

        Debug.Log(inventory.activeInHierarchy);
    }

    void Update()
    {
        if (InventoryManager.equipmentCollected && !firstAudioPlayed)
        {
            StartCoroutine(AudioSequence());
            delay = 16f;
            firstAudioPlayed = true;
        }
        
        if (DelayedAlready)
        {
            tutorialMessageObj.SetActive(true);

            if (inventory.activeInHierarchy)
            {
                TabToOpen = true;
                tutorialMessageObj.SetActive(false);
                tutoriaMessage.text = "";
            }

            if (!inventory.activeInHierarchy && TabToOpen && !earBudVoice.isPlaying)
            {
                Debug.Log("close inventory");
                FinishInventory = true;
            }
        }


    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnterMonster"))
        {
            earBudVoice.clip = Resources.Load<AudioClip>("Night 0/" + "Night 0 - Stop!");
            earBudVoice.PlayOneShot(earBudVoice.clip);
            Destroy(other);
        }

        if (other.CompareTag("EnterDeadEnd"))
        {
            earBudVoice.clip = Resources.Load<AudioClip>("Night 0/" + "Night 0 - DeadEnd");
            earBudVoice.PlayOneShot(earBudVoice.clip);
            Destroy(other);
        }

        if (other.CompareTag("walker Trigger"))
        {
            earBudVoice.clip = Resources.Load<AudioClip>("Night 0/" + "Night 0 - stick to wall");
            earBudVoice.PlayOneShot(earBudVoice.clip);
        }
        
    }
   /* private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EnterMonster") && !invisibleWall)
        {
            Debug.Log("exit trigger");
            earBudVoice.clip = Resources.Load<AudioClip>("Night 0/" + "Night 0 - Follow music");
            earBudVoice.PlayOneShot(earBudVoice.clip);
        }

    }*/
   IEnumerator AudioSequence()
    {
        earBudVoice.clip = Resources.Load<AudioClip>("Night 0/" + "Night 0 - Hi");
        earBudVoice.PlayOneShot(earBudVoice.clip);
        yield return new WaitForSeconds(delay);
        DelayedAlready = true;
        Debug.Log("finish playing audio 1");

        //play audio 2 when inventory is open
        while (!TabToOpen)
        {
            yield return null;
        }
        earBudVoice.clip = Resources.Load<AudioClip>("Night 0/" + "Night 0 - Inventory");
        earBudVoice.PlayOneShot(earBudVoice.clip);
        Debug.Log("finish audio 2");

        //play next when inventory is closed
        while (!FinishInventory)
        {
            yield return null;
        }
        Debug.Log(inventory.activeInHierarchy);
        earBudVoice.clip = Resources.Load<AudioClip>("Night 0/" + "Night 0 - Hallway");
        earBudVoice.PlayOneShot(earBudVoice.clip);
        openDoorTrigger.SetActive(true);
    }


}
