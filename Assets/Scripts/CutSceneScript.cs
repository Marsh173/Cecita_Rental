using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using TMPro;

public class CutSceneScript : MonoBehaviour
{
    private PlayableDirector cutscene;
    public AudioSource emailsound;
    public AudioClip emailNote;
    public static bool cutsceneEnd;
    public GameObject taskList, FirstPerson, startSceneObj, cutSceneCanvas, crosshair, dialoguemanager;
    public TMP_Text monologue;

    void Awake()
    {
        cutsceneEnd = false;
        cutscene = GetComponent<PlayableDirector>();
        emailsound = GetComponent<AudioSource>();

        taskList.SetActive(false);
        FirstPerson.SetActive(false);
        crosshair.SetActive(false);
        dialoguemanager.SetActive(false);

        cutscene.Play();
        cutscene.stopped += NextStep;
    }

    private void NextStep(PlayableDirector obj)
    {
        cutsceneEnd = true;
        emailsound.PlayOneShot(emailNote);
        taskList.SetActive(true);
        FirstPerson.SetActive(true);
        startSceneObj.SetActive(false);
        cutSceneCanvas.SetActive(false);
        crosshair.SetActive(true);
        dialoguemanager.SetActive(true);
        monologue.text = "That's an email from the manager, I better check my laptop.";
        StartCoroutine(shoMonologue(5));
    }
    public IEnumerator shoMonologue(int sec)
    {
        yield return new WaitForSeconds(sec);
        monologue.text = "";
    }
}
