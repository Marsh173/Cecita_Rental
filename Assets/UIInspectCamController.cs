using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInspectCamController : MonoBehaviour
{
    [SerializeField]private GameObject selectedObject;
    [SerializeField] Vector3 mousepos;
    [SerializeField] Quaternion originalrot;

    private void Start()
    {
        selectedObject = GameObject.FindGameObjectWithTag("3DitemInspection");
        if(selectedObject!= null)originalrot = selectedObject.transform.rotation;
    }
    private void Update()
    {

      /*  selectedObject = GameObject.FindGameObjectWithTag("3DitemInspection");
        originalrot = selectedObject.transform.rotation;*/
       

        if (selectedObject != null)
        {
           

            if (Input.GetMouseButtonDown(0))
            {
                mousepos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 rotate = mousepos - new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                Vector3 rot = new Vector3(selectedObject.transform.rotation.eulerAngles.x + rotate.normalized.y * 7,
                   selectedObject.transform.rotation.eulerAngles.y + rotate.normalized.x *7,
                   selectedObject.transform.rotation.eulerAngles.z);
                
                selectedObject.transform.rotation = Quaternion.Euler(

                   rot

                    );
                mousepos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            }
        }
    }

    public void resetRot()
    {
        selectedObject.transform.rotation = originalrot;
    }

}
