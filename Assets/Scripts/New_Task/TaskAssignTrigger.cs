using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskAssignTrigger : MonoBehaviour
{
    public AssignTask_By assign;
    public Task_Type type;

    public string task_name;

    public int attach_to_number = 0;

    private TaskManager_Test_Yunfei tm;

    private void Start()
    {
        tm = GameObject.FindGameObjectWithTag("TaskManager").GetComponent<TaskManager_Test_Yunfei>();
        if (assign == AssignTask_By.add_when_awake)
        {
            Set_Tasks();
        }
    }

    public enum AssignTask_By
    {
        trigger,
        button,
        dialogue,
        add_when_awake,
        when_tasks_done,
        none
        }

    public enum Task_Type
    {
        Main_Task,Sub_Task, Sub_Sub_Task,none
    }

    public void Set_Tasks()
    {
      

    }

}
