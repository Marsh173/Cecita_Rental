using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSave : MonoBehaviour
{
    [SerializeField] string save_name = " ";
    public void save_task_system()
    {
        ES3.Save(save_name, gameObject);
    }

    public void load_task_system()
    {
        if (ES3.KeyExists(save_name))
        {
            ES3.Load(save_name);
        }
        
    }
}
