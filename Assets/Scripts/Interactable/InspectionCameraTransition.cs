using UnityEngine;
using DG.Tweening;
using TMPro;

public class InspectionCameraTransition : MonoBehaviour
{
    private InteractableItem DocInfo;

    public Transform inspectionCameraPosition;
    public float transitionDuration = 1f;
    public float originalFOV = 60f, inspectionFOV = 60f;

    public GameObject inspectionScreen, playerObj, inventory;
    private GameObject playBody, inspectionButton, transcriptWindow;

    private Camera playerCamera;
    private Vector3 originalCameraPosition;
    private Quaternion originalCameraRotation;
    private FirstPersonAIO player;

    public static bool isInCam = false; //for inventory reference only
    private bool isInInspection = false;

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
        if (isInInspection && Input.GetMouseButtonDown(1))
        {
            TransitionToOriginalPosition();
        }
    }

    public void TransitionToInspectionPosition()
    {

        //active cursor, deactive player.
        EnablePlayerMovement(false);

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

        //move camera position/rotation
        playerCamera.transform.DOMove(inspectionCameraPosition.position, transitionDuration);
        playerCamera.transform.DORotate(inspectionCameraPosition.rotation.eulerAngles, transitionDuration);
        playerCamera.fieldOfView = inspectionFOV;

        isInCam = true;
        isInInspection = true;

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
        playerCamera.fieldOfView = originalFOV;
        playerCamera.transform.DOMove(originalCameraPosition, transitionDuration);
        playerCamera.transform.DORotate(originalCameraRotation.eulerAngles, transitionDuration).OnComplete(() => EnablePlayerMovement(true));
        
        isInCam = false;
        isInInspection = false;

        EnableInspectionUI(false);

        //deactive cursor, active player.
        if (inventory.activeSelf)
        {
            inventory.SetActive(false);
        }

        //EnablePlayerMovement(true);
        this.gameObject.layer = 7;
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
        playBody.SetActive(enable);
        player.enableFOVShift = enable;
        player.enableCameraMovement = enable;
        player.playerCanMove = enable;
        playerCamera.transform.localPosition = new Vector3(0, 0, 0);
        Cursor.visible = !enable;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
