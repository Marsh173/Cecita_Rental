using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEye : MonoBehaviour
{
    public GameObject eye;

    private void Start()
    {
        eye.active = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        eye.active = true;
    }
}
