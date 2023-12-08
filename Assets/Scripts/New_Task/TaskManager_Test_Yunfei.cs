using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskManager_Test_Yunfei : MonoBehaviour
{
    public List<GameObject> Tasks = new List<GameObject>();
    public GameObject TaskPrefab;

    //store temp data to work with button elements (in button it's impossible to assign more than one value in void () )
   
    public string temp_task_name;
    public string temp_sub_task_name;
    public int temp_attach_to_main= 0;

    public GameObject TaskOrigin;

    public float y_height_task = -25;

    public bool Tasks_visible = true;

    public Animator ani;

    private void Start()
    {
        //tasks shows for 5 sec
        StartCoroutine(showTasks(5));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleVisibility();
        }
        
    }

    //showing tasks at the beginning
    public IEnumerator showTasks(int sec)
    {
        ani.SetBool("Open", true);
        yield return new WaitForSeconds(sec);
        ani.SetBool("Open", false);
        Tasks_visible = false;
    }

    public void ToggleVisibility()
    {
        StopAllCoroutines();
        Tasks_visible = !Tasks_visible;
        ani.SetBool("Open", Tasks_visible);
    }


    // ----------- assign main task - CONTAINS SETTASK ALREADY
    public void AddTask()
    {
        var t = Instantiate(TaskPrefab, TaskOrigin.transform);
        
        Tasks.Add(t);
        
        t.GetComponent<TaskData>().taskName = temp_task_name;

        ClearTempData();


        t.transform.SetParent(TaskOrigin.transform);
        RearrangeTasks();
    }

    // ----------- assign new text to task texts - 
    /* public void SetTask(GameObject task, string task_name )
     {

         task.GetComponent<TaskData>().taskName = task_name;
     }*/

    // ----------- assign sub task
    //must set a main task first before assigning sub tasks!!
    public void SetSubTask()
    {
        if(Tasks.Count>temp_attach_to_main)
        {
            var maintask = Tasks[temp_attach_to_main];
            maintask.GetComponent<TaskData>().addSub(temp_sub_task_name);
        }

        RearrangeTasks();

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
            var t = Tasks[i];
            var origin = TaskOrigin.GetComponent<RectTransform>().anchoredPosition;
            
            var y_shift =  i * y_height_task;

            if (i >0)
            {
                for (int u = 0; u < Tasks.Count - 1; u++) 
                {
                    y_shift +=  Tasks[u].GetComponent<TaskData>().subtask.Count * y_height_task;
                }
            }
            var t_p = t.GetComponent<RectTransform>().anchoredPosition;
            t.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0 - y_shift , 0);
        }
    }

    public void ClearTempData()
    {
        temp_sub_task_name = "";
        temp_task_name = "";
        temp_attach_to_main = -1;
    }
}
