using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TaskListTrack))]
[CanEditMultipleObjects]
public class TaskAssignEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TaskListTrack tlt = (TaskListTrack)target;

        if (GUILayout.Button("Generate Task List"))
        {
            tlt.RefreshList();
        }
        base.OnInspectorGUI();

        for(int i = 0; i < tlt.task_track.Count; i ++)
        {
            GUILayout.Box(i + ". " + tlt.NametoNum[i].ToString() + ": " + tlt.task_track[i].ToString());
        }
        
    }


}
