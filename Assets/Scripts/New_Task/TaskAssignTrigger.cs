using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskAssignTrigger : MonoBehaviour
{
    public AssignTask_By assign;

    private string task_name;
    private string sub_task_name;
    private int MainTask_num;
    private int SubTask_num;

    private string CollisionTag;
    private bool addmaintask = false;
    private bool addsubtask = false;
    private bool finishmaintask = false;
    private bool finishsubtask = false;

    [SerializeField] private bool destroy_after_trigger = false;
    [SerializeField] private bool disable_after_trigger = false;

    private TaskManager_Test_Yunfei tm;

    private void Start()
    {
        tm = GameObject.FindGameObjectWithTag("TaskManager").GetComponent<TaskManager_Test_Yunfei>();
        if (assign == AssignTask_By.add_when_awake)
        {
            Set_Tasks();
            if (destroy_after_trigger) Destroy(gameObject);
            if (disable_after_trigger) this.enabled = false;
        }
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
        dialogue,
        add_when_awake
        }

    public void Set_Tasks()
    {
        if (addmaintask) tm.AddTask_Script(task_name, MainTask_num);
        if (addsubtask) tm.SetSubTask_Script(sub_task_name, MainTask_num);
        if (finishmaintask) 
        {
            //tm.TaskDone(MainTask_num);
            tm.TaskDone_ByName(task_name);
        }
        
        
        if (finishsubtask) tm.SubTaskDone_Script(SubTask_num, MainTask_num);

        if (addmaintask || addsubtask|| finishmaintask || finishsubtask)
        {
            if (destroy_after_trigger) Destroy(gameObject);
            if (disable_after_trigger) this.enabled = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == CollisionTag)
        {
            Set_Tasks();
        }
    }
}
