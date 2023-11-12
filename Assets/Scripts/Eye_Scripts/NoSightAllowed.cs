using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class NoSightAllowed : MonoBehaviour
{
    public static NoSightAllowed instance;
    [SerializeField] private Button eye_UI;
    public Sprite Eye_Open, Eye_Close;
    public GameObject SoundCollectingMessage, itemAdded, FInstruction, wallHitUI;
    public Animator anim;

    //timer
    private float countDown, openedTimes;
    [SerializeField] private float countDownTime = 5f;
    private TMP_Text countdownText, FInstructionText;
    bool TimerActive;

    //warning bar
    public float EyeBarDecreaseSpeed, RechargeSpeed;
    public static float CurrentEyeBarAmount;
    public GameObject ChargeBarUI;
    public Image RedAura, BarImage;
    private WaitForSeconds rechargeTick = new WaitForSeconds(0.001f);
    private Coroutine rechargeC;
    private void Start()
    {
        Respawn.restarted = false;
        Respawn.dead = false;
        instance = this;

        TimerActive = true;
        eye_UI = this.GetComponent<Button>();
        eye_UI.GetComponent<Image>().sprite = Eye_Open;
        FInstructionText = FInstruction.GetComponent<TMP_Text>();
        countDown = countDownTime = 5f;
        anim.SetBool("isBegun", false);

        CurrentEyeBarAmount = 100f;
        BarImage = ChargeBarUI.GetComponent<Image>();
        wallHitUI.SetActive(false);
    }
    //reset everything when enabled (entered a safe room)
    private void OnEnable()
    {
        Respawn.restarted = false;
        Respawn.dead = false;
        instance = this;
        
        RedAura.color = new Color(RedAura.color.r, RedAura.color.g, RedAura.color.b, 0);
        TimerActive = true;
        //countdownText.enabled = true;
        countDown = countDownTime = 5f;
        eye_UI.image.sprite = Eye_Open;
        FInstructionText.text = "Press F to close your eyes";
        anim.SetBool("isBegun", false);

        CurrentEyeBarAmount = 100f;
        BarImage.fillAmount = 1f;
        wallHitUI.SetActive(false);
        openedTimes = 0;
    }

    private void Update()
    { 
        if(Input.GetKeyDown(KeyCode.F))
        {
            EyeAnimation();
            Debug.Log("animation played");
            //eye_UI.onClick.Invoke();
            FInstructionText.text = FInstructionText.text == "Press F to open your eyes" ? "Press F to close your eyes" : "Press F to open your eyes";

            //eye close recharge bar
            if (FInstructionText.text == "Press F to open your eyes")
            {
                if(rechargeC != null)
                {
                    StopCoroutine(rechargeC);
                }

                rechargeC = StartCoroutine(RechargeBar());
                RedAura.color = new Color(RedAura.color.r, RedAura.color.g, RedAura.color.b, 0);
            }
            else if (rechargeC != null)
            {
                StopCoroutine(rechargeC);
            }
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

            //countDown -= 1 * Time.deltaTime;
            //countdownText.text = countDown.ToString("0");

            if (CurrentEyeBarAmount <= 25f)
            {
                RedAura.color = new Color(RedAura.color.r, RedAura.color.g, RedAura.color.b, (2.5f - CurrentEyeBarAmount / 10f) / 2f );
            }

            /*//death when timer's out
            if (countDown <= 0)
            {
                countdownText.text = "";
            }*/
        }
        /*else
        {
            countDown = countDownTime;
        }*/

        //recharge bar
        if (FInstructionText.text == "Press F to close your eyes")
        {
            //decrease when eye open
            if(TimerActive && CurrentEyeBarAmount != 0)
            {
                CurrentEyeBarAmount -= 100/(countDownTime - openedTimes / 2) * Time.deltaTime;
                Debug.Log("speed" + 100/(countDownTime - openedTimes / 2) * Time.deltaTime);
                Debug.Log(openedTimes);

            }

            if (CurrentEyeBarAmount <= 0)
            {
                //Debug.Log(Respawn.dead);
                Respawn.dead = true;
            }
        }
        BarImage.fillAmount = CurrentEyeBarAmount / 100f;
        //Debug.Log(countDown);
    }

    //Animation
    public void EyeAnimation()
    {
        //close eye animation
        if (eye_UI.image.sprite == Eye_Open)
        {
            eye_UI.image.sprite = Eye_Close;
            anim.SetBool("isBegun", true);
            anim.SetBool("isOpen", false);
            wallHitUI.SetActive(true);
            TimerActive = false;
            RedAura.color = new Color(RedAura.color.r, RedAura.color.g, RedAura.color.b, 0);
            //FInstructionText.text = "Press F to close your eyes";
            //countdownText.enabled = false;
        }
        //open eye animation
        else
        {
            openedTimes++;
            eye_UI.image.sprite = Eye_Open;
            anim.SetBool("isBegun", false);
            anim.SetBool("isOpen", true);
            wallHitUI.SetActive(false);
            TimerActive = true;
            //FInstructionText.text = "Press F to open  your eyes";
            //countdownText.enabled = true;

        }
    }

    void FadeToColor(Color color)
    {
        Graphic graphic = GetComponent<Graphic>();
        graphic.CrossFadeColor(color, eye_UI.colors.fadeDuration, true, true);
    }

    private IEnumerator RechargeBar()
    {
        yield return new WaitForSeconds(1.5f);
        Debug.Log("recharge waited");
        while (CurrentEyeBarAmount < 100)
        {
            CurrentEyeBarAmount += RechargeSpeed * Time.deltaTime;
            yield return rechargeTick;
        }

        rechargeC = null;

        Debug.Log("recharge finished");
    }
}
