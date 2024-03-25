using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayAudio : MonoBehaviour
{
    private AudioSource sound;
    public GameObject bObj;
    private RawImage buttonImage;
    public Texture playIcon, pauseIcon;
    private Vector3 playIconPos, pauseIconPos;
    bool playing;
    private PuzzleHandler puzzleHandler;
    public Slider s;

    private void Awake()
    {
        puzzleHandler = FindObjectOfType<PuzzleHandler>();
    }

    void Start()
    {
        playing = false;
        sound = gameObject.GetComponent<AudioSource>();
        buttonImage = bObj.GetComponent<RawImage>();
        playIconPos = new Vector3(1.8f, 0, 0);
        pauseIconPos = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!sound.isPlaying)
        {
            buttonImage.texture = playIcon;
            buttonImage.transform.localPosition = playIconPos;
        }
    }

    public void playPuzzleSound()
    {
        if (!playing)
        {
            //if (puzzleHandler.detectedNames[0] == "") { puzzleHandler.detectedNames[0] = sound.clip.ToString(); }
            //else if (puzzleHandler.detectedNames[1] == "") { puzzleHandler.detectedNames[1] = sound.clip.ToString(); }
            //else if (puzzleHandler.detectedNames[2] == "") { puzzleHandler.detectedNames[2] = sound.clip.ToString(); }
            //else if (puzzleHandler.detectedNames[3] == "") { puzzleHandler.detectedNames[3] = sound.clip.ToString(); }
            if (puzzleHandler.ableToInteract)
            {
                for (int i = 0; i < puzzleHandler.detectedNames.Count; i++)
                {
                    if (puzzleHandler.detectedNames[i] == "") { puzzleHandler.detectedNames[i] = sound.clip.ToString(); break; }
                }
            }

            playing = true;
            sound.Play();
            buttonImage.texture = pauseIcon;
            buttonImage.transform.localPosition = pauseIconPos;
        }
        else if(playing)
        {
            playing = false;
            sound.Pause();
            buttonImage.texture = playIcon;
            buttonImage.transform.localPosition = playIconPos;
        }
    }

    public void playAtTime()
    {
        sound.time = s.value * sound.clip.length;
    }

    public void playInventorySound()
    {
        if (!sound.isPlaying)
        {
            playing = true;
            sound.Play();
            buttonImage.texture = pauseIcon;
            buttonImage.transform.localPosition = pauseIconPos;
        }
        else
        {
            playing = false;
            sound.Pause();
            buttonImage.texture = playIcon;
            buttonImage.transform.localPosition = playIconPos;
        }
    }
}
