using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSmoothingFactor = -1;

    public float lookRight;
    public float lookLeft;

    private Quaternion camRotation;

    private void Start()
    {
        camRotation = transform.localRotation;
    }

    private void Update()
    {
        //Debug.Log(Input.GetAxis("Mouse X") + " : " + Input.GetAxis("Mouse Y"));

        camRotation.y += Input.GetAxis("Mouse X") * cameraSmoothingFactor;


        if(camRotation.y > 200)
        {
            camRotation.y = 200;
        }
        else if(camRotation.y < 90)
        {
            camRotation.y = 90;
        }


        transform.localRotation = Quaternion.Euler(camRotation.x, camRotation.y, camRotation.z);

        //Debug.Log(camRotation.y);
    }
}
