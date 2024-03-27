using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class notificationMark : MonoBehaviour
{
    [SerializeField] GameObject nm;
    public bool isNew = false;

    private void Update()
    {
        nm.SetActive(false);
    }
    public void clicked()
    {
        isNew = false;
    }
}
