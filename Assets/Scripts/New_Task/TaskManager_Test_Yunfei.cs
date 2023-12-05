using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskManager_Test_Yunfei : MonoBehaviour
{
    public List<GameObject> Tasks = new List<GameObject>();
    public GameObject TaskPrefab;

    //store temp data to work with button elements (in button it's impossible to assign more than one value in void () )
    public string temp_taskTitle;
    public string temp_task_name;
    public string temp_sub_task_name;

    public Transform TaskOrigin;

    public float y_height_task = 25;

    // ----------- assign main task - CONTAINS SETTASK ALREADY
    public void AddTask()
    {
        var t = Instantiate(TaskPrefab, gameObject.transform);
        t.transform.SetParent(transform);
        Tasks.Add(t);
        SetTask(t, temp_taskTitle, temp_task_name);
        if(temp_sub_task_name != "")
        {
            SetSubTask(t,temp_sub_task_name);
        }
        else
        {
            ClearTempData();
        }

        
        RearrangeTasks();
    }

    // ----------- assign new text to task texts - 
    public void SetTask(GameObject task, string task_Title, string task_name )
    {
        task.GetComponent<TaskData>().taskTitle = task_Title;
        task.GetComponent<TaskData>().taskName = task_name;
    }

    // ----------- assign sub task
    //must set a main task first before assigning sub tasks!!
    public void SetSubTask(GameObject maintask,string subtask_name)
    {
        maintask.GetComponent<TaskData>().addSub(subtask_name);
        ClearTempData();
    }

    //------------- completing tasks

    public void TaskDone(int index)
    {
        Tasks[index].GetComponent<TaskData>().isCompleted = true;
        Tasks.RemoveAt(index);
        RearrangeTasks();
    }

    public void SubTaskDone(int mainT, int subT)
    {
        Tasks[mainT].GetComponentInChildren<TaskData>().subtask[subT].GetComponent<Sub_Task>().isCompleted = true;
        Tasks[mainT].GetComponentInChildren<TaskData>().subtask.RemoveAt(subT);
    }

    public void RearrangeTasks()
    {
        for (int i = 0; i < Tasks.Count; i++)
        {
            Tasks[i].transform.position = new Vector3(TaskOrigin.position.x, TaskOrigin.position.y + i * y_height_task, TaskOrigin.position.z);
        }
    }

    public void ClearTempData()
    {
        temp_taskTitle = "";
        temp_sub_task_name = "";
        temp_task_name = "";
    }
}
