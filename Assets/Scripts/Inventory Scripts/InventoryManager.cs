using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<PlaylistItems> AItems = new List<PlaylistItems>();
    public List<NormalItems> NItems = new List<NormalItems>();
    //Audio list
    public AudioSource audioSource;
    public AudioClip[] soundArray;
    public AudioClip sound;
    public int audioIndex = 0;

    public Transform PlaylistBroadcastContent, PlaylistMonsterContent, NormalItemContent;
    public GameObject PlaylistItem, NormalItem;
    public KeyCode inventoryKey;
    public GameObject Inventory;

    //Find Specific Item
    public static bool equipmentCollected = false;
    public static bool ThirdFloorElevatorCardCollected = false;

    private void Awake()
    {
        Inventory.SetActive(false);
        Instance = this;
        //hide cursor at game start
        Cursor.visible = false;
    }

    public void AddPlaylist(PlaylistItems Item)
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
                FirstPersonAIO.instance.enableCameraMovement = true;
                FirstPersonAIO.instance.playerCanMove = true;

                //hide cursor when close inventory 
                Cursor.visible = false;
            }
            else if(!Inventory.activeSelf)
            {
                Inventory.SetActive(true);
                FirstPersonAIO.instance.enableCameraMovement = false;
                FirstPersonAIO.instance.playerCanMove = false;

                //List every item each time the inventory is opened
                ListItems();

                //show cursor when open inventory 
                Cursor.visible = true;
            }
        }

        if(!equipmentCollected)
        {
            FindEquipment();
        }

        if (!ThirdFloorElevatorCardCollected)
        {
            FindThirdFloorElevatorCard();
        }
    }

    public void ListItems()
    {
        //clean inventory before listing the items
        foreach(Transform item in PlaylistBroadcastContent)
        {
            Destroy(item.gameObject);
        }

        foreach(var item in AItems)
        {
            //find the elemets in each item and replace inventory default
            GameObject itemobj = Instantiate(PlaylistItem, PlaylistBroadcastContent);
            var itemName = itemobj.transform.Find("itemName").GetComponent<TMP_Text>();
            var itemLength = itemobj.transform.Find("audioLength").GetComponent<TMP_Text>();
            var itemIcon = itemobj.transform.Find("icon").GetComponent<Image>();
            var itemAudio = itemobj.transform.Find("audio").GetComponent<AudioSource>();

            //display name and image in inventory UI
            itemName.text = item.displayName;
            itemLength.text = item.audioLength;
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

    //detect if earbud and recorder are collected
    public void FindEquipment()
    {
        if(NItems.Find(item => item.name == "ear buds") && NItems.Find(item => item.name == "recorder"))
        {
            equipmentCollected = true;
            //Debug.Log("Equiped " + equipmentCollected);
        }
    }

    public void FindThirdFloorElevatorCard()
    {
        if (NItems.Find(item => item.name == "ElevatorCard"))
        {
            ThirdFloorElevatorCardCollected = true;
            //Debug.Log("Got Elevator card " + ThirdFloorElevatorCardCollected);
        }
    }

}
