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
    
    public List<GameObject> subtask = new List<GameObject>();
    //public GameObject subtaskPrefab;
    public bool started = false; // turn this to true when all sub tasks are added
    public bool isCompleted;

    public float y_height;

    //add sub and give them a name and a number
    /*public void addSub(string subtask_name, int sub_num)
    {
        var s = Instantiate(subtaskPrefab, transform);
        s.transform.SetParent(transform);
        subtask.Add(s);
        s.GetComponent<Sub_Task>().sub_taskName = subtask_name;
        var sublistHeight = subtask.Count * y_height;
        s.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -  sublistHeight,0);
        s.GetComponent<Sub_Task>().sub_num = sub_num;
    }*/

    private void Update()
    {
        task_name_UI.text = taskName;

        if (isCompleted)
        {
           
        }
    }

    public void updateHeight()
    {

    }
}
