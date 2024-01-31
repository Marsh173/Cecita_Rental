using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskAssignTrigger : MonoBehaviour
{
    public AssignTask_By assign;

    private string task_name;
    private string sub_task_name;
    private string subsubtask_name;
    private int MainTask_num;
    private int SubTask_num;
    private int subsubtask_num;
    
    private string CollisionTag;
    private bool addmaintask = false;
    private bool addsubtask = false;
    private bool addsubsubtask = false;
    private bool finishmaintask = false;
    private bool finishsubtask = false;
    private bool finishsubsubtask = false;

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

    public enum AssignTask_By
    {
        trigger,
        button,
        dialogue,
        add_when_awake
        }

    public void Set_Tasks()
    {
        if (addmaintask) tm.AddTask(MainTask_num,task_name);
        if (addsubtask) tm.SetSubTask(SubTask_num, MainTask_num, sub_task_name);
        if (addsubsubtask) tm.SetSubsubTask(MainTask_num, SubTask_num, subsubtask_num, subsubtask_name);
        if (finishmaintask) 
        {
            tm.TaskDone(MainTask_num);
        }
        
        
        if (finishsubtask) tm.SubTaskDone(SubTask_num, MainTask_num);
        if (finishsubsubtask) tm.SubsubTaskDone(SubTask_num, MainTask_num,subsubtask_num);

        if (addmaintask || addsubtask|| finishmaintask || finishsubtask||addsubsubtask||finishsubsubtask)
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
