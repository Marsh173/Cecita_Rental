using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskConditions : MonoBehaviour
{
    [SerializeField] List<bool> conditions = new List<bool>();
    [SerializeField] int Task_num, subTask_num;

    [SerializeField] bool isSubtask = false;

    private void Update()
    {
        if (checkAllConditions())
        {
            TaskManager_Test_Yunfei tm = GameObject.FindGameObjectWithTag("TaskManager").GetComponent<TaskManager_Test_Yunfei>();
            if (isSubtask)
            {
                tm.temp_attach_to_main_num = Task_num;
                tm.SubTaskDone(subTask_num);

            }
            else tm.TaskDone(Task_num);
        }
    }

    public bool checkAllConditions()
    {
        bool checkall = true;
        for (int i = 0; i < conditions.Count; i ++)
        {
            if (conditions[i] != true) checkall = false;
        }

        return checkall;
    }
}
