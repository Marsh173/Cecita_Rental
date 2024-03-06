/*using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TaskAssignTrigger))]
[CanEditMultipleObjects]
public class TaskAssignEditor : Editor
{
    //SerializedProperty TaskAssignTrigger;
    string Trigger_Type = "none selected";
    string Task_Type = "none selected";
    int trigger = 0;
    int task = 0;

    string maintask = "";
    string subtask = "";
    string attach_to_main_task = "";
    string subsubtask = "";
    public override void OnInspectorGUI()
    {

        GUI.contentColor = Color.white;

        TaskAssignTrigger taskassigntrigger = (TaskAssignTrigger)target;
        switch (trigger)
        { 
            case 1:
                taskassigntrigger.assign = TaskAssignTrigger.AssignTask_By.trigger;
                break;

            case 2:
                taskassigntrigger.assign = TaskAssignTrigger.AssignTask_By.dialogue;
                break;

            case 3:
                taskassigntrigger.assign = TaskAssignTrigger.AssignTask_By.button;
                break;

            case 4:
                taskassigntrigge
                    r.assign = TaskAssignTrigger.AssignTask_By.add_when_awake;
                break;

            default:
                taskassigntrigger.assign = TaskAssignTrigger.AssignTask_By.none;
                break;

        }
        if(taskassigntrigger.assign == TaskAssignTrigger.AssignTask_By.none)
        {
            GUILayout.BeginHorizontal();
            if (trigger != 1)
            {
                if (GUILayout.Button("Trigger"))
                {
                    Trigger_Type = "Trigger";
                    trigger = 1;
                }

            }
            else GUILayout.Box("Trigger");

            if (trigger != 2)
            {
                if (GUILayout.Button("Dialogue"))
                {
                    Trigger_Type = "Dialogue";
                    trigger = 2;
                }
            }
            else GUILayout.Box("Dialogue");

            if (trigger != 3)
            {
                if (GUILayout.Button("Button"))
                {
                    Trigger_Type = "Button";
                    trigger = 3;
                }

            }
            else GUILayout.Box("Button");

            if (trigger != 4)
            {

                if (GUILayout.Button("Awake"))
                {
                    Trigger_Type = "Awake";
                    trigger = 4;
                }
            }
            else GUILayout.Box("Awake");

            GUILayout.EndHorizontal();
        }

        

        GUILayout.BeginHorizontal();

        if (task != 1)
        {
            if (GUILayout.Button("Add Maintask"))
            {
                Task_Type = "Add Maintask";
                task = 1;
                
            }

        }
        else GUILayout.Box("Add Maintask");

        if (task != 2)
        {
            if (GUILayout.Button("Finish Maintask"))
            {
                Task_Type = "Finish Maintask";
                task = 2;
            }
        }
        else GUILayout.Box("Finish Maintask");


        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (task != 3)
        {
            if (GUILayout.Button("Add Subtask"))
            {
                Task_Type = "Add Subtask";
                task = 3;
            }
        }
        else GUILayout.Box("Add Subtask");

        if (task != 4)
        {
            if (GUILayout.Button("Finish Subtask"))
            {
                Task_Type = "Finish Subtask";
                task = 4;
            }
        }
        else GUILayout.Box("Add Subtask");
        
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (task != 5)
        {
            if (GUILayout.Button("Add Subsubtask"))
            {
                Task_Type = "Add Subsubtask";
                task = 5;
            }
        }
        else GUILayout.Box("Finish Subsubtask");

        if (task != 6)
        {
            if (GUILayout.Button("Finish Subsubtask"))
            {
                Task_Type = "Finish Subsubtask";
                task = 6;
            }
        }
        else GUILayout.Box("Finish Subsubtask");


        GUILayout.EndHorizontal();

        if(task == 1 || task == 2)
        {
            GUILayout.Label("Task : ");
            maintask = GUILayout.TextArea(maintask, 200);
        }

        if (task == 3 || task == 4)
        {
            GUILayout.Label("Sub Task : ");
            subtask = GUILayout.TextArea(subtask, 200);
            GUILayout.Label("Attach to MaintaskNum : ");
            attach_to_main_task = GUILayout.TextArea(attach_to_main_task, 200);
        }

            switch (task)
        {
            case 1:
                taskassigntrigger.type = TaskAssignTrigger.Task_Type.Add_Maintask;
                break;

            case 2:
                taskassigntrigger.type = TaskAssignTrigger.Task_Type.Finish_Maintask;
                break;
            case 3:
                taskassigntrigger.type = TaskAssignTrigger.Task_Type.Add_subtask;
                break;
            case 4:
                taskassigntrigger.type = TaskAssignTrigger.Task_Type.Finish_subtask;
                break;
            case 5:
                taskassigntrigger.type = TaskAssignTrigger.Task_Type.Add_subsubtask;
                break;
            case 6:
                taskassigntrigger.type = TaskAssignTrigger.Task_Type.Finish_subsubtask;
                break;
            default:
                taskassigntrigger.type = TaskAssignTrigger.Task_Type.none;
                break;

        }
            base.OnInspectorGUI();
    }
}
*/