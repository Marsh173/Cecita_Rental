using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class InspectionCameraTransition : MonoBehaviour
{
    public static InspectionCameraTransition instance;
    private Camera playercam;
    public Transform InspectionCam;

    private Transform originalCamPosition;
    private Vector3 originalPos, originalAngle;
    public bool isInCam;

    public GameObject inspectionButton; //for adding transcript overlay
    public GameObject inspectionWindow;
    private GameObject PlayBody;


    private void Start()
    {
        instance = this;
        playercam = FirstPersonAIO.instance.gameObject.GetComponentInChildren<Camera>();
        originalCamPosition = playercam.transform;
        inspectionButton.SetActive(false);
        PlayBody = FirstPersonAIO.instance.transform.GetChild(1).gameObject;
    }

    private void Update()
    {
        if (isInCam)
        {
            if (Input.GetMouseButtonDown(1))
            {
                TransitCamToOriginalPos();
            }
        }
    }

    public void TransitCamToOriginalPos()
    {
        isInCam = false;
        //playercam.transform.position = InspectionCam.position;
        //playercam.transform.rotation = InspectionCam.rotation;
        //playercam.transform.rotation = originalCamPosition.rotation;
        //StartCoroutine(LerpPosition(playercam.transform, originalPos, 1f));
        //StartCoroutine(LerpFunction(playercam.transform, originalAngle, 1f));
        playercam.transform.DOMove(originalPos, 1f);
        playercam.transform.DORotate(originalAngle, 1f).OnComplete(() => EnableMovement());

        gameObject.layer = 7;
    }

    public void TransitCamToInspectionPos()
    {
        Debug.Log("In cam?"+isInCam);

        originalCamPosition = playercam.transform;
        originalPos = originalCamPosition.position;
        originalAngle = originalCamPosition.rotation.eulerAngles;

        Debug.Log(originalPos + "original pos");
        Debug.Log(playercam.transform.position + "player pos");
        Debug.Log(InspectionCam.position + "cam pos");

        playercam.transform.DOMove(InspectionCam.position, 1);
        playercam.transform.DORotate(InspectionCam.rotation.eulerAngles, 1);

        FirstPersonAIO.instance.enableCameraMovement = false;
        FirstPersonAIO.instance.playerCanMove = false;
        Cursor.visible = true;
        isInCam = true;
        if(inspectionButton != null)
        {
            inspectionButton.SetActive(true);
        }
        PlayBody.GetComponent<MeshRenderer>().enabled = false;

        //outline + prompt message hide
        if (this.GetComponent<Outline>() != null)
        {
            this.GetComponent<Outline>().enabled = false;
        }

        gameObject.layer = 0;
    }

    private void EnableMovement()
    {
        FirstPersonAIO.instance.enableCameraMovement = true;
        FirstPersonAIO.instance.playerCanMove = true;

        Cursor.visible = false;
        if (inspectionButton != null)
        {
            inspectionButton.SetActive(false);
        }
        inspectionWindow.SetActive(false);
        PlayBody.GetComponent<MeshRenderer>().enabled = true;
        //outline + prompt message show
        if (this.GetComponent<Outline>() != null)
        {
            this.GetComponent<Outline>().enabled = true;
        }
        PlayerInteract.instnace.message.enabled = true;
        PlayerInteract.instnace.itemIcon.SetActive(true);
    }
}
