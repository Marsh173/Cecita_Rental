using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskData :  MonoBehaviour
{
    public string taskTitle;
    public string taskName;

    public TextMeshProUGUI task_title_UI;
    public TextMeshProUGUI task_name_UI;

    
    public List<GameObject> subtask = new List<GameObject>();
    public GameObject subtaskPrefab;
    public bool isCompleted;

    public void addSub()
    {
        var s = Instantiate(subtaskPrefab);
        s.transform.SetParent(gameObject.transform);
        subtask.Add(s);
    }

    private void Update()
    {
        task_title_UI.text = taskTitle;
        task_name_UI.text = taskName;
    }
}
