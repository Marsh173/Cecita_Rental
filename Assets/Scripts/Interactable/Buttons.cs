using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    [SerializeField] private AudioSource soundSource;
    public AudioClip buttonSound;
    void Start()
    {
        soundSource = GetComponent<AudioSource>();
    }

    public void playSound()
    {
        soundSource.clip = buttonSound;

        if (!soundSource.isPlaying)
        {
            Debug.Log("playing");
            soundSource.Play();
        }
    }
}
