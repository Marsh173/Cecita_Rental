using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class InspectionCameraTransition : MonoBehaviour
{
    private Camera playercam;
    public Transform InspectionCam;


    public void TransitCamToInspectionPos()
    {
        playercam = FirstPersonAIO.instance.gameObject.GetComponentInChildren<Camera>();
        playercam.transform.DOMove(InspectionCam.position, 1);
        playercam.transform.DORotate(InspectionCam.rotation.eulerAngles, 1);
        FirstPersonAIO.instance.enableCameraMovement = false;
        FirstPersonAIO.instance.playerCanMove = false;
        Cursor.visible = true;
    }
}
