using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEnabler : MonoBehaviour
{
    public void EnableGameObject(GameObject ObjectToEnable)
    {
        ObjectToEnable.SetActive(true);
    }

    public void DisableGameObject(GameObject ObjectToDisable)
    {
        ObjectToDisable.SetActive(false);
    }
}
