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
    public GameObject subsubprefab;
    public float y_height;

    public List<GameObject> subsubtask = new List<GameObject>();

    private void Update()
    {
        if(sub_task_text!= null)sub_task_text.text = sub_taskName;

        if (isCompleted)
        {
            FinishSubTask();
        }
    }

    public void addSubSub(string subsubtask_name, int subsub_num)
    {
        var s = Instantiate(subsubprefab, transform);
        s.transform.SetParent(transform);
        subsubtask.Add(s);
        s.GetComponent<sub_sub_task>().sub_sub_name = subsubtask_name;
        var sublistHeight = subsubtask.Count * y_height;
        s.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -sublistHeight, 0);
        s.GetComponent<sub_sub_task>().sub_sub_num = subsub_num;
    }

    public void FinishSubTask()
    {
        Destroy(gameObject);
    }
}
