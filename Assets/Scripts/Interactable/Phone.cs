using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class Phone : MonoBehaviour
{
    public bool isInCam;
    public GameObject PhoneUI;
    public string phoneNumber;
    public TMP_Text PhoneNumberText;
    public string correctNumber;
    private bool CanDial;
    public Camera playercam;

    public LoadingScreen loadingscreen;

    //public GameObject hallway, hallwayAudio, bedroom;

    private void Start()
    {
        //bedroom.SetActive(false);
    }
    public void PhoneInteract()
    {
        //PhoneCam.enabled = true;
        //PhoneCam.DOFade(1, 1.2f);
        FirstPersonAIO.instance.enableCameraMovement = false;
        FirstPersonAIO.instance.playerCanMove = false;
        isInCam = true;
        Cursor.visible = true;
        PhoneUI.SetActive(true);
        CanDial = true;
    }


    public void PhoneInteract2()
    {
        FirstPersonAIO.instance.enableCameraMovement = false;
        FirstPersonAIO.instance.playerCanMove = false;
        isInCam = true;
        Cursor.visible = true;
        PhoneUI.SetActive(true);
        CanDial = true;
    }

    private void Update()
    {
        if (isInCam)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Debug.Log("esc");
                //PhoneCam.enabled = false;                           //IMPORTANT: ALWAYS TURN OFF ITEM CAMERAS ON EXITING ITEM INTERACTION
                //PhoneCam.DOFade(0, 1.2f);
                //FirstPersonAIO.instance.enableCameraMovement = true;
                //FirstPersonAIO.instance.playerCanMove = true;
                Cursor.visible = false;
                PhoneUI.SetActive(false);
                CanDial = false;
            }
        }
    }

    public void AddNumber(float temp)
    {
        if (phoneNumber.Length <3 && CanDial)
        {
            phoneNumber += temp;
            PhoneNumberText.text = phoneNumber;
        }

    }

    public void ClearNumber()
    {
        if (CanDial)
        {
            phoneNumber = "";
            PhoneNumberText.text = phoneNumber;
        }
    }

    public void CallNumber()
    {
        if (phoneNumber == correctNumber) //PuzzleHandler.hasSolvedClockPuzzle && 
        {
            //hallway.SetActive(false);
            //hallwayAudio.SetActive(false);
            //bedroom.SetActive(true);
            PhoneNumberText.text = "Dialing...";

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
        PhoneNumberText.text = "Wrong";
        CanDial = false;
        yield return new WaitForSeconds(1);
        phoneNumber = "";
        PhoneNumberText.text = phoneNumber;
        CanDial = true;
    }

}
