using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class PlayerInteract : MonoBehaviour
{
    //Event system
    public static PlayerInteract instnace;
    public static bool hasRecorderInHand = false;
    public UnityEvent EventTurnOnInteraction;
    public UnityEvent EventTurnOffInteraction;

    public Camera cam;
    public Ray ray;
    public TMP_Text pMessage, monologue;
    public GameObject itemIcon, inventory;
    private GameObject lastHitObject;

    [SerializeField]
    private float distance = 6f;
    //private float wallHitDistance = 6f;

    [SerializeField]
    private LayerMask mask;

    [Header("Crosshair")]
    public GameObject crosshair;

    public InspectionCameraTransition inspectScript;

    private int LayerWall;


    private void Start()
    {
        Time.timeScale = 1;

        instnace = this;
        if (pMessage.text != null) pMessage.text = "";
        if (monologue.text != null) monologue.text = "";

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        //Event system to turn camera on/off                                                        -Bryan
        /*if (Input.GetKeyUp(KeyCode.R) && InventoryManager.EquipmentCollected)
        {
            if (!hasRecorderInHand)
            {
                RunTurnOnEvent();
                hasRecorderInHand = true;
            }

            else
            {
                RunTurnOffEvent();
                hasRecorderInHand = false;
            }
        }*/
        //End of Event system code


        if(KeypadCameraTransition.isInKeyCam || InspectionCameraTransition.isInCam)
        {
            if (crosshair != null && crosshair.activeSelf) crosshair.SetActive(false);
        }
        else if (!KeypadCameraTransition.isInKeyCam || !InspectionCameraTransition.isInCam)
        {
            if (crosshair != null && !crosshair.activeSelf) crosshair.SetActive(true);
        }

        #region General Raycast
        //disable ray when in inspection cam or in inventory

        if (InspectionCameraTransition.isInCam || inventory.activeSelf)
        {
            ray = new Ray(new Vector3(0, 0, 0), Vector3.forward);
        }
        else
        {
            ray = new Ray(cam.transform.position, cam.transform.forward);
        }

        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo;


        if (Physics.Raycast(ray, out hitInfo, distance))
        {
            //turn off crosshair when hit NPC
            if (hitInfo.collider.CompareTag("NPC"))
            {
                if (crosshair != null && crosshair.activeSelf) crosshair.SetActive(false);
            }
        }
        else
        {
            if (crosshair != null && !crosshair.activeSelf && (!KeypadCameraTransition.isInKeyCam || !InspectionCameraTransition.isInCam)) crosshair.SetActive(true);
        }

        if (Physics.Raycast(ray, out hitInfo, distance, mask) && hitInfo.collider.GetComponent<Interactable>() != null)
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                if (lastHitObject == null)         //Make sure overlapped interactable objects remove outlines as intended
                {
                    if (hitInfo.collider.gameObject.GetComponent<Outline>() == null)
                    {
                        //Debug.Log(hitInfo.collider.GetComponent<Interactable>().promptMessage);
                        hitInfo.collider.GetComponent<Interactable>().BaseInteract();
                        pMessage.text = hitInfo.collider.GetComponent<Interactable>().promptMessage;
                        monologue.text = hitInfo.collider.GetComponent<Interactable>().monologueText;

                        //Turn on interactble item icon                                                     -Bryan's latest
                        itemIcon = hitInfo.collider.GetComponent<Interactable>().promptIcon;
                        itemIcon.SetActive(true);

                        //Turn off crosshair
                        if (crosshair != null && crosshair.activeSelf) crosshair.SetActive(false);

                        GameObject hitObject = hitInfo.collider.gameObject;
                        lastHitObject = hitObject;
                        Outline outlineScript = hitObject.AddComponent<Outline>();
                    }
                }
            }
            else
            {
                //Turn on crosshair
                if (crosshair != null && !crosshair.activeSelf && (!KeypadCameraTransition.isInKeyCam || !InspectionCameraTransition.isInCam)) crosshair.SetActive(true);

                if (pMessage.text != null) pMessage.text = "";
                if (monologue.text != null) monologue.text = "";

                if (itemIcon != null)
                {
                    //Turn off interactable item icon                                                   -Bryan's latest
                    itemIcon.SetActive(false);
                    itemIcon = null;
                }

                
            }
        }
        else
        {
            if (pMessage.text != null) pMessage.text = "";
            if (monologue.text != null) monologue.text = "";

            //Turn off interactable item icon                                                       -Bryan's latest
            if (itemIcon != null)
            {
                itemIcon.SetActive(false);
                itemIcon = null;
            }

            if (hitInfo.collider != null && !hitInfo.collider.CompareTag("NPC"))
            {
                //Turn on crosshair if not hit NPC
                if (crosshair != null && !crosshair.activeSelf) crosshair.SetActive(true);
            }
                

            if (lastHitObject != null)
            {
                lastHitObject.GetComponent<Interactable>().BaseDisableInteract();
                Outline scriptToDetach = lastHitObject.GetComponent<Outline>();
                if (scriptToDetach != null)
                {
                    Destroy(scriptToDetach);
                }
                lastHitObject = null;

            }
        }
        #endregion


    }

    public void RunTurnOnEvent()
    {
        EventTurnOnInteraction.Invoke();
    }

    public void RunTurnOffEvent()
    {
        EventTurnOffInteraction.Invoke();
    }

}
