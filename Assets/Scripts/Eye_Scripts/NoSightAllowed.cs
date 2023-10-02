using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NoSightAllowed : MonoBehaviour
{
    public static NoSightAllowed instance;
    private Button eye_UI;
    public Sprite Eye_Open;
    public Sprite Eye_Close;
    public GameObject SoundCollecting, itemAdded;
    public GameObject F;

    public KeyCode keyF;

    public Animator anim;

    public static float countDown;
    float countDownTime = 5f;
    public Text countdownText;
    bool TimerActive;
    public float EyeChargeBar;
    public GameObject ChargeBarUI;
    public RawImage RedAura;
    
    private void Start()
    {
        instance = this;
        TimerActive = true;
        eye_UI = GetComponent<Button>();
        eye_UI.GetComponent<Image>().sprite = Eye_Open;
        countDown = countDownTime;
        anim.SetBool("isBegun", false);
        EyeChargeBar = 100;
    }

    private void Update()
    { 
        //Key binding, activate animation
        if(Input.GetKeyDown(keyF))
        {
            FadeToColor(eye_UI.colors.pressedColor);
            eye_UI.onClick.Invoke();
            F.GetComponent<Text>().text = F.GetComponent<Text>().text == "Press F to open your eyes" ? "Press F to close your eyes" : "Press F to open your eyes";
            if (F.GetComponent<Text>().text == "Press F to close your eyes")
            {
                EyeChargeBar-= 20f;
                if (EyeChargeBar < 0)
                {
                    SceneManager.LoadScene("Death");
                }
                ChargeBarUI.GetComponent<Image>().fillAmount = EyeChargeBar/100f;
            }
            else
            {
                RedAura.color = new Color(RedAura.color.r, RedAura.color.g, RedAura.color.b, 0);
            }
        }
        else if(Input.GetKeyUp(keyF))
        {
            FadeToColor(eye_UI.colors.normalColor);
        }

        //Timer decrease
        if(TimerActive)
        {
            if (TriggerEye.enteredCantOpen)
            {
                countDownTime = 2f;
            }
            else
            {
                countDownTime = 5f;
            }

            countDown -= 1 * Time.deltaTime;
            countdownText.text = countDown.ToString("0");
            if(countDown <= 2.5f)
            {
                RedAura.color = new Color(RedAura.color.r, RedAura.color.g, RedAura.color.b, (3f - countDown) / 3f );
            }

            //death when timer's out
            if (countDown <= 0)
            {
                countDown = countDownTime;
                SceneManager.LoadScene("Death");
            }
        }
        else
        {
            countDown = countDownTime; 
        }
    }

    //Animation
    public void ChangeSprite()
    {
        //
        if (eye_UI.image.sprite == Eye_Open)
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
            anim.SetBool("isBegun", false);
            anim.SetBool("isOpen", true);
            TimerActive = true;
            countdownText.enabled = true;
            
        }
    }

    void FadeToColor(Color color)
    {
        Graphic graphic = GetComponent<Graphic>();
        graphic.CrossFadeColor(color, eye_UI.colors.fadeDuration, true, true);
    }

}
