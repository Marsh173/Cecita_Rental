using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerToAssignTask : MonoBehaviour
{
/*    [SerializeField] private InitializeTask InitialT;
    [SerializeField] private TaskManager_Test_Yunfei TM;
    private TaskData TD;
    public TMP_Text monologue;
    public bool checkedEmail;
    private void Start()
    {
        checkedEmail = false;
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

        else if(other.CompareTag("Player") && !checkedEmail)
        {
            monologue.text = "I should check my email first.";
        }
    }
    
    IEnumerator addsub()
    {
        yield return new WaitForSeconds(0.1f);
        InitialT.settext();
        TM.SetSubTask();
    }

    public void CheckEmail()
    {
        checkedEmail = true;
    }

    public void TempSubtaskDone()
    {
        monologue.text = "Maybe I should just wait for the package theif when the time comes...";
    }

    public void TempTalktoFrontDeskDone()
    {
        CutSceneScript.talkedtoNPC = true;
    }*/

    
}
