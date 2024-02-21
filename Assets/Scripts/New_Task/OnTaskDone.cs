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
            if (b) {

                if (tlt.task_track.ContainsKey(conditions[i]))b = tlt.task_track[conditions[i]]; 

            }
            else break;
        }
        return b;
    }

    private void FixedUpdate()
    {
        if (CheckConditions()) TaskAreDone();
    }
    public void TaskAreDone()
    {
        WhenTaskDone.Invoke();                                                
    }



}
