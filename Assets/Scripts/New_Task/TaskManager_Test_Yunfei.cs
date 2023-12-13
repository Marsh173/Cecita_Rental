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
    public int temp_attach_to_main_num = 0;
    public int temp_main_num = 0;
    public int temp_sub_num = 0;

    public GameObject TaskOrigin, PressT;

    public float y_height_task = -25;

    public bool Tasks_visible = true;

    public Animator ani;

    private void Start()
    {
        PressT.SetActive(false);

        //show task after cutscene
        if(CutSceneScript.cutsceneEnd)
        {
            //tasks shows for 5 sec
            StartCoroutine(showTasks(8));
        }
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
        PressT.SetActive(true);
        Tasks_visible = false;
    }

    public void ToggleVisibility()
    {
        PressT.SetActive(false);
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
        t.GetComponent<TaskData>().task_num = temp_main_num;

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
        
        GameObject maintask = getMaintask(temp_attach_to_main_num);

        if(maintask != null)maintask.GetComponent<TaskData>().addSub(temp_sub_task_name, temp_sub_num);

        RearrangeTasks();

        ClearTempData();
    }

    //------------- completing tasks

    public void TaskDone(int index)
    {
        /*Tasks[index].GetComponent<TaskData>().isCompleted = true;
        Tasks.RemoveAt(index);*/
      
        GameObject task = getMaintask(index);

        if (task != null)
        {
            task.GetComponent<TaskData>().isCompleted = true;
            Tasks.RemoveAt(Tasks.IndexOf(task));
            RearrangeTasks();
        }
        
    }

    public void SubTaskDone(int mainT, int subT)
    {
        GameObject subtask = getSubtask(mainT, subT);
        subtask.GetComponent<Sub_Task>().isCompleted = true;
        List<GameObject> sublist = getMaintask(mainT).GetComponent<TaskData>().subtask;
        sublist.RemoveAt(sublist.IndexOf(subtask));
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
            t.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 70 - y_shift , 0);
        }
    }

    public void ClearTempData()
    {
        temp_sub_task_name = "";
        temp_task_name = "";
        temp_attach_to_main_num = 0;
        temp_main_num = 0;
        temp_sub_num = 0;

    }

    public GameObject getMaintask(int taskNum)
    {
        GameObject task = null;
        for (int i = 0; i < Tasks.Count; i++)
        {
            if(Tasks[i].GetComponent<TaskData>().task_num == taskNum)
            {
                task =  Tasks[i].gameObject;
                break;
            }

        }

        return task;
    }

    public GameObject getSubtask(int taskNum, int subNum)
    {

        GameObject mainTask = getMaintask(taskNum);
        GameObject subTask = null;

        List<GameObject> list = mainTask.GetComponent<TaskData>().subtask;

        if(mainTask != null)
        {
            for (int i = 0; i < list.Count; i ++)
            {
                if (list[i].GetComponent<Sub_Task>().sub_num == subNum) subTask = list[i]; 
            }
        }

        return subTask;
    }
}
