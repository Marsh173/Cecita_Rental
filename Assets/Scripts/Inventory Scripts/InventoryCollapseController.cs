using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCollapseController : MonoBehaviour
{
    [SerializeField] GameObject audio_viewport, monster_viewport;
    [SerializeField] GameObject audio_title, monster_title, monsterspe_title;

    [SerializeField] RectTransform audio_t, monster_t, monsterspe_t;

    public void expandViewport(string catagory)
    {
        if(catagory == "Audio")
        {
            audio_viewport.SetActive(true);
            monster_title.GetComponent<RectTransform>().anchoredPosition = new Vector3(audio_t.anchoredPosition.x, audio_t.anchoredPosition.y - 100, 0);
            monsterspe_t.anchoredPosition = new Vector3(audio_t.anchoredPosition.x, monster_t.anchoredPosition.y - 25, 0);
        }
        else if(catagory == "Monster")
        {
            monster_viewport.SetActive(true);
            monsterspe_title.SetActive(true);
            //monsterspe_t.anchoredPosition = new Vector3(audio_t.anchoredPosition.x, monster_t.anchoredPosition.y - 25, 0);
        }
    }

    public void collapseViewport(string catagory)
    {
        if (catagory == "Audio")
        {
            audio_viewport.SetActive(false);
            monster_title.GetComponent<RectTransform>().anchoredPosition = new Vector3(audio_t.anchoredPosition.x, audio_t.anchoredPosition.y + 175, 0);
            monsterspe_t.anchoredPosition = new Vector3(audio_t.anchoredPosition.x, monster_t.anchoredPosition.y - 25, 0);
        }
        else if (catagory == "Monster")
        {
            monster_viewport.SetActive(false);
            monsterspe_title.SetActive(false);
        }
    }


}
