using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//[ExecuteInEditMode]
public class InventoryViewItem : MonoBehaviour
{
    //if there's an object, set the view item active and pull the model
    [SerializeField] TextMeshProUGUI iconname;
    [SerializeField, TextArea(3, 10)] string icon_Description = "";
    [SerializeField] int index;
    GameObject vig;

    private void Awake()
    {
        vig = GameObject.FindGameObjectWithTag("ViewItemGraphic");
        
    }
    public void ViewItem()
    {
        Debug.Log("vi");
        if (iconname != null)
        {
            vig.transform.GetChild(0).gameObject.SetActive(true);
            vig.GetComponentInChildren<UIInspectCamController>().index_obj = index;
            vig.GetComponentInChildren<UIInspectCamController>().changeOBJ();
            vig.transform.GetChild(0).GetChild(3).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = iconname.text;
            vig.transform.GetChild(0).GetChild(3).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = icon_Description;
        }
    }
}
