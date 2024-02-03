using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskDoneTrigger : MonoBehaviour
{
    public FinishTask_By fin;
    public TaskManager_Test_Yunfei tm;
    public TaskListTrack tlt;



    public int task_num_DONOTCHANGE = 0;
    //finishes a task

    public enum FinishTask_By
    {
        trigger,
        button,
        dialogue,
        when_tasks_done,
        none
    }

    private void Start()
    {
        if(tm == null)tm = GameObject.FindGameObjectWithTag("TaskManager").GetComponent<TaskManager_Test_Yunfei>();
        if (tlt == null) tlt = FindObjectOfType<TaskListTrack>();
    }

    private void Update()
    {
        if(fin == FinishTask_By.when_tasks_done)
        {

        }
    }

    public void Task_Done()
    {
        tlt.task_track[task_num_DONOTCHANGE-1] = true;
        StartCoroutine(tm.TaskFinish(task_num_DONOTCHANGE));
    }

}
