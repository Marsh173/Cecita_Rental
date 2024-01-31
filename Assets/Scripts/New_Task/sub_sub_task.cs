using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class sub_sub_task : MonoBehaviour
{
    public int sub_sub_num = 0;
    public string sub_sub_name = "";
    public TextMeshProUGUI nametext;
    public bool isCompleted = false;

    private void Start()
    {
        nametext.text = sub_sub_name; 
    }
}
