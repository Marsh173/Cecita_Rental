using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskManager_Test_Yunfei : MonoBehaviour
{
    public List<GameObject> Tasks = new List<GameObject>();
    public GameObject TaskPrefab;

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
    public void AddTask(int tasknum, string taskname)
    {
        var t = Instantiate(TaskPrefab, TaskOrigin.transform);

        Tasks.Add(t);

        t.GetComponent<TaskData>().taskName = taskname;
        t.GetComponent<TaskData>().task_num = tasknum;


        t.transform.SetParent(TaskOrigin.transform);
        RearrangeTasks();
    }
    public void AddTask_Script(string task_name, int task_num)
    {
        var t = Instantiate(TaskPrefab, TaskOrigin.transform);

        Tasks.Add(t);

        t.GetComponent<TaskData>().taskName = task_name;
        t.GetComponent<TaskData>().task_num = task_num;
        t.transform.SetParent(TaskOrigin.transform);
        RearrangeTasks();
    }
    public void SetSubTask(int subnum, int mainnum, string subname)
    {
        
        GameObject maintask = getMaintask(mainnum);

        if(maintask != null)maintask.GetComponent<TaskData>().addSub(subname, subnum);

        RearrangeTasks();
    }

    public void SetSubsubTask(int mainnum, int subnum, int subsubnum, string subsubname)
    {
        GameObject subtask = getSubtask(mainnum,subnum);
        subtask.GetComponent<Sub_Task>().addSubSub(subsubname,subsubnum);
    }
    //------------- completing tasks

    public void TaskDone(int index)
    {

        GameObject task = getMaintask(index);

        if (task != null)
        {
            task.GetComponent<TaskData>().isCompleted = true;

            //add task done animation and open task window
            Tasks.RemoveAt(Tasks.IndexOf(task));
            RearrangeTasks();
        }

    }

    public void SubTaskDone(int subnum,int mainnum)
    {
        GameObject subtask = getSubtask(mainnum, subnum);
        subtask.GetComponent<Sub_Task>().isCompleted = true;
    }

    public void SubsubTaskDone(int subnum, int mainnum, int subsubnum)
    {
        GameObject subsubtask = getSubsubtask(mainnum, subnum,subsubnum);
        subsubtask.GetComponent<sub_sub_task>().isCompleted = true;
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

    public GameObject getSubsubtask(int taskNum, int subNum, int subsubnum)
    {
        GameObject subtask = getSubtask(taskNum,subNum);
        GameObject subsubtask = null;
        List<GameObject> list = subtask.GetComponent<Sub_Task>().subsubtask;
        if (subtask != null)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].GetComponent<Sub_Task>().sub_num == subNum) subsubtask = list[i];
            }
        }

        return subsubtask;
    }

        public int getSubtaskIndex(int taskNum, int subNum)
    {
        GameObject mainTask = getMaintask(taskNum);
        int subtaskindex = 0;
        List<GameObject> list = mainTask.GetComponent<TaskData>().subtask;

        if (mainTask != null)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].GetComponent<Sub_Task>().sub_num == subNum) subtaskindex = i;
            }
        }
        return subtaskindex;
    }
}
