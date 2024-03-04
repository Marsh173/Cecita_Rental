using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class KeypadGeneral : MonoBehaviour
{
    public bool KeypadIsInCam, isPhone, Unlocked;
    public GameObject KeypadObj, container;
    public TMP_Text NumberText;
    public string correctNumber;
    public int codeLength;
    private AudioSource audioS;
    public AudioClip correctSound, wrongSound;

    [SerializeField] private string EnteredNumber;
    private bool CanEnter;

    public LoadingScreen loadingscreen;
    public string thisSceneName, nextSceneName;
    public UnityEvent EventIfCorrect;

    //public GameObject hallway, hallwayAudio, bedroom;

    private void Start()
    {
        //bedroom.SetActive(false);
        audioS = GetComponent<AudioSource>();
        KeypadObj.SetActive(false);
        Unlocked = false;
    }
    public void KeypadInteract()
    {
        KeypadIsInCam = true;
        KeypadObj.SetActive(true);
        CanEnter = true;
        Debug.Log("KeypadisInCam");
    }
    
    private void Update()
    {
        if (KeypadIsInCam)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("esc");             //IMPORTANT: ALWAYS TURN OFF ITEM CAMERAS ON EXITING ITEM INTERACTION
                
                Cursor.visible = false;
                KeypadObj.SetActive(false);
                CanEnter = false;
                KeypadIsInCam = false;
            }
        }
    }

    public void AddNumber(float temp)
    {

        if (EnteredNumber.Length < codeLength && CanEnter)
        {
            EnteredNumber += temp;
            NumberText.text = EnteredNumber;
        }

    }

    public void ClearNumber()
    {
        if (CanEnter)
        {
            EnteredNumber = "";
            NumberText.text = EnteredNumber;
        }
    }

    public void EnterNumber()
    {
        if (EnteredNumber == correctNumber) //PuzzleHandler.hasSolvedClockPuzzle && 
        {
            Unlocked = true;
            KeypadObj.SetActive(false);
            Debug.Log("number correct");
            Debug.Log("camera state keypad: " + FirstPersonAIO.instance.enableCameraMovement);

            if (audioS.isPlaying)
            {
                audioS.Stop();
                audioS.PlayOneShot(correctSound);
            }
            else audioS.PlayOneShot(correctSound);

            if(container != null && container.GetComponent<Animation>())
            {
                container.GetComponent<Animation>().Play();
                Debug.Log("Animation played");
            }

            EventIfCorrect.Invoke();

            //get Current Scene first
            Scene currentScene = SceneManager.GetActiveScene();

            if (currentScene.name != "Night_Two" && isPhone) //if it's night 1
            {
                loadingscreen.LoadSceneWithLoadingScreen("Night_Two");

                //hallway.SetActive(false);
                //hallwayAudio.SetActive(false);
                //bedroom.SetActive(true);
            }
            else if (isPhone)
            {
                loadingscreen.LoadSceneWithLoadingScreen("MainMenu");
            }

            this.gameObject.layer = 0;
            ClearNumber();

        }
        else
        {
            Debug.Log("wrong number");

            StartCoroutine(WrongNumber());
            ClearNumber();
        }
    }

    IEnumerator WrongNumber()
    {
        if (audioS.isPlaying)
        {
            audioS.Stop();
            audioS.PlayOneShot(wrongSound);
        }
        else audioS.PlayOneShot(wrongSound);

        CanEnter = false;
        yield return new WaitForSeconds(1);
        EnteredNumber = "";
        NumberText.text = EnteredNumber;
        CanEnter = true;
    }

}
