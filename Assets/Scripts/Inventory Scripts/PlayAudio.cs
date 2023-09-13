using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayAudio : MonoBehaviour
{
    private AudioSource sound;
    public GameObject bTextObj;
    Text buttonText;
    bool playing;
    float timePlaying;

    void Start()
    {
        playing = false;
        sound = gameObject.GetComponent<AudioSource>();
        buttonText = bTextObj.GetComponent<Text>();
        Debug.Log(buttonText);
    }

    // Update is called once per frame
    void Update()
    {
        if (!sound.isPlaying)
        {
            buttonText.text = "Play";
        }
    }

    public void playSound()
    {
        if (!playing)
        {
            playing = true;
            sound.Play();
            buttonText.text = "Pause";
        }
        else if(playing)
        {
            sound.Pause();
            buttonText.text = "Play";
            playing = false;
        }

        Debug.Log(playing);
    }
}
