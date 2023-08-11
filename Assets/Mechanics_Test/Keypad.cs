using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Keypad : MonoBehaviour
{
    [SerializeField] private TMP_Text Ans;
    public string Answer = "1234";
    [SerializeField] private Animator door;
    [SerializeField] private TMP_Text Ans3d;

    public Canvas canvasToDestroy;
    public GameObject keypadObj;
    public LayerMask newLayer;
    

    public void Number(int number)
    {
        if(Ans.text.Length < 4)
        {
            Ans.text += number.ToString();
            Ans3d.text = Ans.text;
        }
        
    }

    public void Execute()
    {
        if(Ans.text == Answer)
        {
            //Open Door
            Ans.text = "PASS";
            Ans.color = Color.green;
            Ans3d.text = "PASS";
            Ans3d.color = Color.green;
            door.SetBool("Open", true);
            keypadObj.layer = newLayer;
            Destroy(canvasToDestroy.gameObject);
        }
        else
        {
            Ans.text = "ERROR";
            Ans.color = Color.red;
            Ans3d.text = "ERROR";
            Ans3d.color = Color.red;
        }
    }

    public void Clear()
    {
        Ans.text = "";
        Ans.color = Color.black;

        Ans3d.text = "";
        Ans3d.color = Color.black;
    }

   
    
}
