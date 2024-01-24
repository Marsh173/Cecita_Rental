using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskAssignTrigger : MonoBehaviour
{
    public AssignTask_By assign;

    [SerializeField] public string task_name;
    [SerializeField] public string sub_task_name;
    [SerializeField] public int MainTask_num;
    [SerializeField] public int SubTask_num;

    [SerializeField] private string CollisionTag;
    [SerializeField] private bool addmaintask = false;
    [SerializeField] private bool addsubtask = false;
    [SerializeField] private bool finishmaintask = false;
    [SerializeField] private bool finishsubtask = false;

    private TaskManager_Test_Yunfei tm;

    private void Start()
    {
        tm = GameObject.FindGameObjectWithTag("TaskManager").GetComponent<TaskManager_Test_Yunfei>();
    }

    public void settext()
    {
        tm.temp_task_name = task_name;
        tm.temp_sub_task_name = sub_task_name;
        tm.temp_attach_to_main_num = MainTask_num;
        tm.temp_main_num = MainTask_num;
        tm.temp_sub_num = SubTask_num;
    }

    public enum AssignTask_By
    {
        trigger,
        button,
        dialogue
        }

    public void Set_Tasks()
    {
        if(addmaintask) tm.AddTask_Script(task_name, MainTask_num);
        if(addsubtask) tm.SetSubTask_Script(sub_task_name, MainTask_num);
        if (finishmaintask) tm.TaskDone_ByName(task_name);
        if (finishsubtask) tm.SubTaskDone_Script(SubTask_num, MainTask_num);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == CollisionTag)
        {
            Set_Tasks();
        }
    }
}
