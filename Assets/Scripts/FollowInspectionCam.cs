using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowInspectionCam : MonoBehaviour
{
    public Transform target; // Drag and drop your 3D object here in the Unity Editor
    public Transform targeted_obj;

    void Update()
    {
        
        if (target != null)
        {
            targeted_obj = target.GetChild(0);
            // Calculate the position the camera should be to look at the target
            Vector3 targetPosition = targeted_obj.position;

            // Set the camera's position and rotation
            transform.LookAt(targetPosition);
        }
    }
}
