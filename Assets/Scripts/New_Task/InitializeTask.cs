using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeTask : MonoBehaviour
{
    [SerializeField]public string taskTitle;
    [SerializeField] public string task_name;
    [SerializeField] public string sub_task_name;

    public TaskManager_Test_Yunfei tm;
    private void Start()
    {
        tm = GameObject.FindGameObjectWithTag("TaskManager").GetComponent<TaskManager_Test_Yunfei>();
    }

    public void settext()
    {
        tm.temp_taskTitle = taskTitle;
        tm.temp_task_name = task_name;
        tm.temp_sub_task_name = sub_task_name;
    }
}
