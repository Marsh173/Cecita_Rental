using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TV : Interactable
{
    public GameObject recorder;
    public GameObject plane;
    public GameObject videoPlayer;
    private bool interacted;
    private bool TvIsOn;
    private bool isRecorded = false;


    [Header("Recorder")]
    public GameObject sliderObj;
    public float sliderDuration = 3.0f;
    public TMP_Text popUpMessage;
    public Slider slider;
    public AudioSource audioSource;
    public AudioClip thePassword;


    private void Start()
    {
        interacted = false;
        recorder.SetActive(false);
        sliderObj.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        //LMB to open up keypad
        if (interacted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!TvIsOn && !isRecorded)
                {
                    plane.SetActive(true);
                    videoPlayer.SetActive(true);
                    promptMessage = "(Click to) Use the Recorder";
                    TvIsOn = true;
                }
                else if(TvIsOn && !isRecorded)
                {
                    promptMessage = "Record the Message";
                    recorder.SetActive(true);
                }
                else if(TvIsOn && isRecorded)
                {
                    TvIsOn = false;
                    plane.SetActive(false);
                    videoPlayer.SetActive(false);
                    promptMessage = "No need to use TV Anymore";

                }
            }
        }

    }

    protected override void Interact()
    {
        
        interacted = true;
    }

    protected override void DisableInteract()
    {
        //Debug.Log("Interacted with Player");
        interacted = false;
    }

    public void Record()
    {
        if (TvIsOn)
        {
            sliderObj.SetActive(true);
            StartCoroutine(IncreaseSliderValue());
        }
        else
        {
            popUpMessage.text = "No Source to be Recorded";
        }
        
    }

    private IEnumerator IncreaseSliderValue()
    {
        float startValue = slider.minValue;
        float elapsedTime = 0.0f;

        while (elapsedTime < sliderDuration)
        {
            float t = elapsedTime / sliderDuration;
            slider.value = Mathf.Lerp(startValue, slider.maxValue, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the slider reaches the target value
        slider.value = slider.maxValue;
        popUpMessage.text = "Recorded";
        sliderObj.SetActive(false);
        isRecorded = true;
        promptMessage = "Record complete, turn off the TV";
    }

    public void Play()
    {
        if (isRecorded)
        {
            //play audio
            audioSource.Play();
        }
        else
        {
            popUpMessage.text = "No Audio to Play";
        }
    }

    public void StopPlay()
    {
        if (isRecorded)
        {
            //play audio
            audioSource.Stop();
        }
        else
        {
            popUpMessage.text = "No Audio to Stop";
        }
    }

    public void Reverse()
    {
        if (isRecorded)
        {
            audioSource.clip = thePassword;
        }
        else
        {
            popUpMessage.text = "No Audio to Reverse";
        }
    }
}
