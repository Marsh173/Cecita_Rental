using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class InspectionCameraTransition : MonoBehaviour
{
    private Camera playercam;
    public Transform InspectionCam;

    private Transform originalCamPosition;
    private bool isInCam;

    public GameObject inspectionButton; //for adding transcript overlay
    public GameObject inspectionWindow;

    private void Start()
    {
        playercam = FirstPersonAIO.instance.gameObject.GetComponentInChildren<Camera>();
        originalCamPosition = playercam.transform;
        inspectionButton.SetActive(false);

    }

    private void Update()
    {
        if (isInCam)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("esc");
                playercam.transform.DOMove(originalCamPosition.position, 1);
                playercam.transform.DORotate(originalCamPosition.rotation.eulerAngles, 1);
                FirstPersonAIO.instance.enableCameraMovement = true;
                FirstPersonAIO.instance.playerCanMove = true;
                Cursor.visible = false;
                isInCam = false;
                inspectionButton.SetActive(false);
                inspectionWindow.SetActive(false);
            }
        }
    }

    public void TransitCamToInspectionPos()
    {
        
        playercam.transform.DOMove(InspectionCam.position, 1);
        playercam.transform.DORotate(InspectionCam.rotation.eulerAngles, 1);
        FirstPersonAIO.instance.enableCameraMovement = false;
        FirstPersonAIO.instance.playerCanMove = false;
        Cursor.visible = true;
        isInCam = true;
        inspectionButton.SetActive(true);
    }
}
