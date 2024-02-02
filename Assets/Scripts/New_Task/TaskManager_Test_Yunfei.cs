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


    // ----------- assign main task 
    public void AddTask(int tasknum, string taskname)
    {
        var t = Instantiate(TaskPrefab, TaskOrigin.transform);

        Tasks.Add(t);

        t.GetComponent<TaskData>().taskName = taskname;
        t.GetComponent<TaskData>().task_num = tasknum;


        t.transform.SetParent(TaskOrigin.transform);
        RearrangeTasks();
    }

    public void AddTask_Attach(int attach_num,int tasknum, string taskname, int s)
    {
        var t = Instantiate(TaskPrefab, TaskOrigin.transform);

        Tasks.Insert(GetTaskIndex(attach_num)+1, t);


        t.GetComponent<TaskData>().taskName = taskname;
        t.GetComponent<TaskData>().task_num = tasknum;
        t.GetComponent<TaskData>().sub = s;

        t.transform.SetParent(TaskOrigin.transform);

        //get attached task


        RearrangeTasks();
    }


    public IEnumerator TaskFinish(int num)
    {
        GameObject t = Tasks[GetTaskIndex(num)];
        t.GetComponent<Animator>().SetBool("Done", true);
        yield return new WaitForSeconds(1.5f);
        Tasks.RemoveAt(GetTaskIndex(num));
        Destroy(t);
        RearrangeTasks();

    }

    public void RearrangeTasks()
    {
        for (int i = 0; i < Tasks.Count; i++)
        {
            var t = Tasks[i];
            var y_shift =  i * y_height_task;
           
            t.GetComponent<RectTransform>().anchoredPosition = new Vector3(50, 60 + y_shift , 0);
        }
    }

    public int GetTaskIndex(int num)
    {
        int index = 0;

        for (int i = 0; i < Tasks.Count; i++)
        {
            if (Tasks[i].GetComponent<TaskData>().task_num == num)index = i;
        }

        return index;
    }
}
