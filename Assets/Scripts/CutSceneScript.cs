using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using TMPro;

public class CutSceneScript : MonoBehaviour
{
    private PlayableDirector startcutscene, endscnene;
    public AudioSource emailsound;
    public AudioClip emailNote;
    public static bool cutsceneEnd;
    public GameObject taskList, FirstPerson, startSceneObj, cutSceneCanvas, crosshair, dialoguemanager, endscneneObj;
    public TMP_Text monologue;
    public static bool talkedtoNPC, enteredEndSequen;

    private void Start()
    {
        cutsceneEnd = enteredEndSequen = false;
        talkedtoNPC = false;
        startcutscene = startSceneObj.GetComponent<PlayableDirector>();
        endscnene = endscneneObj.GetComponent<PlayableDirector>();
        emailsound = GetComponent<AudioSource>();
    }
    void Awake()
    {
        taskList.SetActive(false);
        FirstPerson.SetActive(false);
        crosshair.SetActive(false);
        dialoguemanager.SetActive(false);

        cutsceneEnd = enteredEndSequen = false;
        talkedtoNPC = false;
        startcutscene = startSceneObj.GetComponent<PlayableDirector>();
        endscnene = endscneneObj.GetComponent<PlayableDirector>();
        emailsound = GetComponent<AudioSource>();

        startcutscene.Play();
        startcutscene.stopped += NextsStep;
    }
    private void Update()
    {
        if (enteredEndSequen)
        {
            endscneneObj.SetActive(true);
            endscnene.Play();
            endscnene.stopped += NexteStep;
        }
    }
    private void NextsStep(PlayableDirector obj)
    {
        Debug.Log("start scene ended");
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

    private void NexteStep(PlayableDirector obj)
    {
        endscneneObj.SetActive(false);
        SceneManager.LoadScene("Night_One");
    }
        public IEnumerator shoMonologue(int sec)
    {
        yield return new WaitForSeconds(sec);
        monologue.text = "";
    }
}
