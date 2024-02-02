using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTaskDone : MonoBehaviour
{
    public TaskListTrack tlt;
    public List<int> conditions = new List<int>();

    public UnityEvent WhenTaskDone;


    public bool CheckConditions()
    {
        bool b = true;
        for (int i = 0; i < conditions.Count; i ++)
        {
            if (b) b = tlt.task_track[i];
            else break;
        }
        return b;
    }

    private void Update()
    {
        if (CheckConditions()) TaskAreDone();
        Debug.Log(CheckConditions());
    }
    public void TaskAreDone()
    {
        WhenTaskDone.Invoke();                                                
    }



}
