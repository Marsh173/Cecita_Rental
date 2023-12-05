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
    public bool isCompleted;

    public float y_height;

    //add sub and give them a name
    public void addSub(string subtask_name)
    {
        var s = Instantiate(subtaskPrefab, gameObject.transform);
        s.transform.SetParent(gameObject.transform);
        subtask.Add(s);
        s.GetComponent<Sub_Task>().sub_taskName = subtask_name;
        s.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,-75,0);
    }

    private void Update()
    {
        task_title_UI.text = taskTitle;
        task_name_UI.text = taskName;

        if (isCompleted)
        {
            FinishTask();
        }
    }

    public void FinishTask()
    {
        //finish all sub tasks

        //finish main task
        Destroy(gameObject);
    }

    public void updateHeight()
    {

    }
}
