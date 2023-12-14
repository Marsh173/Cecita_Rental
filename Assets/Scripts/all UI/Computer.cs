using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;        
        }
        else
        {
            Cursor.visible = false;
        }
    }

    public void CurosrUnvisible()
    {
        Cursor.visible = false;
    }

}
