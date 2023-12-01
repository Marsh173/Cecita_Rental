using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//[ExecuteInEditMode]
public class InventoryViewItem : MonoBehaviour
{
    //if there's an object, set the view item active and pull the model
    [SerializeField] TextMeshProUGUI iconname;
    [SerializeField] GameObject vig;

    private void Awake()
    {
        vig = GameObject.FindGameObjectWithTag("ViewItemGraphic");
        Debug.Log("item clicked");
    }
    public void ViewItem()
    {
        if(iconname.text != "")
        {
            vig.transform.GetChild(0).gameObject.SetActive(true);
            vig.transform.GetChild(0).GetChild(2).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = iconname.text;
        }
    }
}
