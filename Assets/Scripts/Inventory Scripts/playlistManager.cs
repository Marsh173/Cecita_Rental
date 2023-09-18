using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playlistManager : MonoBehaviour
{
    public static playlistManager Instance;
    public List<playlistItems> AItems = new List<playlistItems>();
    //public List<NormalItems> NItems = new List<NormalItems>();

    public Transform ItemContent;
    public GameObject InventoryItem;
    public KeyCode inventoryKey;
    public GameObject Inventory;

    private void Awake()
    {
        Inventory.SetActive(false);
        Instance = this;
        //hide cursor at game start
        Cursor.visible = false;
    }

    public void Add(playlistItems Item)
    {
        AItems.Add(Item);
    }

    private void Update()
    {
        if (Input.GetKeyDown(inventoryKey))
        {
            if(Inventory.activeSelf)
            {
                Inventory.SetActive(false);

                //hide cursor when close inventory 
                Cursor.visible = false;
            }
            else if(!Inventory.activeSelf)
            {
                Inventory.SetActive(true);

                //List every item each time the inventory is opened
                ListItems();

                //show cursor when open inventory 
                Cursor.visible = true;
            }
        }
    }

    public void ListItems()
    {
        //clean inventory before listing the items
        foreach(Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach(var item in AItems)
        {
            //find the elemets in each item and replace inventory default
            GameObject itemobj = Instantiate(InventoryItem, ItemContent);
            var itemName = itemobj.transform.Find("itemName").GetComponent<TMP_Text>();
            var itemIcon = itemobj.transform.Find("icon").GetComponent<Image>();
            var itemAudio = itemobj.transform.Find("audio").GetComponent<AudioSource>();

            //display name and image in inventory UI
            itemName.text = item.displayName;
            itemIcon.sprite = item.icon;
            itemAudio.clip = item.audio;
        }
    }

}
