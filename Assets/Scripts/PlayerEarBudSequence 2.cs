using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEarBudSequence : MonoBehaviour
{
    private AudioSource earBudVoice;
    public GameObject invisibleWall;

    private bool firstAudioPlayed;
    [SerializeField] private float delay = 0.0f;
    void Start()
    {
        earBudVoice = GetComponent<AudioSource>();
        firstAudioPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (InventoryManager.equipmentCollected && !firstAudioPlayed)
        {
            earBudVoice.clip = Resources.Load<AudioClip>("Night 0/" + "Night 0 - Hi");
            StartCoroutine(delayPlay());
            earBudVoice.PlayOneShot(earBudVoice.clip);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnterMonster"))
        {
            earBudVoice.clip = Resources.Load<AudioClip>("Night 0/" + "stop2");
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
        firstAudioPlayed = true;
        yield return new WaitForSeconds(delay);
    }
}
