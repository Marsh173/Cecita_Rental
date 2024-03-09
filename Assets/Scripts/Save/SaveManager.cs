using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    TaskSave[] t;
    // Start is called before the first frame update
    void Start()
    {
        t = FindObjectsOfType(typeof(TaskSave)) as TaskSave[];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            saveAll();
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            loadAll();
        }
    }
    // Update is called once per frame
    public void saveAll()
    {
        for (int i = 0; i < t.Length; i++)
        {
            t[i].save_task_system();
        }
    }

    public void loadAll()
    {
        for (int i = 0; i < t.Length; i++)
        {
            t[i].load_task_system();
        }
    }
}
