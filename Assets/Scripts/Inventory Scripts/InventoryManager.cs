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
    public List<Documents> DItems = new List<Documents>();
    
    //Audio list
    public AudioSource audioSource;
    public AudioClip[] soundArray;
    public AudioClip sound;
    public int audioIndex = 0;

    public Transform PlaylistBroadcastContent, PlaylistMonsterContent, NormalItemContent, DocumentContent;
    public GameObject PlaylistItem, NormalItem, DocumentItem;
    public KeyCode inventoryKey;
    public GameObject Inventory;

    //Find Specific Item
    public static bool EquipmentCollected, RecorderCollected, EarbudsCollected = false;
    public static bool ThirdFloorElevatorCardCollected, keyCollected = false;

    public InspectionCameraTransition inspectScript;
    public GameObject inspectionScreen;
    private void Awake()
    {
        Inventory.SetActive(false);
        
        //add all pre-made display into a list for further reference
        /*foreach (Transform child in NormalItemContent.transform)
        {
            NItemDisplayObjs.Add(child.gameObject);
        }*/

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

    public void AddDocuments(Documents Item)
    {
        DItems.Add(Item);
    }

    private void Update()
    {
        if (Input.GetKeyDown(inventoryKey))
        {
            if(Inventory.activeSelf) //Inventory.transform.position.y == 540
            {
                Inventory.SetActive(false);
                //Inventory.transform.position = new Vector2(960, -1000);

                //if inspect script is in scene, check the state of it first
                if (inspectScript != null)
                {
                    if (!InspectionCameraTransition.isInCam) 
                    {
                        FirstPersonAIO.instance.enableCameraMovement = true;
                        FirstPersonAIO.instance.playerCanMove = true;

                        //hide cursor when close inventory 
                        Cursor.visible = false;
                    }
                    else
                    {
                        //disable inspection screen when opening inventory in inspection cam
                        inspectionScreen.SetActive(true);
                    }
                }
                //if inspect script is not in scene, do it normally
                else
                {
                    FirstPersonAIO.instance.enableCameraMovement = true;
                    FirstPersonAIO.instance.playerCanMove = true;
                    Cursor.visible = false;
                }
            }
            else
            {
                //show cursor when open inventory 
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;

                Inventory.SetActive(true);
                //Inventory.transform.position = new Vector2(960, 540);

                FirstPersonAIO.instance.enableCameraMovement = false;
                FirstPersonAIO.instance.playerCanMove = false;

                //List every item each time the inventory is opened
                ListItems();

                //disable inspection screen when opening inventory in inspection cam
                if (inspectScript != null)
                {
                    if (InspectionCameraTransition.isInCam)
                    {
                        inspectionScreen.SetActive(false);
                    }
                }
            }
        }

        if(!RecorderCollected || !EarbudsCollected)
        {
            FindEquipment();
        }
        else
        {
            EquipmentCollected = true;
        }

        /*
        if (!ThirdFloorElevatorCardCollected)
        {
            FindThirdFloorElevatorCard();
        }*/

        if(!keyCollected)
        {
            TempKeyUnlock();
        }
    }

    public void ListItems()
    {
        //clean audio inventory before listing the items
        foreach (Transform item in PlaylistBroadcastContent)
        {
            if (AItems.Find(item => item.name == item.name))
            {
                Destroy(item.gameObject);
            }
        }

        foreach (var item in AItems)
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

            itemobj.GetComponentInChildren<HoverInventory>().transcript = item.Atranscript;
        }


        //clean normal inventory before listing the items
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
            var itemButton = itemobj.transform.Find("Button").GetComponent<InventoryViewItem>();

            //display name and image in inventory UI
            itemButton.iconname.text =  itemName.text = item.displayName;
            itemButton.icon_Description = item.descriptions;
            itemIcon.sprite = item.icon;

            

            if(itemName.text == "Bluetooth Earbuds" || itemName.text == "Recorder")
            {
                Destroy(itemobj);
            }
        }


        foreach (Transform item in DocumentContent)
        {
            if (DItems!= null && DItems.Find(item => item.name == item.name))
            {
                Destroy(item.gameObject);
            }
        }

        foreach (var item in DItems)
        {
            //find the elemets in each item and replace inventory default
            GameObject itemobj = Instantiate(DocumentItem, DocumentContent);
            var itemName = itemobj.transform.Find("itemName").GetComponent<TMP_Text>();
            //var itemTranscript = itemobj.transform.Find("transcript").GetComponent<TMP_Text>();
            var itemIcon = itemobj.transform.Find("image").GetComponent<Image>();

            //display name and image in inventory UI
            itemName.text = item.displayName;
            //itemTranscript.text = item.transcript;
            itemIcon.sprite = item.image;

            Debug.Log("Doc Added");
        }
    }

    //detect if earbud and recorder are collected
    public void FindEquipment()
    {
        if(NItems.Find(item => item.name == "recorder"))
        {
            RecorderCollected = true;
        }

        if(NItems.Find(item => item.name == "ear buds"))
        {
            EarbudsCollected = true;
        }
    }

    public void FindThirdFloorElevatorCard()
    {
        if (NItems.Find(item => item.name == "ElevatorCard"))
        {
            ThirdFloorElevatorCardCollected = true;
            //Debug.Log("Got Elevator card " + ThirdFloorElevatorCardCollected);
        }
        else ThirdFloorElevatorCardCollected = false;
    }

    public void TempKeyUnlock()
    {
        if (NItems.Find(item => item.name == "Night 2 Key"))
        {
            keyCollected = true;
        }
    }

}
