using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class TaskListTrack : MonoBehaviour
{
    [SerializeField] public List<string> tasks = new List<string>();
    public Dictionary<int, bool> task_track = new Dictionary<int, bool>();
    public Dictionary<int, string> NametoNum = new Dictionary<int, string>();

    public void RefreshList()
    {
        tasks.Clear();
        task_track.Clear();
        NametoNum.Clear();
        foreach (Transform child in transform)
        {
            TaskAssignTrigger tat = child.gameObject.GetComponent<TaskAssignTrigger>();
            if (tat != null) tasks.Add(tat.task_name);
            if (child.childCount != 0)
            {
                foreach (Transform child_ in child)
                {
                    tasks.Add(child_.GetComponent<TaskAssignTrigger>().task_name);
                    if (child_.childCount != 0)
                    {
                        foreach (Transform child_0 in child_)
                        {
                            tasks.Add(child_0.GetComponent<TaskAssignTrigger>().task_name);
                        }
                    }
                }
            }
        }
        
        //generate both dictionaries

        for (int i = 0; i < tasks.Count; i ++)
        {
            NametoNum.Add(i,tasks[i]);
            task_track.Add(i, false);
        }
        

    }
}
