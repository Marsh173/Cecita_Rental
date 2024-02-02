using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TaskData :  MonoBehaviour
{
    public string taskName;
    public int task_num;
    public TextMeshProUGUI task_name_UI;
    public bool isCompleted;

    public float y_height;

    public int sub = 0;

    private void Update()
    {
        task_name_UI.text = taskName;
    }
}
