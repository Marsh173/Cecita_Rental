using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AudioItemHighlight : MonoBehaviour
{
    [SerializeField] GameObject highlighttxt,nametxt;

    private void Update()
    {
        if(nametxt.GetComponent<TextMeshProUGUI>() != null && highlighttxt.GetComponent<TextMeshProUGUI>() != null)
        {
            highlighttxt.GetComponent<TextMeshProUGUI>().text = nametxt.GetComponent<TextMeshProUGUI>().text;
        }
    }

    private void Start()
    {
        hideHighlight();
    }
    public void hideHighlight()
    {
        highlighttxt.GetComponent<TextMeshProUGUI>().enabled = false;
    }

    public void showHighlight()
    {
        highlighttxt.GetComponent<TextMeshProUGUI>().enabled = true;
        AudioItemHighlight[] a = FindObjectsOfType(typeof(AudioItemHighlight)) as AudioItemHighlight[];
        for (int i = 0; i < a.Length; i ++)
        {
            if(a[i] != this)a[i].hideHighlight();
        }
    }
}
