using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayAudio : MonoBehaviour
{
    private AudioSource sound;
    public GameObject bTextObj;
    TMP_Text buttonText;
    bool playing;
    float timePlaying;
    private PuzzleHandler puzzleHandler;

    private void Awake()
    {
        puzzleHandler = FindObjectOfType<PuzzleHandler>();
    }

    void Start()
    {
        playing = false;
        sound = gameObject.GetComponent<AudioSource>();
        buttonText = bTextObj.GetComponent<TMP_Text>();
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
            //if (puzzleHandler.detectedNames[0] == "") { puzzleHandler.detectedNames[0] = sound.clip.ToString(); }
            //else if (puzzleHandler.detectedNames[1] == "") { puzzleHandler.detectedNames[1] = sound.clip.ToString(); }
            //else if (puzzleHandler.detectedNames[2] == "") { puzzleHandler.detectedNames[2] = sound.clip.ToString(); }
            //else if (puzzleHandler.detectedNames[3] == "") { puzzleHandler.detectedNames[3] = sound.clip.ToString(); }

            for (int i = 0; i < puzzleHandler.detectedNames.Count; i++) 
            {
                if (puzzleHandler.detectedNames[i] == "") { puzzleHandler.detectedNames[i] = sound.clip.ToString(); break; }
            }

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
