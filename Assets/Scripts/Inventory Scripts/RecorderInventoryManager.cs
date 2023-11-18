using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using UnityEditor.Experimental.GraphView;

public class RecorderInventoryManager : MonoBehaviour
{
    public static RecorderInventoryManager Instance;
    public List<PlaylistItems> AItems = new List<PlaylistItems>();
    //Audio list
    public AudioSource audioSource;
    public AudioClip[] soundArray;
    public AudioClip sound;
    public int audioIndex = 0;

    public Transform PlaylistItemContent;
    public GameObject PlaylistItem;
    public GameObject Inventory;

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

    private void Update()
    {

        // recorder inventory activate
        if (Input.GetAxis("Mouse ScrollWheel") != 0f && PlayerInteract.hasRecorderInHand)
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0f && Inventory.activeSelf) // backwards
            {
                Inventory.SetActive(false);
                FirstPersonAIO.instance.enableCameraMovement = true;
                FirstPersonAIO.instance.playerCanMove = true;

                //hide cursor when close inventory 
                Cursor.visible = false;
                Debug.Log("Close tape");
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                Inventory.SetActive(true);
                FirstPersonAIO.instance.enableCameraMovement = false;
                FirstPersonAIO.instance.playerCanMove = false;

                //List every item each time the inventory is opened
                ListItems();

                //show cursor when open inventory 
                Cursor.visible = true;
                Debug.Log("Open tape");
            }
        }

        //
    }

    public void ListItems()
    {
        //clean inventory before listing the items
        foreach (Transform item in PlaylistItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in AItems)
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

        /*
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
        */
    }
}
