using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<playlistItems> AItems = new List<playlistItems>();
    public List<NormalItems> NItems = new List<NormalItems>();

    public Transform PlaylistItemContent, NormalItemContent;
    public GameObject PlaylistItem, NormalItem;
    public KeyCode inventoryKey;
    public GameObject Inventory;

    public bool equipmentCollected = false;

    private void Awake()
    {
        Inventory.SetActive(false);
        Instance = this;
        //hide cursor at game start
        Cursor.visible = false;
    }

    public void AddPlaylist(playlistItems Item)
    {
        AItems.Add(Item);
    }

    public void AddNormal(NormalItems Item)
    {
        NItems.Add(Item);
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
        foreach(Transform item in PlaylistItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach(var item in AItems)
        {
            //find the elemets in each item and replace inventory default
            GameObject itemobj = Instantiate(PlaylistItem, PlaylistItemContent);
            var itemName = itemobj.transform.Find("itemName").GetComponent<TMP_Text>();
            var itemIcon = itemobj.transform.Find("icon").GetComponent<Image>();
            var itemAudio = itemobj.transform.Find("audio").GetComponent<AudioSource>();

            //display name and image in inventory UI
            itemName.text = item.displayName;
            itemIcon.sprite = item.icon;
            itemAudio.clip = item.audio;
        }


        //clean inventory before listing the items
        foreach (Transform item in NormalItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in NItems)
        {
            //find the elemets in each item and replace inventory default
            GameObject itemobj = Instantiate(NormalItem, NormalItemContent);
            var itemName = itemobj.transform.Find("itemName").GetComponent<TMP_Text>();
            var itemIcon = itemobj.transform.Find("icon").GetComponent<Image>();

            //display name and image in inventory UI
            itemName.text = item.displayName;
            itemIcon.sprite = item.icon;
        }
    }

    public void FindEquipment()
    {
        if(NItems.Find(item => item.name == "ear buds") && NItems.Find(item => item.name == "recorder"))
        {
            equipmentCollected = true;
        }
    }

}
