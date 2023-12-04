using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskManager_Test_Yunfei : MonoBehaviour
{
    public List<GameObject> Tasks = new List<GameObject>();
    public GameObject TaskPrefab;

    public string temp_taskTitle;
    public string temp_task_name;
    public string temp_sub_task_name;
    public int temp_sub_task_num;
    

    public void AddTask()
    {
        var t = Instantiate(TaskPrefab, gameObject.transform);
        t.transform.SetParent(transform);
        Tasks.Add(t);
        SetTask(t, temp_taskTitle, temp_task_name);
        SetSubTask(t,temp_sub_task_name,temp_sub_task_num);
    }

    public void SetTask(GameObject task, string task_Title, string maintask )
    {
        
    }

    public void SetSubTask(GameObject task,string subtask_name, int subTaskNum)
    {
        
    }

    public void TaskDone(int index)
    {
        Tasks[index].GetComponent<TaskData>().isCompleted = true;
    }

    public void SubTaskDone(int mainT, int subT)
    {
        Tasks[mainT].GetComponent<TaskData>().subtask[subT].GetComponent<Sub_Task>().isCompleted = true;
    }
}
