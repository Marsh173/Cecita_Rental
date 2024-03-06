using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class NoSightAllowed : MonoBehaviour
{
    //eye blinking
    public static NoSightAllowed instance;
    [SerializeField] private Button eye_UI;
    [SerializeField] private Image eyeBlinkImage;
    public Sprite Eye_Open, Eye_Close;
    public GameObject SoundCollectingMessage, itemAdded, FInstruction, wallHitUI;
    public Animator anim;

    //timer
    [SerializeField] private float openedTimes, countDownTime = 5f;
    public TMP_Text FInstructionText;
    bool TimerActive;

    //warning bar
    public float EyeBarDecreaseSpeed, RechargeSpeed;
    public static float CurrentEyeBarAmount = 100f;
    public GameObject EyeBarUI, AlertBarUI;
    public Slider slider;
    public Image RedAura, EyeBarImage, AlertBarimage;
    private WaitForSeconds rechargeTick = new WaitForSeconds(0.001f);
    private Coroutine rechargeC;
    private void Start()
    {
        Respawn.restarted = false;
        Respawn.dead = false;
        instance = this;

        eye_UI = GetComponent<Button>();
        eye_UI.GetComponent<Image>().sprite = Eye_Close;
        eyeBlinkImage.color = AdjustColor.eyeBgColor;
        FInstructionText = FInstruction.GetComponent<TMP_Text>();
        EyeBarImage = EyeBarUI.GetComponent<Image>();
        slider = slider.GetComponent<Slider>();
        AlertBarimage = AlertBarUI.GetComponent<Image>();

        CurrentEyeBarAmount = 100f;
        EyeBarImage.fillAmount = slider.value = 1f;
        AlertBarimage.fillAmount = 0f;
        openedTimes = 0;
        wallHitUI.SetActive(false);
        anim.SetBool("isBegun", true);
    }
    //reset everything when enabled (entered a safe room)
    private void OnEnable()
    {
        //initialize values
        if(eyeBlinkImage.color.a == 0)
        {
            eyeBlinkImage.color = new Color(0.1f, 0.1f, 0.1f, 1f);
        }
        else eyeBlinkImage.color = AdjustColor.eyeBgColor;
        Respawn.restarted = false;
        Respawn.dead = false;
        instance = this;
        RedAura.color = new Color(RedAura.color.r, RedAura.color.g, RedAura.color.b, 0);
        countDownTime = 5f;
        CurrentEyeBarAmount = 100f;
        openedTimes = 0;
        EyeBarImage.fillAmount = slider.value = 1f;
        AlertBarimage.fillAmount = 0f;

        //close eyes when walk out
        TimerActive = false;
        CloseEyeAnimation();
        eye_UI.image.sprite = Eye_Close;
        FInstructionText.text = "to open your eyes";
        anim.SetBool("isBegun", true);
        wallHitUI.SetActive(true);
    }

    private void Update()
    {
        //Debug.Log(CurrentEyeBarAmount);

        if (Input.GetKeyDown(KeyCode.F))
        {
            EyeAnimation();
            //Debug.Log("animation played");
            //eye_UI.onClick.Invoke();
            FInstructionText.text = FInstructionText.text == "to open your eyes" ? "to close your eyes" : "to open your eyes";
            
            //eye close recharge bar
            if (FInstructionText.text == "to open your eyes")
            {
                //prevent several rechage running at the same time
                if (rechargeC != null)
                {
                    StopCoroutine(rechargeC);
                }


                rechargeC = StartCoroutine(RechargeBar());
                RedAura.color = new Color(RedAura.color.r, RedAura.color.g, RedAura.color.b, 0);
            }
            //stop recharge when eyes are opend
            else if (rechargeC != null)
            {
                StopCoroutine(rechargeC);
            }
        }

        if (FInstructionText.text == "to close your eyes")
        {
            //decrease when eye open
            if (TimerActive && CurrentEyeBarAmount != 0)
            {
                CurrentEyeBarAmount -= 100 / countDownTime * Time.deltaTime;
                //Debug.Log("openedTimes "+openedTimes);
                //Debug.Log("slider value " + slider.value);
            }

            if (CurrentEyeBarAmount <= 0 && FInstructionText.text != "to open your eyes")
            {
                //Debug.Log(CurrentEyeBarAmount);
                //Respawn.dead = true;
                FInstructionText.text = "to open your eyes";
                CloseEyeAnimation();

                if (rechargeC != null)
                {
                    StopCoroutine(rechargeC);
                }

                rechargeC = StartCoroutine(RechargeBar());
            }
        }

        //Timer decrease
        if (TimerActive)
        {
            if (TriggerEye.enteredCantOpen)
            {
                countDownTime = 2f;
            }
            else
            {
                countDownTime = 5f;
            }

            if (CurrentEyeBarAmount <= 25f)
            {
                RedAura.color = new Color(RedAura.color.r, RedAura.color.g, RedAura.color.b, (2.5f - CurrentEyeBarAmount / 10f) / 2f );
            }
        }

        EyeBarImage.fillAmount = slider.value = CurrentEyeBarAmount / 100f;
        AlertBarimage.fillAmount = openedTimes * 0.1f;

        if (AdjustColor.settingsOn)
        {
            eyeBlinkImage.color = AdjustColor.eyeBgColor;
        }
    }

    //Animation
    public void EyeAnimation()
    {
        //close eye animation
        if (eye_UI.image.sprite == Eye_Open)
        {
            CloseEyeAnimation();
        }

        //open eye animation
        else
        {
            if (100 - openedTimes * 10 > 10f)
            {
                openedTimes++;
            }
            eye_UI.image.sprite = Eye_Open;
            anim.SetBool("isBegun", false);
            anim.SetBool("isOpen", true);
            wallHitUI.SetActive(false);
            TimerActive = true;

        }
    }

    public void CloseEyeAnimation()
    {
        eye_UI.image.sprite = Eye_Close;
        anim.SetBool("isBegun", true);
        anim.SetBool("isOpen", false);
        wallHitUI.SetActive(true);
        TimerActive = false;
        RedAura.color = new Color(RedAura.color.r, RedAura.color.g, RedAura.color.b, 0);
    }

    void FadeToColor(Color color)
    {
        Graphic graphic = GetComponent<Graphic>();
        graphic.CrossFadeColor(color, eye_UI.colors.fadeDuration, true, true);
    }

    private IEnumerator RechargeBar()
    {
        yield return new WaitForSeconds(1.5f);
        //Debug.Log("recharge waited");
        while (CurrentEyeBarAmount < 100 - openedTimes * 10)
        {
            CurrentEyeBarAmount += RechargeSpeed * Time.deltaTime;
            yield return rechargeTick;
        }

        rechargeC = null;
        //Debug.Log("Supposed CurrentEyeBarAmount" + (100 - openedTimes * 10));
        //Debug.Log("recharge finished");
    }
}
