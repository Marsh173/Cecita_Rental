using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerEarBudSequence : MonoBehaviour
{
    public List<AudioClip> audioClips = new List<AudioClip>();
    private AudioSource earBudVoice;
    public GameObject basicMovementUI, interactTutorialObj, inventoryTutorialObj, inventory, crosshair, interactiveDoor, UIPauseTutorial;
    public float fadeDuration = 2f;
    private RawImage bMUI;
    private float warningTimes, audioTime;
    private int listCounter, interactCounter;

    private bool firstAudioPlayed, TabToOpen, FinishInventory;
    private bool DelayedAlready, interrupted;

    [SerializeField] private float delay = 0.0f;
    void Start()
    {
        basicMovementUI.SetActive(true);
        bMUI = basicMovementUI.GetComponent<RawImage>();
        UIPauseTutorial.SetActive(false);
        interactiveDoor.layer = 0; //set door to default layer
        inventoryTutorialObj.SetActive(false);
        interactTutorialObj.SetActive(false);
        earBudVoice = GetComponent<AudioSource>();
        DelayedAlready = firstAudioPlayed = FinishInventory = TabToOpen = false;
        warningTimes = audioTime = interactCounter = listCounter = 0;
        interrupted = false;

        //audioClips.Add(Resources.Load<AudioClip>("Night 0/" + "Night 0 - Stop!"));
        audioClips.Add(Resources.Load<AudioClip>("Night 0/" + "Night 0 - Follow music"));
        audioClips.Add(Resources.Load<AudioClip>("Night 0/" + "Night 0 - broadcast 2"));
        audioClips.Add(Resources.Load<AudioClip>("Night 0/" + "Night 0 - broadcast 3"));
        audioClips.Add(Resources.Load<AudioClip>("Night 0/" + "Night 0 - midpoint"));
        audioClips.Add(Resources.Load<AudioClip>("Night 0/" + "Night 0 - final point"));
        audioClips.Add(Resources.Load<AudioClip>("Night 0/" + "Night 0 - Entered bedroom"));

        /*
        foreach (AudioClip element in audioClips)
        {
            Debug.Log(element);
        }*/
    }

    void Update()
    {
        if(Input.anyKey)
        {
            StartCoroutine(FadeOut(bMUI));
        }

        //LMB to interact tutorial
        if (!crosshair.activeSelf/* && interactCounter <= 3*/)
        {
            interactTutorialObj.SetActive(true);
            //interactCounter++;
        }
        else interactTutorialObj.SetActive(false);

        if (InventoryManager.EquipmentCollected && !firstAudioPlayed)
        {
            //taskPlaceholder1.SetActive(false);
            StartCoroutine(AudioStartSequence());
            delay = 16f;
            firstAudioPlayed = true;
        }

        //Tab to open inventory tutorial
        if (DelayedAlready && !TabToOpen)
        {
            inventoryTutorialObj.SetActive(true);

            if (inventory.activeInHierarchy)
            {
                TabToOpen = true;
                inventoryTutorialObj.SetActive(false);
            }
        }
        /*inventory.transform.position.y != 540*/
        if (!inventory.activeInHierarchy && TabToOpen && !earBudVoice.isPlaying)
        {
            FinishInventory = true;
        }

        //UI tutorial screen
        if (Input.GetMouseButtonDown(0) && Time.timeScale == 0)
        {
            UIPauseTutorial.SetActive(false);
            Time.timeScale = 1;
        }

        //detect if a clip has finished playing
        if (earBudVoice.isPlaying && audioTime <= earBudVoice.clip.length)
        {
            audioTime = audioTime +  Time.deltaTime;
            //Debug.Log("audioTime" + audioTime);

        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(InventoryManager.EquipmentCollected && this.tag == "Player")
        {
            //Wallhit tutorial
            if (other.CompareTag("Hallway"))
            {
                Time.timeScale = 0;
                UIPauseTutorial.SetActive(true);

                Debug.Log("Collition stuff: " + this.name);
            }
            //Monster Sequence
            if (other.CompareTag("EnterMonster") && !earBudVoice.isPlaying)
            {
                earBudVoice.clip = Resources.Load<AudioClip>("Night 0/" + "Night 0 - Stop!");
                earBudVoice.PlayOneShot(earBudVoice.clip);
                Destroy(other);
                audioTime = 0;
            }

            //broadcast turn off guide
            if (other.CompareTag("FollowMusicA") && !earBudVoice.isPlaying)
            {
                earBudVoice.clip = audioClips[listCounter];
                Debug.Log("Audio: " + audioClips[listCounter]);
                earBudVoice.PlayOneShot(earBudVoice.clip);
                listCounter++;
                Destroy(other);
            }

            if (other.CompareTag("walker Trigger") && !earBudVoice.isPlaying)
            {
                earBudVoice.clip = Resources.Load<AudioClip>("Night 0/" + "Night 0 - stick to wall");
                earBudVoice.PlayOneShot(earBudVoice.clip);
                Destroy(other);
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

            if (other.CompareTag("InFrontOfMonster"))
            {

            }
        }
        else earBudVoice.Stop();
        
        
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

        Debug.Log("Inventory state: " + inventory.activeInHierarchy + "Finish invenTutor?: " + FinishInventory);
        earBudVoice.clip = Resources.Load<AudioClip>("Night 0/" + "Night 0 - Hallway");
        earBudVoice.PlayOneShot(earBudVoice.clip);
        interactiveDoor.layer = 7; //set door to be interactble layer
    }

    private IEnumerator FadeOut(RawImage rImage)
    {
        Color originalColor = rImage.color;
        float alphaStep = Time.fixedDeltaTime / fadeDuration;

        // Loop until the image is completely faded out
        while (rImage.color.a > 0f)
        {
            originalColor.a -= alphaStep;
            rImage.color = originalColor;
            yield return new WaitForFixedUpdate();
        }

        Debug.Log("Faded");
        // Ensure the image is completely faded out
        originalColor.a = 0f;
        rImage.color = originalColor;
        basicMovementUI.SetActive(false);
    }
}
