using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;

public class InspectionCameraTransition : MonoBehaviour
{
    private InteractableItem DocInfo;
    private List<string> pages = new List<string>();
    private int pageNumber = 0;

    public Transform inspectionCameraPosition;
    public float transitionDuration = 1f;
    public float originalFOV = 60f, inspectionFOV = 60f;

    public GameObject inspectionScreen, playerObj, inventory;
    private GameObject playBody, inspectionButton, transcriptWindow;

    private Camera playerCamera;
    private Vector3 originalCameraPosition;
    private Quaternion originalCameraRotation;
    private FirstPersonAIO player;

    public static bool isInCam = false; //for reference only
    private bool isInInspection = false;
    private KeypadGeneral keyPad; //for checking if it is a keypad lock

    void Start()
    {
        DocInfo = GetComponent<InteractableItemWithEvent>();
        player = playerObj.GetComponent<FirstPersonAIO>();
        keyPad = GetComponent<KeypadGeneral>();

        playerCamera = playerObj.GetComponentInChildren<Camera>();
        playerCamera = Camera.main;

        playBody = playerObj.transform.GetChild(0).GetChild(2).gameObject;

        originalCameraPosition = playerCamera.transform.position;
        originalCameraRotation = playerCamera.transform.rotation;
        inspectionScreen.SetActive(false);

        inspectionButton = inspectionScreen.transform.GetChild(0).gameObject;
        transcriptWindow = inspectionScreen.transform.GetChild(1).gameObject;
    }

    void Update()
    {
        if (isInInspection && Input.GetMouseButtonDown(1))
        {
            TransitionToOriginalPosition();
        }
    }

    public void TransitionToInspectionPosition()
    {
        this.gameObject.layer = 0;

        //active cursor, deactive player.
        EnablePlayerMovement(false);

        if (inspectionCameraPosition == null)
        {
            Debug.LogError("Inspection camera position is not assigned!");
            return;
        }

        if (DocInfo.Doc != null)
        {
            pages = DocInfo.Doc.transcript;
            transcriptWindow.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = pages[0];
        }

        originalCameraPosition = playerCamera.transform.position;
        originalCameraRotation = playerCamera.transform.rotation;

        //move camera position/rotation, disable player body after finished transition
        playerCamera.transform.DOMove(inspectionCameraPosition.position, transitionDuration);
        playerCamera.transform.DORotate(inspectionCameraPosition.rotation.eulerAngles, transitionDuration).OnComplete(() => playBody.SetActive(false));
        playerCamera.fieldOfView = inspectionFOV;
        Debug.Log("Camera transformed to inspect");

        isInCam = true;
        isInInspection = true;

        //disable outline & interactable ability
        if (this.GetComponent<Outline>() != null)
        {
            this.GetComponent<Outline>().enabled = false;
        }

        EnableInspectionUI(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void TransitionToOriginalPosition()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        //show player as soon as exit cam
        playBody.SetActive(true);
        playerCamera.fieldOfView = originalFOV;
        playerCamera.transform.DOMove(originalCameraPosition, transitionDuration);
        playerCamera.transform.DORotate(originalCameraRotation.eulerAngles, transitionDuration).OnComplete(() => EnablePlayerMovement(true));
        Debug.Log("Camera transformed back");

        isInCam = false;
        isInInspection = false;

        EnableInspectionUI(false);

        //deactive cursor, active player.
        if (inventory.activeSelf)
        {
            inventory.SetActive(false);
        }
        
        //if it is a keypad lock and has been unlocked, deactivate the object interact
        if(keyPad != null && keyPad.Unlocked)
        {
            this.gameObject.layer = 0;
        }
        else this.gameObject.layer = 7;
    }

    void EnableInspectionUI(bool enable)
    {
        inspectionScreen.SetActive(enable);

        if (/*inspectionButton != null && */DocInfo.Doc != null)
        {
            inspectionButton.SetActive(enable);
        }
        else inspectionButton.SetActive(!enable);

        if (transcriptWindow.activeSelf)
            transcriptWindow.SetActive(false);
    }

    void EnablePlayerMovement(bool enable) 
    {
        player.enableFOVShift = enable;
        player.enableCameraMovement = enable;
        player.playerCanMove = enable;
        playerCamera.transform.localPosition = new Vector3(0, 0, 0);
        //Debug.Log("camera state: " + player.enableCameraMovement);
    }

    void NextPage()
    {
        if (pageNumber < pages.Count)
        {
            pageNumber++;
            transcriptWindow.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = pages[pageNumber];
        }
        else pageNumber = pageNumber;

        Debug.Log("N Total page" + pages.Count + " Current page: " + pageNumber);
    }

    void PreviousPage()
    {
        if (pageNumber >= 0)
        {
            pageNumber--;
            transcriptWindow.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = pages[pageNumber];
        }
        else pageNumber = pageNumber;

        Debug.Log("P Total page" + pages.Count + " Current page: " + pageNumber);
    }
}
