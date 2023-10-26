using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class NoSightAllowed : MonoBehaviour
{
    public static NoSightAllowed instance;
    private Button eye_UI;
    public Sprite Eye_Open, Eye_Close;
    public GameObject SoundCollecting, itemAdded, F;
    public KeyCode keyF;
    public Animator anim;

    //timer
    public static float countDown;
    [SerializeField] private float countDownTime = 5f;
    public TMP_Text countdownText;
    bool TimerActive;

    //warning bar
    public float EyeBarDecreaseSpeed, RechargeSpeed;
    private float CurrentEyeBarAmount;
    public GameObject ChargeBarUI;
    public Image RedAura, BarImage;

    private void Start()
    {
        Respawn.restarted = false;
        Respawn.dead = false;
        instance = this;

        TimerActive = true;
        eye_UI = GetComponent<Button>();
        eye_UI.GetComponent<Image>().sprite = Eye_Open;
        countDown = countDownTime = 5f;
        anim.SetBool("isBegun", false);

        CurrentEyeBarAmount = 100f;
        BarImage = ChargeBarUI.GetComponent<Image>();
    }
    //reset everything when enabled (entered a safe room)
    private void OnEnable()
    {
        Respawn.restarted = false;
        Respawn.dead = false;
        instance = this;

        TimerActive = true;
        countdownText.enabled = true;
        countDown = countDownTime = 5f;
        eye_UI.image.sprite = Eye_Open;
        anim.SetBool("isBegun", false);

        CurrentEyeBarAmount = 100f;
        BarImage.fillAmount = 1f;

        RedAura.color = new Color(RedAura.color.r, RedAura.color.g, RedAura.color.b, 0);
    }

    private void Update()
    { 
        //Key binding, activate animation
        if(Input.GetKeyDown(keyF))
        {
            FadeToColor(eye_UI.colors.pressedColor);
            eye_UI.onClick.Invoke();
            F.GetComponent<TMP_Text>().text = F.GetComponent<TMP_Text>().text == "Press F to open  your eyes" ? "Press F to close your eyes" : "Press F to open  your eyes";

            //eye open
            if (F.GetComponent<TMP_Text>().text == "Press F to close your eyes")
            {
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

            if(countDown <= 3f)
            {
                RedAura.color = new Color(RedAura.color.r, RedAura.color.g, RedAura.color.b, (3f - countDown) / 3f );
            }

            //death when timer's out
            if (countDown <= 0)
            {
                Respawn.dead = true;
                countdownText.text = "";
            }
        }
        else
        {
            countDown = countDownTime;

            //StartCoroutine(RechargeBar());
            if (CurrentEyeBarAmount < 100)
            {
                CurrentEyeBarAmount = Mathf.SmoothDamp(BarImage.fillAmount * 100f, 100f, ref RechargeSpeed, 500 * Time.deltaTime);
                BarImage.fillAmount = CurrentEyeBarAmount / 100f;
            }
        }

        if (F.GetComponent<TMP_Text>().text == "Press F to close your eyes")
        {
            CurrentEyeBarAmount = Mathf.SmoothDamp(BarImage.fillAmount * 100, 0, ref EyeBarDecreaseSpeed, 500 * Time.deltaTime);
            Debug.Log(CurrentEyeBarAmount);
            BarImage.fillAmount = CurrentEyeBarAmount / 100f;
            
            if (CurrentEyeBarAmount < 0)
            {
                Respawn.dead = true;
            }
        }
        /*else
        {
            RedAura.color = new Color(RedAura.color.r, RedAura.color.g, RedAura.color.b, 0);
        }*/

    }

    //Animation
    public void ChangeSprite()
    {
        //
        if (eye_UI.image.sprite == Eye_Open)
        {
            RedAura.color = new Color(RedAura.color.r, RedAura.color.g, RedAura.color.b, 0);
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

    private IEnumerator RechargeBar()
    {
        yield return new WaitForSeconds(2);

    }
}
