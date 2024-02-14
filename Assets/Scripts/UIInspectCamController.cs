using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInspectCamController : MonoBehaviour
{
    [SerializeField]private GameObject selectedObject;
    [SerializeField] Vector3 mousepos, rotateV, mousepostemp;
    [SerializeField] Quaternion originalrot;
    [SerializeField] public int index_obj = 0;

    private void Awake()
    {
        selectedObject = GameObject.FindGameObjectWithTag("3DitemInspection");
        if(selectedObject!= null)originalrot = selectedObject.transform.rotation;
    }
    private void Update()
    {
        mousepostemp = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

        if (selectedObject != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mousepos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                rotateV = Vector3.zero;
            }

            if (Input.GetMouseButton(0))
            {
                rotateV = new Vector3(mousepostemp.y - mousepos.y, mousepos.x-mousepostemp.x, 0);
                selectedObject.transform.Rotate(rotateV.normalized, Space.World);
            }

            if (Input.GetMouseButtonUp(0))
            {
                mousepos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            }
        }
    }

    public void changeOBJ()
    {
        if (selectedObject != null)
        {
            for (int i = 0; i < selectedObject.transform.childCount; i++)
            {
                if (i == index_obj)
                {
                    selectedObject.transform.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    selectedObject.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }



    public void resetRot()
    {
        if (selectedObject != null)
        {
            selectedObject.transform.rotation = originalrot;
        }
    }

}
