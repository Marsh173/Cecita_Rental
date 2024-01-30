using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TaskNameData", order = 1)]
public class TaskNameData : ScriptableObject
{
    public int MainTask_Num = 0;
    public string Task_Description = "";
    public string[] Sub_Task_Description = {"","" };
}
