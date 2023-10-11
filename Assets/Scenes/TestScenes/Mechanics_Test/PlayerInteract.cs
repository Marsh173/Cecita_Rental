using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class PlayerInteract : MonoBehaviour
{
    //Event system
    public bool hasRecorderTurnedOn = false;
    public UnityEvent EventTurnOnInteraction;
    public UnityEvent EventTurnOffInteraction;

    public Camera cam;
    public TMP_Text message;
    public GameObject itemIcon;
    private GameObject lastHitObject;

    [SerializeField]
    private float distance = 6f;

    [SerializeField]
    private LayerMask mask;

    private void Start()
    {
        if (message.text != null) message.text = "";
    }

    private void Update()
    {
        //Event system to turn camera on/off                                                        -Bryan
        if (Input.GetKeyUp(KeyCode.R))
        {
            if (!hasRecorderTurnedOn)
            {
                RunTurnOnEvent();
                hasRecorderTurnedOn = true;
            }

            else
            {
                RunTurnOffEvent();
                hasRecorderTurnedOn = false;
            }
        }
        //End of Event system code


        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if(hitInfo.collider.GetComponent<Interactable>() != null)
            {
                if (hitInfo.collider.gameObject.GetComponent<Outline>() == null)
                {
                //Debug.Log(hitInfo.collider.GetComponent<Interactable>().promptMessage);
                hitInfo.collider.GetComponent<Interactable>().BaseInteract();
                message.text = hitInfo.collider.GetComponent<Interactable>().promptMessage;

                //Turn on interactble item icon                                                     -Bryan's latest
                itemIcon = hitInfo.collider.GetComponent<Interactable>().promptIcon;
                itemIcon.SetActive(true);
                //

                GameObject hitObject = hitInfo.collider.gameObject;
                lastHitObject = hitObject;
                Outline outlineScript = hitObject.AddComponent<Outline>();
                }
            }
            else
            {
                if (message.text != null) message.text = "";

                //Turn off interactable item icon                                                   -Bryan's latest
                itemIcon.SetActive(false);
                itemIcon = null;
                //
            }
        }
        else
        {
            if (message.text != null) message.text = "";

            //Turn off interactable item icon                                                       -Bryan's latest
            if (itemIcon != null)
            {
                itemIcon.SetActive(false);
                itemIcon = null;
            }
            //

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
