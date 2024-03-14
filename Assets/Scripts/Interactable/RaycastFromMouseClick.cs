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
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                if (lastHitObject == null)         //Make sure overlapped interactable objects remove outlines as intended
                {
                    if (hitInfo.collider.gameObject.GetComponent<Outline>() == null)
                    {
                        itemIcon = hitInfo.collider.GetComponent<Interactable>().promptIcon;
                        itemIcon.SetActive(true);

                        GameObject hitObject = hitInfo.collider.gameObject;
                        lastHitObject = hitObject;
                        Outline outlineScript = hitObject.AddComponent<Outline>();
                    }


                    if (Input.GetMouseButtonDown(0))
                    {
                        Debug.Log("Clicked on: " + hitInfo.collider.gameObject.name);
                        hitInfo.collider.GetComponent<Interactable>().BaseInteract();
                        // Perform actions when clicked
                    }
                }
            }
        }
    }
}
