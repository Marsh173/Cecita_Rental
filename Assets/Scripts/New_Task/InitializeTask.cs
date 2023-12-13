using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeTask : MonoBehaviour
{
    [SerializeField]public string taskTitle;
    [SerializeField] public string task_name;
    [SerializeField] public string sub_task_name;
    //[SerializeField] public int index_of_MainTask;
    [SerializeField] public int MainTask_num; 
    [SerializeField] public int SubTask_num; 

    public TaskManager_Test_Yunfei tm;
    private void Start()
    {
        tm = GameObject.FindGameObjectWithTag("TaskManager").GetComponent<TaskManager_Test_Yunfei>();
    }

    public void settext()
    {
        tm.temp_task_name = task_name;
        tm.temp_sub_task_name = sub_task_name;
        //tm.temp_attach_to_main_num = index_of_MainTask;
        tm.temp_main_num = MainTask_num;
        tm.temp_sub_num = SubTask_num;
    }
}
