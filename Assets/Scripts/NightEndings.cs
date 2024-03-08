using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightEndings : MonoBehaviour
{
    public AudioClip EndingAudio;
    public GameObject Bedroom, EmergencyRoomParts, endTimeline, player, system;
    private AudioSource audioS;
    private FirstPersonAIO playerControl;
    private Animator Anim;
    // Start is called before the first frame update
    void Start()
    {
        audioS = player.GetComponent<AudioSource>();
        playerControl = player.GetComponent<FirstPersonAIO>();
        Anim = player.GetComponentInChildren<Animator>();
        Anim.enabled = false;
    }

    private void Tutorial()
    {
        //Anim.enabled = true;
        //endTimeline.SetActive(true);
        audioS.PlayOneShot(EndingAudio);
    }

    private void Night1()
    {
        EmergencyRoomParts.SetActive(false);
        Bedroom.SetActive(true);
    }
    private void Night2()
    {
        system.SetActive(false);
    }

    private void SleepCutScene()
    {
        playerControl.playerCanMove = false;
        system.SetActive(false);
        //a line of code that disables camera movement
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
