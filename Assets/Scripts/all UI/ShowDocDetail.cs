using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowDocDetail : MonoBehaviour
{
    public string title, transcript;
    public Sprite image;
    public GameObject titleObj, transcriptObj, imageObj;
    GameObject[] objectsWithTag;

    void Start()
    {
        objectsWithTag = GameObject.FindGameObjectsWithTag("");

        // Iterate over each GameObject found
        foreach (GameObject obj in objectsWithTag)
        {
            if(obj.name == "") titleObj = obj;
            if (obj.name == "") transcriptObj = obj;
            if (obj.name == "") imageObj = obj;
        }
    }
    public void ShowDocumentDetails()
    {
        titleObj.GetComponent<TMP_Text>().text = title;
        transcriptObj.GetComponent<TMP_Text>().text = transcript;
        imageObj.GetComponent<Image>().sprite = image;
    }
}