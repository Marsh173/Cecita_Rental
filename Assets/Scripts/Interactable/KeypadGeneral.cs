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
    public bool KeypadisInCam;
    public GameObject KeypadObj;
    public TMP_Text NumberText;
    public string correctNumber;
    public int codeLength;
    private AudioSource audioS;
    public AudioClip correctSound, wrongSound;

    [SerializeField] private string EnteredNumber;
    private bool CanEnter;
    public bool EnteredCorrectNumber;

    public LoadingScreen loadingscreen;
    public string thisSceneName, nextSceneName;
    public UnityEvent EventIfCorrect;

    //public GameObject hallway, hallwayAudio, bedroom;

    private void Start()
    {
        //bedroom.SetActive(false);
        audioS = GetComponent<AudioSource>();
        EnteredCorrectNumber = false;
        KeypadObj.SetActive(false);
    }
    public void KeypadInteract()
    {
        KeypadisInCam = true;
        KeypadObj.SetActive(true);
        CanEnter = true;
        Debug.Log("KeypadisInCam");
    }
    
    private void Update()
    {
        if (KeypadisInCam)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("esc");             //IMPORTANT: ALWAYS TURN OFF ITEM CAMERAS ON EXITING ITEM INTERACTION
                
                Cursor.visible = false;
                KeypadObj.SetActive(false);
                CanEnter = false;
                KeypadisInCam = false;
            }
        }

        if(EnteredCorrectNumber)
        {
            EventIfCorrect.Invoke();
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
            //hallway.SetActive(false);
            //hallwayAudio.SetActive(false);
            //bedroom.SetActive(true);

            KeypadObj.SetActive(false);
            EnteredCorrectNumber = true;
            Debug.Log("number correct");
            Debug.Log("camera state keypad: " + FirstPersonAIO.instance.enableCameraMovement);

            if (audioS.isPlaying)
            {
                audioS.Stop();
                audioS.PlayOneShot(correctSound);
            }
            else audioS.PlayOneShot(correctSound);

            //get Current Scene first
            Scene currentScene = SceneManager.GetActiveScene();

            if (currentScene.name != "Night_Two")
            {
                loadingscreen.LoadSceneWithLoadingScreen("Night_Two");
            }
            else
            {
                loadingscreen.LoadSceneWithLoadingScreen("MainMenu");
            }

        }
        else
        {
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
        EnteredCorrectNumber = false;
        yield return new WaitForSeconds(1);
        EnteredNumber = "";
        NumberText.text = EnteredNumber;
        CanEnter = true;
    }

}
