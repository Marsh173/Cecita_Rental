using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToAssignTask : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TaskManager.instance.CompleteObjetive("Pick up package from the front door");
            TaskManager.instance.AssignObejctive("Package Lost");
            TaskManager.instance.AssignObejctive("Find the missing package", TaskManager.instance.ObjectiveList[1]);
            Destroy(this.gameObject);
        }
    }
}
