using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance;
    public List<Task> ObjectiveList;
    public GameObject ObjectivePrefab;
    public GameObject ObjectivePrefabAnim;
    public GameObject TaskUI, TaskAnimUI, TaskTitleUI;
    public bool UIShown;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        AssignObejctive("Task Parent2");
        UIShown = true;
    }

    public void AssignTask(string tempText)
    {
        AssignObejctive(tempText);
    }

    public void AssignObejctive(string ObjectiveText)
    {
        Task newObjective = Instantiate(ObjectivePrefab, TaskUI.transform).GetComponent<Task>();
        newObjective.GetComponent<TMP_Text>().text = ObjectiveText;
        ObjectiveList.Add(newObjective);
        //StartCoroutine(ObjectiveAnimation(newObjective, ObjectiveText));
        ChangeTextColor(newObjective.GetComponent<TMP_Text>());
    }

    void ChangeTextColor(TMP_Text temp)
    {
        temp.DOColor(Color.yellow, 0.5f).OnComplete(() => temp.DOColor(Color.white, 0.5f));
    }

    IEnumerator ObjectiveAnimation(Task tempObjective, string ObjectiveText)
    {
        tempObjective.gameObject.SetActive(false);
        Task newObjective = Instantiate(ObjectivePrefabAnim, TaskAnimUI.transform).GetComponent<Task>();
        newObjective.GetComponent<TMP_Text>().text = ObjectiveText;
        Debug.Log(newObjective.GetComponent<TMP_Text>().text);
        //newObjective.GetComponent<TMP_Text>().color = new Color(1, 1, 1, 0);
        yield return null;
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(newObjective.GetComponent<TMP_Text>().DOColor(new Color(1, 1, 1, 1), 1)).PrependInterval(0.1f).Append(newObjective.transform.DOScale(1f, 0.3f))
          .Append(newObjective.GetComponent<RectTransform>().DOAnchorPos(new Vector2(811, (-TaskUI.transform.childCount + 1)*50), 1)).OnComplete(()
          => DestoryAndEnable(newObjective.gameObject, tempObjective.gameObject));
    }

    public void DestoryAndEnable(GameObject destroyedObject, GameObject enabledObject)
    {
        Destroy(destroyedObject);
        enabledObject.SetActive(true);
    }


    public void AssignObejctive(string ObjectiveText, Task MainTask)
    {
        Task newObjective = Instantiate(ObjectivePrefab, TaskUI.transform).GetComponent<Task>();
        newObjective.GetComponent<TMP_Text>().text = ObjectiveText;
        newObjective.GetComponent<TMP_Text>().fontSize = 30;
        for (int i = 0; i < TaskUI.transform.childCount; i++)
        {
            if (TaskUI.transform.GetChild(i).GetComponent<Task>() == MainTask)
            {
                MainTask.BabyTask++;
                newObjective.transform.SetSiblingIndex(i+ MainTask.BabyTask);
                break;
            }
        }
        ObjectiveList.Add(newObjective);
        ChangeTextColor(newObjective.GetComponent<TMP_Text>());
        //StartCoroutine(ObjectiveAnimation(newObjective, ObjectiveText));
    }

    /*public void AssignObejctive(Objective objective)
    {
        //Objective newObjective = Instantiate(ObjectivePrefab, UIManager.instance.objectiveUI.transform).GetComponent<Objective>();
        //Objective newObjective = Instantiate(objective.gameObject, UIManager.instance.objectiveUI.transform).GetComponent<Objective>();
        GameObject newObjective = Instantiate(objective.gameObject, UIManager.instance.objectiveUI.transform);//.GetComponent<Objective>();
        ObjectiveList.Add(newObjective);
        StartCoroutine(newObjective.GetComponent<Objective>().OnAssigned());
    }*/

    //public void AssignObejctive(GameObject objective)
    //{

    //    Task newObjective = Instantiate(objective, TaskUI.transform).GetComponent<Task>();
    //    newObjective.prefabRef = objective;
    //    ObjectiveList.Add(newObjective);
    //    StartCoroutine(ObjectiveAnimation(newObjective, newObjective.GetComponent<TMP_Text>().text));
    //    StartCoroutine(newObjective.OnAssigned());
    //}

    /*public void AddComplexObjective(string MainObjective, string[] SubObjective)
    {
        Objective tempObejctive = Instantiate(ObjectivePrefab, UIManager.instance.objectiveUI.transform).GetComponent<Objective>();
        tempObejctive.GetComponent<TMP_Text>().text = MainObjective;
        for (int i = 0; i < ObjectiveList.Count; i++)
        {
            if (ObjectiveList[i] == null)
            {
                ObjectiveList[i] = tempObejctive;
                break;
            }
        }
        for (int i = 0; i < SubObjective.Length; i++)
        {
            //tempObejctive.subObjectives.Add(AddSubObejctive(SubObjective[i], tempObejctive));
        }

    }*/

    /*public Objective AddSubObejctive(string ObjectiveText, Objective MainObjective)
    {
        Objective tempObejctive = Instantiate(ObjectivePrefab, UIManager.instance.objectiveUI.transform).GetComponent<Objective>();
        tempObejctive.GetComponent<TMP_Text>().fontSize -= 4;
        tempObejctive.GetComponent<TMP_Text>().text = "-" + ObjectiveText;
        //tempObejctive.mainObjective = MainObjective;
        for (int i = 0; i < ObjectiveList.Count; i++)
        {
            if (ObjectiveList[i] == null)
            {
                ObjectiveList[i] = tempObejctive;
                break;
            }
        }
        return tempObejctive;
    }*/

    public void CompleteObjetive(string ObjetiveName)
    {
        foreach (Task objective in ObjectiveList)
        {
            if (ObjetiveName == objective.gameObject.name || ObjetiveName == objective.gameObject.GetComponent<TMP_Text>().text)
            {
                /*if (objective.subObjectives.Count <= 0)
                {
                }*/
                objective.Finish();
                break;
            }
        }
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    AssignObejctive("Task Child");
        //}

        if (Input.GetKeyDown(KeyCode.T))
        {
            UIShown = !UIShown;
            TaskUI.SetActive(UIShown);
            TaskTitleUI.SetActive(UIShown);
        }
    }

}
