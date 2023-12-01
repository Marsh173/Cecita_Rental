using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEnabler : MonoBehaviour
{
    private Image buttonimage;
    private void Start()
    {
        buttonimage = GetComponent<Button>().image;
    }

    public void EnableGameObject(GameObject ObjectToEnable)
    {
        ObjectToEnable.SetActive(true);
    }

    public void DisableGameObject(GameObject ObjectToDisable)
    {
        ObjectToDisable.SetActive(false);
    }

    public void changeSpriteClicked(Sprite buttonImageC)
    {
        if(buttonimage.sprite != buttonImageC)
        {
            buttonimage.sprite = buttonImageC;
        }
    }
}
