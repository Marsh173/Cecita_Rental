using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerEarBudSequence : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource earBudVoice;
    public GameObject invisibleWall;
    public GameObject tutorialMessageObj;
    public TMP_Text tutoriaMessage;
    public GameObject inventory;

    private bool firstAudioPlayed;

    bool DelayedAlready;
    [SerializeField] private float delay = 0.0f;
    void Start()
    {
        tutorialMessageObj.SetActive(false);
        tutoriaMessage = tutorialMessageObj.GetComponent<TMP_Text>();
        earBudVoice = GetComponent<AudioSource>();
        firstAudioPlayed = false;
        //StartCoroutine(delayPlay());
        DelayedAlready = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (InventoryManager.equipmentCollected && !firstAudioPlayed)
        {
            earBudVoice.clip = Resources.Load<AudioClip>("Night 0/" + "Night 0 - Hi");
            earBudVoice.PlayOneShot(earBudVoice.clip);
            StartCoroutine(delayPlay());
            firstAudioPlayed = true;
        }
        
        if (DelayedAlready)
        {
            tutorialMessageObj.SetActive(true);

            if (inventory.active)
            {
                earBudVoice.clip = Resources.Load<AudioClip>("Night 0/" + "Night 0 - Inventory");
                if(!earBudVoice.isPlaying)
                {
                    earBudVoice.PlayOneShot(earBudVoice.clip);
                }
                tutorialMessageObj.SetActive(false);
                tutoriaMessage.text = "";
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
   IEnumerator delayPlay()
    {
        yield return new WaitForSeconds(delay);
        DelayedAlready = true;
        Debug.Log("finish delay");
    }
}
