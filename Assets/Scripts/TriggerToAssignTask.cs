using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToAssignTask : MonoBehaviour
{
    [SerializeField] private InitializeTask InitialT;
    [SerializeField] private TaskManager_Test_Yunfei TM;

    private void Start()
    {
        InitialT.settext();
        TM.AddTask();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<InitializeTask>() != null)
        {
            InitialT = other.GetComponent<InitializeTask>();
        }

        if (other.CompareTag("TriggerForPackage") && CompareTag("Player"))
        {
            InitialT.settext();
            TM.AddTask();
            TM.TaskDone(0);
            Destroy(other.gameObject);
        }
    }
}
