using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverInventory : MonoBehaviour
{
    [SerializeField] public GameObject hovertext;

    private void Start()
    {
        hidehovertext();
    }

    private void Update()
    {
        hovertext.GetComponent<RectTransform>().anchoredPosition = new Vector3(Input.mousePosition.x - Screen.width/2, Input.mousePosition.y - Screen.height/2 + 25, 0);
    }
    public void showhovertext()
    {
        hovertext.SetActive(true);
    }

    public void hidehovertext()
    {
        hovertext.SetActive(false);
    }
}
