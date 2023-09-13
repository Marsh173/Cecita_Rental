using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEye : MonoBehaviour
{
    public GameObject eye;

    private void Start()
    {
        eye.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        eye.SetActive(true);
    }
}
