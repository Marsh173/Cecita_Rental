using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HoverInventory : MonoBehaviour
{
    public GameObject hovertext;
    [SerializeField] public string transcript;
    public GameObject highlighttxt;
    //[SerializeField] private float yPos;

    private void Start()
    {
        hovertext = GameObject.FindGameObjectWithTag("HoverText");
        hidehovertext();
        //yPos = 0;
    }

    private void Update()
    {
        //hovertext.GetComponent<RectTransform>().anchoredPosition = new Vector3(Input.mousePosition.x - Screen.width/2, Input.mousePosition.y - Screen.height/2 + yPos, 0);
    }
    public void showhovertext()
    {
        hovertext = GameObject.FindGameObjectWithTag("HoverText");

        if (hovertext != null)
        {
            hovertext.transform.GetChild(0).gameObject.SetActive(true);
            hovertext.GetComponentInChildren<TMP_Text>().text = transcript;
        }
    }

    public void hidehovertext()
    {
        hovertext = GameObject.FindGameObjectWithTag("HoverText");
        
        if (hovertext != null)
        {
            //hovertext.GetComponentInChildren<TMP_Text>().text = "";
            hovertext.transform.GetChild(0).gameObject.SetActive(false);
            
        }
    }

    public void changecolorText()
    {
        

    }
}
