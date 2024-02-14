using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//[ExecuteInEditMode]
public class InventoryViewItem : MonoBehaviour
{
    //if there's an object, set the view item active and pull the model
    [SerializeField] TMP_Text iconname;
    [SerializeField, TextArea(3, 10)] string icon_Description = "";
    [SerializeField] int index;
    [SerializeField] GameObject vig;

    public NormalItems NItems;

    private void Awake()
    {
        vig = GameObject.FindGameObjectWithTag("ViewItemGraphic");
        //NItem.displayName;
    }
    public void ViewItem()
    {
        Debug.Log("viewed");
        Debug.Log("name? " + vig.transform.GetChild(0).GetChild(1).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text);
        Debug.Log("name: " + iconname.text + " Des:" + icon_Description);
        
        //if (iconname != null)
        //{
            vig.transform.GetChild(0).gameObject.SetActive(true);
            vig.GetComponentInChildren<UIInspectCamController>().index_obj = index;
            vig.GetComponentInChildren<UIInspectCamController>().changeOBJ();
        /*foreach (var item in NItems)
        {
            //find the elemets in each item and replace inventory default
            GameObject itemobj = Instantiate(NormalItem, NormalItemContent);
            var itemName = itemobj.transform.Find("itemName").GetComponent<TMP_Text>();

            //display name and image in inventory UI
            iconname.text = item.displayName;
            icon_Description = item.descriptions;
        }*/
            vig.transform.GetChild(0).GetChild(1).GetChild(1).gameObject.GetComponent<TMP_Text>().text = iconname.text;
            vig.transform.GetChild(0).GetChild(1).GetChild(1).gameObject.GetComponent<TMP_Text>().text = icon_Description;
        vig.transform.GetChild(0).GetChild(2).GetChild(0).gameObject.GetComponent<TMP_Text>().text = icon_Description;
        //}
    }
}
