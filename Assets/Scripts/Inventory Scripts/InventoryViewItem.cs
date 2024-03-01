using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//[ExecuteInEditMode]
public class InventoryViewItem : MonoBehaviour
{
    //if there's an object, set the view item active and pull the model
    public TMP_Text iconname;
    [TextArea(3, 10)] public string icon_Description = "";
    [SerializeField] int ObjChildIndex;
    [SerializeField] GameObject vig;

    private void Awake()
    {
        vig = GameObject.FindGameObjectWithTag("ViewItemGraphic");
    }
    public void ViewItem()
    {
        Debug.Log("viewed");
        
        vig.transform.GetChild(0).gameObject.SetActive(true);
        vig.GetComponentInChildren<UIInspectCamController>().index_obj = ObjChildIndex;
        vig.GetComponentInChildren<UIInspectCamController>().changeOBJ();
        
        vig.transform.GetChild(0).GetChild(1).GetChild(1).gameObject.GetComponent<TMP_Text>().text = iconname.text;
        vig.transform.GetChild(0).GetChild(2).GetChild(0).gameObject.GetComponent<TMP_Text>().text = icon_Description;
    }
}
