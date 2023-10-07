using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    public Camera cam;
    public TMP_Text message;
    private GameObject lastHitObject;

    [SerializeField]
    private float distance = 6f;

    [SerializeField]
    private LayerMask mask;

    private void Start()
    {
        message.text = "";
    }

    private void Update()
    {
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
                GameObject hitObject = hitInfo.collider.gameObject;
                lastHitObject = hitObject;
                Outline outlineScript = hitObject.AddComponent<Outline>();
                }
            }
            else
            {
                message.text = "";
                
            }
        }
        else
        {
            message.text = "";
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

}
