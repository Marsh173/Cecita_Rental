using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskAssignTrigger : MonoBehaviour
{
    public AssignTask_By assign;
    public Task_Type type;

    public string task_name;
    public int task_num_DONOTCHANGE = 0;
    public int attach_to_number = 0;

    private TaskManager_Test_Yunfei tm;

    private void Start()
    {
        if (tm == null) tm = GameObject.FindGameObjectWithTag("TaskManager").GetComponent<TaskManager_Test_Yunfei>();

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
        switch (type)
        {
            case Task_Type.Main_Task:
                tm.AddTask(task_num_DONOTCHANGE,task_name);
                break;

            case Task_Type.Sub_Task:
                tm.AddTask_Attach(attach_to_number,task_num_DONOTCHANGE, task_name,1);

                break;

            case Task_Type.Sub_Sub_Task:
                tm.AddTask_Attach(attach_to_number, task_num_DONOTCHANGE, task_name,2);
                break;
        }

        if (assign == AssignTask_By.trigger) GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<TaskAssignTrigger>().enabled = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Set_Tasks();
        }
    }
}
