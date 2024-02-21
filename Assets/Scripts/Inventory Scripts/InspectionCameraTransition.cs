using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class InspectionCameraTransition : MonoBehaviour
{
    public InteractableItem DocInfo;

    public float initialFOV = 60f, FOVOnInteraction = 60f;

    public static InspectionCameraTransition instance;
    [SerializeField] private Transform InspectionCam, originalCamPosition;
    private Camera playercam;
    private Vector3 originalPos, originalAngle;

    public static bool isInCam;

    public FirstPersonAIO player;
    public GameObject inspectionScreen, taskmanager; //for adding transcript overlay
    [SerializeField] private GameObject PlayBody, inspectionButton, transcriptWindow;

    private void Start()
    {
        DocInfo = GetComponent<InteractableItemWithEvent>();

        InspectionCam = GetComponentInChildren<Camera>().transform;
        playercam = player.gameObject.GetComponentInChildren<Camera>();
        PlayBody = player.transform.GetChild(1).GetChild(0).gameObject;
        FOVOnInteraction = GetComponentInChildren<Camera>().fieldOfView;
        originalCamPosition = playercam.transform;

        instance = this;
        initialFOV = 60f;
        initialFOV = playercam.fieldOfView;

        inspectionScreen.SetActive(false);
        inspectionButton = inspectionScreen.transform.GetChild(0).gameObject;
        transcriptWindow = inspectionScreen.transform.GetChild(1).gameObject;
        inspectionButton.SetActive(false);
        transcriptWindow.SetActive(false);
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
        player.enableFOVShift = true;
        playercam.transform.DOMove(originalPos, 1f);
        playercam.transform.DORotate(originalAngle, 1f).OnComplete(() => EnableMovement());
        playercam.fieldOfView = initialFOV;

        gameObject.layer = 7;
    }

    public void TransitCamToInspectionPos()
    {
        if(DocInfo.Doc != null)
        {
            transcriptWindow.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = DocInfo.Doc.transcript[0];
        }

        Debug.Log("In cam?"+isInCam);

        originalCamPosition = playercam.transform;
        originalPos = originalCamPosition.position;
        originalAngle = originalCamPosition.rotation.eulerAngles;

        Debug.Log(originalPos + "original pos");
        Debug.Log(playercam.transform.position + "player pos");
        Debug.Log(InspectionCam.position + "cam pos");

        player.enableFOVShift = false;
        playercam.transform.DOMove(InspectionCam.position, 1);
        playercam.transform.DORotate(InspectionCam.rotation.eulerAngles, 1);
        playercam.fieldOfView = FOVOnInteraction;

        player.enableCameraMovement = false;
        player.playerCanMove = false;
        Cursor.visible = true;
        isInCam = true;

        //taskmanager.SetActive(false);

        if (inspectionScreen != null)
        {
            inspectionScreen.SetActive(true);

            if (inspectionButton != null && DocInfo.Doc != null)
            {
                inspectionButton.SetActive(true);
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
        player.enableCameraMovement = true;
        player.playerCanMove = true;

        Cursor.visible = false;

        //taskmanager.SetActive(true);

        if (inspectionScreen != null)
        {
            inspectionScreen.SetActive(false);

            if (inspectionButton != null)
            {
                inspectionButton.SetActive(false);
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
