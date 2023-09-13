using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioAnalysisManager : MonoBehaviour
{
    public GameObject track1_waveform;
    public GameObject track1_info;
    bool isSmartAnalysis;

    [Header("error Message")]
    public TMP_Text message;
    public GameObject error_message;
    public GameObject close_error;
    public Button[] buttons;

    [Header("Scan Audio")]
    public float activationDuration = 5.0f;
    private bool isScanning = false;
    private float activationTimer = 0.0f;
    public GameObject scan;

    [Header("Scan Result")]
    public GameObject scanResults;
    public GameObject segments;

    [Header("Segment2")]
    public GameObject segment2_waveform;
    private bool isDenoise = false;
    public GameObject result_waveform;
    public GameObject result;

    [Header("Transcript")]
    public TMP_Text transcript;

    [Header("Audio Player")]
    public Button audioPlayer;
    private Sprite playSprite;
    public Sprite stopSprite;
    private bool isPlaying = false;
    public AudioSource audio_full;
    public AudioSource audio_segment2;
    public AudioSource audio_result;
    public Slider slider;

    private void Start()
    {
        track1_waveform.SetActive(false);
        track1_info.SetActive(false);
        isSmartAnalysis = false;
        error_message.SetActive(false);
        scan.SetActive(false);
        close_error.SetActive(false);
        scanResults.SetActive(false);
        segments.SetActive(false);
        segment2_waveform.SetActive(false);
        result_waveform.SetActive(false);
        result.SetActive(false);

        playSprite = audioPlayer.image.sprite;
    }

    private void Update()
    {
        if (isScanning)
        {
            activationTimer += Time.deltaTime;

            if (activationTimer >= activationDuration)
            {
                // Disable the first object and enable the second object
                scan.SetActive(false);

                if (!isDenoise)
                {
                    scanResults.SetActive(true);
                    segments.SetActive(true);
                }
                else
                {
                    result_waveform.SetActive(true);
                    transcript.text = "DON'T TELL THEM YOU CAN SEE";
                    result.SetActive(true);
                    segment2_waveform.SetActive(false);
                }
                

                // Reset the timer and button state
                activationTimer = 0.0f;
                isScanning = false;
            }

            foreach (Button button in buttons)
            {
                button.interactable = false;
            }
        }

        if (isPlaying)
        {
            if (track1_waveform.activeSelf)
            {
                slider.value = audio_full.time / audio_full.clip.length;
                if (audio_full.time >= audio_full.clip.length)
                {
                    isPlaying = false;
                    audioPlayer.image.sprite = playSprite;
                }
            }
            else if (segment2_waveform.activeSelf)
            {
                slider.value = audio_segment2.time / audio_segment2.clip.length;
                if (audio_segment2.time >= audio_segment2.clip.length)
                {
                    isPlaying = false;
                    audioPlayer.image.sprite = playSprite;
                }
            }
            else if (result_waveform.activeSelf)
            {
                slider.value = audio_result.time / audio_result.clip.length;
                if (audio_result.time >= audio_result.clip.length)
                {
                    isPlaying = false;
                    audioPlayer.image.sprite = playSprite;
                }
            }

        }
    }


    //if track 1 is pressed, show waveform and track info.
    public void TrackName()
    {
        track1_waveform.SetActive(true);
        track1_info.SetActive(true);
        isSmartAnalysis = true;
    }

    public void SmartAnalysis()
    {
        if (isSmartAnalysis)
        {
            scan.SetActive(true);
            error_message.SetActive(false);
            isScanning = true;
        }
        else
        {
            //show message
            close_error.SetActive(true);
            error_message.SetActive(true);
            message.text = "No Track Selected! Can't use smart analysis.";
            foreach (Button button in buttons)
            {
                button.interactable = false;
            }
        }
    }

    public void CloseMessage()
    {
        if (error_message.activeSelf)
        {
            error_message.SetActive(false);
            close_error.SetActive(false);
            foreach (Button button in buttons)
            {
                button.interactable = true;
            }
        }
    }

    public void Segment2()
    {
        track1_waveform.SetActive(false);
        scanResults.SetActive(false);
        segments.SetActive(false);

        

        segment2_waveform.SetActive(true);
        transcript.text = "Segment 2 requires noise reduction to further analyze.";

        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
        
        buttons[2].interactable = true; //enable denoise
    }

    public void otherSegments()
    {
        close_error.SetActive(true);
        error_message.SetActive(true);
        message.text = "This segment does not contain perceivable information.";
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
    }

    public void Denoise()
    {
        if (segment2_waveform.activeSelf)
        {
            Debug.Log("Here");
            isScanning = true;
            isDenoise = true;
            scan.SetActive(true);
        }
        else
        {
            close_error.SetActive(true);
            error_message.SetActive(true);
            message.text = "Advanced options are not available at this stage.";
            foreach (Button button in buttons)
            {
                button.interactable = false;
            }
        }
        
    }

    public void PlayAudio()
    {
        if (track1_waveform.activeSelf)
        {
            if (isPlaying)
            {
                audioPlayer.image.sprite = playSprite;
                audio_full.Pause();
            }
            else
            {
                audioPlayer.image.sprite = stopSprite;
                audio_full.Play();
            }

            isPlaying = !isPlaying;
        }
        else if (segment2_waveform.activeSelf)
        {
            if (isPlaying)
            {
                audioPlayer.image.sprite = playSprite;
                audio_segment2.Pause();
            }
            else
            {
                audioPlayer.image.sprite = stopSprite;
                audio_segment2.Play();
            }

            isPlaying = !isPlaying;
        }
        else if (result_waveform.activeSelf)
        {
            if (isPlaying)
            {
                audioPlayer.image.sprite = playSprite;
                audio_result.Pause();
            }
            else
            {
                audioPlayer.image.sprite = stopSprite;
                audio_result.Play();
            }

            isPlaying = !isPlaying;
        }
        else
        {
            close_error.SetActive(true);
            error_message.SetActive(true);
            message.text = "No Track Selected! Can't play audio file.";
            foreach (Button button in buttons)
            {
                button.interactable = false;
            }
        }
    }
}
