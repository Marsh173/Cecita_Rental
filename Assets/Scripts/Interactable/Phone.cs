using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class Phone : MonoBehaviour
{
    public RawImage PhoneCam;
    public bool isInCam;
    public GameObject PhoneUI;
    public string phoneNumber;
    public TMP_Text PhoneNumberText;
    public string correctNumber;
    private bool CanDial;
    public Camera playercam;
    public Transform PhoneCamTrans;
    public void PhoneInteract()
    {
        PhoneCam.enabled = true;
        PhoneCam.DOFade(1, 1.2f);
        FirstPersonAIO.instance.enableCameraMovement = false;
        FirstPersonAIO.instance.playerCanMove = false;
        isInCam = true;
        Cursor.visible = true;
        PhoneUI.SetActive(true);
        CanDial = true;
    }


    public void PhoneInteract2()
    {
        playercam.transform.DOMove(PhoneCamTrans.position, 2, true);
        playercam.transform.DORotate(PhoneCamTrans.rotation.eulerAngles, 2);
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
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("esc");
                PhoneCam.enabled = false;                           //IMPORTANT: ALWAYS TURN OFF ITEM CAMERAS ON EXITING ITEM INTERACTION
                PhoneCam.DOFade(0, 1.2f);
                FirstPersonAIO.instance.enableCameraMovement = true;
                FirstPersonAIO.instance.playerCanMove = true;
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
            PhoneNumberText.text = "dialing...";
            SceneManager.LoadScene("TemporaryMenu");
        }
        //else if(!PuzzleHandler.hasSolvedClockPuzzle && phoneNumber == correctNumber)
        //{
        //    PhoneNumberText.text = "dialing...Did you get the time correctly?";
        //}
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
