using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Sub_Task : MonoBehaviour
{
    public TextMeshProUGUI sub_task_text;
    public string sub_taskName;
    public bool isCompleted;
    public int sub_num;

    private void Update()
    {
        if(sub_task_text!= null)sub_task_text.text = sub_taskName;

        if (isCompleted)
        {
            FinishSubTask();
        }
    }

    public void FinishSubTask()
    {
        Debug.Log("Finish Sub Task" + sub_taskName);
        Destroy(gameObject);
    }
}
