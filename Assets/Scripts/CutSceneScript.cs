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
    public GameObject taskList, taskTriggers, FirstPerson, startSceneObj, crosshair, dialoguemanager, endscneneObj;
    public TMP_Text cutSceneMonologue;
    public static bool talkedtoNPC, enteredEndSequen;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        cutSceneMonologue.text = "";
        cutsceneEnd = enteredEndSequen = false;
        talkedtoNPC = false;
        startcutscene = startSceneObj.GetComponent<PlayableDirector>();
        endscnene = endscneneObj.GetComponent<PlayableDirector>();
        emailsound = GetComponent<AudioSource>();
    }
    void Awake()
    {
        taskList.SetActive(false);
        taskTriggers.SetActive(false);
        FirstPerson.SetActive(false);
        crosshair.SetActive(false);
        dialoguemanager.SetActive(false);

        cutsceneEnd = enteredEndSequen = false;
        talkedtoNPC = false;
        startcutscene = startSceneObj.GetComponent<PlayableDirector>();
        endscnene = endscneneObj.GetComponent<PlayableDirector>();
        emailsound = GetComponent<AudioSource>();

        startSceneObj.SetActive(true);
        startcutscene.Play();
        startcutscene.stopped += NextsStep;
    }
    private void Update()
    {
        if (enteredEndSequen)
        {
            enteredEndSequen = false;
            FirstPerson.SetActive(false);
            crosshair.SetActive(false);
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
        taskTriggers.SetActive(true);
        FirstPerson.SetActive(true);
        startSceneObj.SetActive(false);
        crosshair.SetActive(true);
        dialoguemanager.SetActive(true);
        cutSceneMonologue.text = "That's an email from the manager, I better check my laptop.";
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
        cutSceneMonologue.text = "";
    }
}
