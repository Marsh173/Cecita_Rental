using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerEarBudSequence : MonoBehaviour
{
    public List<AudioClip> audioClips = new List<AudioClip>();
    private AudioSource earBudVoice;
    public GameObject invisibleWall, tutorialMessageObj, inventory, interactiveDoor, UIPauseTutorial;
    private TMP_Text tutoriaMessage;
    private float warningTimes, audioTime;
    private int listCounter;

    private bool firstAudioPlayed, TabToOpen, FinishInventory;
    private bool DelayedAlready, interrupted;

    [SerializeField] private float delay = 0.0f;
    void Start()
    {
        UIPauseTutorial.SetActive(false);
        interactiveDoor.layer = 0; //set door to default layer
        tutorialMessageObj.SetActive(false);
        tutoriaMessage = tutorialMessageObj.GetComponent<TMP_Text>();
        earBudVoice = GetComponent<AudioSource>();
        DelayedAlready = firstAudioPlayed = FinishInventory = TabToOpen = false;
        warningTimes = audioTime = 0;
        interrupted = false;

        audioClips.Add(Resources.Load<AudioClip>("Night 0/" + "Night 0 - Stop!"));
        audioClips.Add(Resources.Load<AudioClip>("Night 0/" + "Night 0 - Follow music"));
        audioClips.Add(Resources.Load<AudioClip>("Night 0/" + "Night 0 - stick to wall"));

        foreach (AudioClip element in audioClips)
        {
            Debug.Log(element);
        }
    }

    void Update()
    {
        if (InventoryManager.EquipmentCollected && !firstAudioPlayed)
        {
            //taskPlaceholder1.SetActive(false);
            StartCoroutine(AudioStartSequence());
            delay = 16f;
            firstAudioPlayed = true;
        }
        
        if (DelayedAlready && !TabToOpen)
        {
            tutorialMessageObj.SetActive(true);

            if (inventory.transform.position.y == 540)
            {
                TabToOpen = true;
                tutorialMessageObj.SetActive(false);
                tutoriaMessage.text = "";
            }
        }

        if (/*!inventory.activeInHierarchy*/inventory.transform.position.y != 540 && TabToOpen && !earBudVoice.isPlaying)
        {
            FinishInventory = true;
        }

        //UI tutorial screen
        if (Input.GetKeyDown(KeyCode.F) && Time.timeScale == 0)
        {
            UIPauseTutorial.SetActive(false);
            Time.timeScale = 1;
        }

        if (earBudVoice.isPlaying && audioTime <= earBudVoice.clip.length)
        {
            audioTime ++;
            Debug.Log("audioTime" + audioTime);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(InventoryManager.EquipmentCollected)
        {
            if (other.CompareTag("Hallway"))
            {
                Time.timeScale = 0;
                UIPauseTutorial.SetActive(true);
            }
                if (other.CompareTag("EnterMonster") && !earBudVoice.isPlaying)
            {
                earBudVoice.clip = Resources.Load<AudioClip>("Night 0/" + "Night 0 - Stop!");
                earBudVoice.PlayOneShot(earBudVoice.clip);
                Destroy(other);
                listCounter = 1;
                audioTime = 0;
            }

            if (other.CompareTag("FollowMusicA") && !earBudVoice.isPlaying)
            {
                earBudVoice.clip = Resources.Load<AudioClip>("Night 0/" + "Night 0 - Follow music");
                earBudVoice.PlayOneShot(earBudVoice.clip);
                Destroy(other);
                listCounter = 2;
            }

            if (other.CompareTag("walker Trigger") && !earBudVoice.isPlaying)
            {
                earBudVoice.clip = Resources.Load<AudioClip>("Night 0/" + "Night 0 - stick to wall");
                earBudVoice.PlayOneShot(earBudVoice.clip);
                Destroy(other);
                listCounter = 3;
            }

            if(interrupted)
            {
                earBudVoice.Pause();
            }

            //tutorial warning about white noise
            if (other.CompareTag("InFrontOfMonster") && warningTimes != 3)
            {
                if(earBudVoice.isPlaying)
                {
                    interrupted = true;
                }
                else
                {
                    interrupted = false;
                }
                earBudVoice.clip = Resources.Load<AudioClip>("Night 0/" + "Night 0 - In front of sth");
                earBudVoice.PlayOneShot(earBudVoice.clip);
                warningTimes++;
            }
        }
        else
        {
            earBudVoice.Stop();
        }
        
        
    }
   IEnumerator AudioStartSequence()
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
