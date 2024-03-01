using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    [SerializeField] private AudioSource soundSource;
    public AudioClip buttonSound;
    void Start()
    {
        soundSource = this.GetComponent<AudioSource>();
    }

    public void playSound()
    {
        soundSource.clip = buttonSound;

        if (!soundSource.isPlaying)
        {
            Debug.Log("button playing");
            soundSource.Play();
        }
        else
        {
            soundSource.Stop();
            soundSource.Play();
        }
    }
}
