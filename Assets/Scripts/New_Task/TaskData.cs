using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TaskData :  MonoBehaviour
{
    public string taskTitle;
    public string taskName;

    public TextMeshProUGUI task_title_UI;
    public TextMeshProUGUI task_name_UI;
    
    public List<GameObject> subtask = new List<GameObject>();
    public GameObject subtaskPrefab;
    public bool started = false; // turn this to true when all sub tasks are added
    public bool isCompleted;

    public float y_height;

    //add sub and give them a name
    public void addSub(string subtask_name)
    {
        var s = Instantiate(subtaskPrefab, transform);
        s.transform.SetParent(transform);
        subtask.Add(s);
        s.GetComponent<Sub_Task>().sub_taskName = subtask_name;
        var sublistHeight = subtask.Count * y_height;
        s.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -  sublistHeight,0);
    }

    private void Update()
    {
        //task_title_UI.text = taskTitle;
        task_name_UI.text = taskName;

        if(started)isCompleted = allSubFinished();

        if (isCompleted)
        {
            FinishTask();
        }
    }

    public bool allSubFinished()
    {
        if(subtask.Count == 0)
        return true;
        else
        {
            return false;
        }
    }

    public void FinishTask()
    {
        //finish all sub tasks

        //finish main task
        for (int i = 0; i < subtask.Count; i++)
        {
            subtask[i].GetComponent<Sub_Task>().isCompleted = true;
        }
        Destroy(gameObject);
    }

    public void updateHeight()
    {

    }
}
