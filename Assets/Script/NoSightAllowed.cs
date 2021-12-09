using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NoSightAllowed : MonoBehaviour
{
    private Button eye_UI;
    public Sprite Eye_Open;
    public Sprite Eye_Close;

    public KeyCode keyF;

    public Animator anim;

    float currentTime;
    public Text countdownText;
    bool TimerActive;

    
    private void Start()
    {
        eye_UI = GetComponent<Button>();
        eye_UI.GetComponent<Image>().sprite = Eye_Open;
        currentTime = 5f;
    }

    public void ChangeSprite()
    {
        if(eye_UI.image.sprite == Eye_Open)
        {
            eye_UI.image.sprite = Eye_Close; //close
            anim.SetBool("isBegun", true);
            anim.SetBool("isOpen", false);
            TimerActive = false;
            countdownText.enabled = false;
            
        }
        else
        {
            eye_UI.image.sprite = Eye_Open; //open
            anim.SetBool("isOpen", true);
            TimerActive = true;
            countdownText.enabled = true;
        }
    }

    private void Update()
    { 
        if(Input.GetKeyDown(keyF))
        {
            FadeToColor(eye_UI.colors.pressedColor);
            eye_UI.onClick.Invoke();
        }
        else  if(Input.GetKeyUp(keyF))
        {
            FadeToColor(eye_UI.colors.normalColor);
        }

        if(TimerActive)
        {
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = currentTime.ToString("0");

            if (currentTime <= 1)
            {
                currentTime = 5;
                SceneManager.LoadScene("Death");
            }
        }
        else
        {
            currentTime = 5f; 
        }
    }

    void FadeToColor(Color color)
    {
        Graphic graphic = GetComponent<Graphic>();
        graphic.CrossFadeColor(color, eye_UI.colors.fadeDuration, true,true);
    }

}
