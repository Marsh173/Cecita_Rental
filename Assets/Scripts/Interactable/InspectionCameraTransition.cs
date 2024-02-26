using UnityEngine;
using DG.Tweening;
using TMPro;

public class InspectionCameraTransition : MonoBehaviour
{
    private InteractableItem DocInfo;

    public Transform inspectionCameraPosition;
    public float transitionDuration = 1f;
    public float originalFOV = 60f;
    public float inspectionFOV = 60f;
    public GameObject inspectionScreen, inventory;
    private GameObject playBody, inspectionButton, transcriptWindow;

    private Camera playerCamera;
    private Vector3 originalCameraPosition;
    private Quaternion originalCameraRotation;

    public GameObject playerObj;
    private FirstPersonAIO player;

    public static bool isInCam = false;

    void Start()
    {
        DocInfo = GetComponent<InteractableItemWithEvent>();
        player = playerObj.GetComponent<FirstPersonAIO>();

        playerCamera = playerObj.GetComponentInChildren<Camera>();
        playerCamera = Camera.main;

        playBody = playerObj.transform.GetChild(1).gameObject;

        originalCameraPosition = playerCamera.transform.position;
        originalCameraRotation = playerCamera.transform.rotation;
        inspectionScreen.SetActive(false);

        inspectionButton = inspectionScreen.transform.GetChild(0).gameObject;
        transcriptWindow = inspectionScreen.transform.GetChild(1).gameObject;
    }

    void Update()
    {
        if (isInCam && Input.GetMouseButtonDown(1))
        {
            TransitionToOriginalPosition();
        }
    }

    public void TransitionToInspectionPosition()
    {
        if (inspectionCameraPosition == null)
        {
            Debug.LogError("Inspection camera position is not assigned!");
            return;
        }

        if (DocInfo.Doc != null)
        {
            transcriptWindow.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = DocInfo.Doc.transcript[0];
        }

        originalCameraPosition = playerCamera.transform.position;
        originalCameraRotation = playerCamera.transform.rotation;


        //active cursor, deactive player.
        player.enableFOVShift = false;
        player.enableCameraMovement = false;
        player.playerCanMove = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;


        //move camera position/rotation
        playerCamera.transform.DOMove(inspectionCameraPosition.position, transitionDuration);
        playerCamera.transform.DORotate(inspectionCameraPosition.rotation.eulerAngles, transitionDuration);
        playerCamera.fieldOfView = inspectionFOV;

        isInCam = true;


        //disable outline & interactable ability
        if (this.GetComponent<Outline>() != null)
        {
            this.GetComponent<Outline>().enabled = false;
        }

        this.gameObject.layer = 0;

        EnableInspectionUI(true);
    }

    public void TransitionToOriginalPosition()
    {
        playerCamera.transform.DOMove(originalCameraPosition, transitionDuration);
        playerCamera.transform.DORotate(originalCameraRotation.eulerAngles, transitionDuration);
        playerCamera.fieldOfView = originalFOV;

        isInCam = false;

        EnableInspectionUI(false);

        //deactive cursor, active player.
        player.enableFOVShift = true;
        player.enableCameraMovement = true;
        player.playerCanMove = true;
        Cursor.visible = false;
        this.gameObject.layer = 7;
    }

    void EnableInspectionUI(bool enable)
    {
        inspectionScreen.SetActive(enable);
        playBody.SetActive(enable);
        if (inspectionButton != null && DocInfo.Doc != null)
            inspectionButton.SetActive(enable);
        
        if (transcriptWindow.activeSelf)
            transcriptWindow.SetActive(false);
    }
}
