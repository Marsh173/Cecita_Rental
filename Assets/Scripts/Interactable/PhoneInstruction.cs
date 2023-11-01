using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class PhoneInstruction : MonoBehaviour
{
    public RawImage ItemCam;
    public bool isInCam;
    //public GameObject ItemUI;

    public void ItemInteract()
    {
        ItemCam.enabled = true;
        ItemCam.DOFade(1, 1.2f);
        FirstPersonAIO.instance.enableCameraMovement = false;
        FirstPersonAIO.instance.playerCanMove = false;
        isInCam = true;
        Cursor.visible = true;
        //ItemUI.SetActive(true);
    }

    private void Update()
    {
        if (isInCam)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("esc");
                ItemCam.enabled = false;                                    //IMPORTANT: ALWAYS TURN OFF ITEM CAMERAS ON EXITING ITEM INTERACTION
                ItemCam.DOFade(0, 1.2f);
                FirstPersonAIO.instance.enableCameraMovement = true;
                FirstPersonAIO.instance.playerCanMove = true;
                Cursor.visible = false;
                isInCam = false;
                //ItemUI.SetActive(false);
            }
        }
    }
}
