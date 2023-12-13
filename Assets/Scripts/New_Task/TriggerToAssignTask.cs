using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToAssignTask : MonoBehaviour
{
    [SerializeField] private InitializeTask InitialT;
    [SerializeField] private TaskManager_Test_Yunfei TM;
    private TaskData TD;

    private void Start()
    {
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
            
            TM.TaskDone(1);
            Destroy(other.gameObject);

            StartCoroutine(addsub());
        }
    }

    IEnumerator addsub()
    {
        yield return new WaitForSeconds(0.1f);
        InitialT.settext();
        TM.SetSubTask();
    }
}
