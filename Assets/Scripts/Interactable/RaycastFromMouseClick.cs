using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastFromMouseClick : MonoBehaviour
{
    private GameObject lastHitObject;
    public GameObject itemIcon;

    void Update()
    {
        // Create a ray from the mouse cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        // Check if the ray hits something
        if (Physics.Raycast(ray, out hitInfo) && hitInfo.collider.GetComponent<Interactable>() != null)
        {
            GameObject hitObject = hitInfo.collider.gameObject;

            if (hitObject.layer == LayerMask.NameToLayer("Keypad Buttons"))
            {
                hitInfo.collider.GetComponent<Interactable>().BaseInteract();

                if (lastHitObject == null)         //Make sure overlapped interactable objects remove outlines as intended
                {
                    if (hitInfo.collider.gameObject.GetComponent<Outline>() == null)
                    {
                        lastHitObject = hitObject;
                        Outline outlineScript = hitObject.AddComponent<Outline>();

                    }

                    //Debug.Log("Hover on: " + hitObject.name);

                }
            }
        }
        else
        {
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
