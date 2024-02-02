using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskDoneTrigger : MonoBehaviour
{
    public FinishTask_By fin;
    //finishes a task

    public enum FinishTask_By
    {
        trigger,
        button,
        dialogue,
        when_tasks_done,
        none
    }

}
