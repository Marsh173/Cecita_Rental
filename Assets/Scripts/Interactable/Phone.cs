using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Phone : MonoBehaviour
{
    public RawImage PhoneCam;
    public bool isInCam;
    public void PhoneInteract()
    {
        PhoneCam.enabled = true;
        PhoneCam.DOFade(1, 1.2f);
        FirstPersonAIO.instance.enableCameraMovement = false;
        FirstPersonAIO.instance.playerCanMove = false;
        isInCam = true;
        Cursor.visible = true;
    }

    private void Update()
    {
        if (isInCam)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log("esc");
                PhoneCam.DOFade(0, 1.2f);
                FirstPersonAIO.instance.enableCameraMovement = true;
                FirstPersonAIO.instance.playerCanMove = true;

            }
        }
    }
}
