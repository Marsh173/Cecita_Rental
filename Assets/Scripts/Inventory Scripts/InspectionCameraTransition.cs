using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class InspectionCameraTransition : MonoBehaviour
{
    public static InspectionCameraTransition instance;
    public InteractableItem DocInfo;
    private Camera playercam;
    public Transform InspectionCam;

    public float initialFOV;
    public float FOVOnInteraction;

    public Transform originalCamPosition;
    private Vector3 originalPos, originalAngle;
    public bool isInCam;

    public GameObject inspectionScreen, inspectionButtonW, transcriptWindow, taskmanager; //for adding transcript overlay
    private GameObject PlayBody;

    private void Start()
    {
        DocInfo = GetComponent<InteractableItemWithEvent>();
        InspectionCam = GetComponentInChildren<Camera>().transform;
        taskmanager = GameObject.FindWithTag("TaskManager");
        instance = this;
        playercam = FirstPersonAIO.instance.gameObject.GetComponentInChildren<Camera>();
        originalCamPosition = playercam.transform;
        PlayBody = FirstPersonAIO.instance.transform.GetChild(1).GetChild(0).gameObject;
        initialFOV = playercam.fieldOfView;
        FOVOnInteraction = GetComponentInChildren<Camera>().fieldOfView;
        initialFOV = 60f;
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
        FirstPersonAIO.instance.enableFOVShift = true;
        playercam.transform.DOMove(originalPos, 1f);
        playercam.transform.DORotate(originalAngle, 1f).OnComplete(() => EnableMovement());
        playercam.fieldOfView = initialFOV;

        gameObject.layer = 7;
    }

    public void TransitCamToInspectionPos()
    {

        transcriptWindow.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = DocInfo.Doc.transcript[0];

        Debug.Log("In cam?"+isInCam);

        originalCamPosition = playercam.transform;
        originalPos = originalCamPosition.position;
        originalAngle = originalCamPosition.rotation.eulerAngles;

        Debug.Log(originalPos + "original pos");
        Debug.Log(playercam.transform.position + "player pos");
        Debug.Log(InspectionCam.position + "cam pos");

        FirstPersonAIO.instance.enableFOVShift = false;
        playercam.transform.DOMove(InspectionCam.position, 1);
        playercam.transform.DORotate(InspectionCam.rotation.eulerAngles, 1);
        playercam.fieldOfView = FOVOnInteraction;

        FirstPersonAIO.instance.enableCameraMovement = false;
        FirstPersonAIO.instance.playerCanMove = false;
        Cursor.visible = true;
        isInCam = true;
        taskmanager.SetActive(false);

        if (inspectionScreen != null)
        {
            inspectionScreen.SetActive(true);

            if (inspectionButtonW != null)
            {
                inspectionButtonW.SetActive(true);
            }
        }
        PlayBody.GetComponent<SkinnedMeshRenderer>().enabled = false;

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
        taskmanager.SetActive(true);

        if (inspectionScreen != null)
        {
            inspectionScreen.SetActive(false);

            if (inspectionButtonW != null)
            {
                inspectionButtonW.SetActive(false);
            }
        }

        transcriptWindow.SetActive(false);

        PlayBody.GetComponent<SkinnedMeshRenderer>().enabled = true;
        //outline + prompt message show
        if (this.GetComponent<Outline>() != null)
        {
            this.GetComponent<Outline>().enabled = true;
        }
        PlayerInteract.instnace.pMessage.enabled = true;
        PlayerInteract.instnace.itemIcon.SetActive(true);
    }
}
