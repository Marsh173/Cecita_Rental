using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerEarBudSequence : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource earBudVoice;
    public GameObject invisibleWall, tutorialMessageObj, inventory, interactiveDoor, UIPauseTutorial;
    public TMP_Text tutoriaMessage;

    private bool firstAudioPlayed, TabToOpen, FinishInventory;
    private bool DelayedAlready;

    [SerializeField] private float delay = 0.0f;
    void Start()
    {
        UIPauseTutorial.SetActive(false);
        interactiveDoor.layer = 0; //set door to default layer
        tutorialMessageObj.SetActive(false);
        tutoriaMessage = tutorialMessageObj.GetComponent<TMP_Text>();
        earBudVoice = GetComponent<AudioSource>();
        DelayedAlready = firstAudioPlayed = FinishInventory = TabToOpen = false;


        Debug.Log(inventory.activeInHierarchy);
    }

    void Update()
    {
        if (InventoryManager.EquipmentCollected && !firstAudioPlayed)
        {
            //taskPlaceholder1.SetActive(false);
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
                FinishInventory = true;
            }
        }

        //UI tutorial screen
        if(Input.GetKeyDown(KeyCode.F) && Time.timeScale == 0)
        {
            UIPauseTutorial.SetActive(false);
            Time.timeScale = 1;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(InventoryManager.EquipmentCollected)
        {
            if (other.CompareTag("EnterMonster"))
            {
                earBudVoice.clip = Resources.Load<AudioClip>("Night 0/" + "Night 0 - Stop!");
                earBudVoice.PlayOneShot(earBudVoice.clip);
                Destroy(other);
            }

            if (other.CompareTag("FollowMusicA"))
            {
                earBudVoice.clip = Resources.Load<AudioClip>("Night 0/" + "Night 0 - Follow music");
                earBudVoice.PlayOneShot(earBudVoice.clip);
                Destroy(other);
            }

            if (other.CompareTag("walker Trigger"))
            {
                earBudVoice.clip = Resources.Load<AudioClip>("Night 0/" + "Night 0 - stick to wall");
                earBudVoice.PlayOneShot(earBudVoice.clip);
                Destroy(other);
            }

            if (other.CompareTag("Hallway"))
            {
                Time.timeScale = 0;
                UIPauseTutorial.SetActive(true);
            }
        }
        else
        {
            earBudVoice.Stop();
        }
        
        
    }
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
        interactiveDoor.layer = 7; //set door to be interactble layer
    }


}
