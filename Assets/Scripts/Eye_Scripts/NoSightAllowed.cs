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
    public GameObject SoundCollecting, itemAdded, FInstruction;
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
    private WaitForSeconds rechargeTick = new WaitForSeconds(0.001f);
    private Coroutine rechargeC;
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

    private void FixedUpdate()
    { 
        if(Input.GetKeyDown(KeyCode.F))
        {
            ChangeSprite();
            //eye_UI.onClick.Invoke();
            FInstruction.GetComponent<TMP_Text>().text = FInstruction.GetComponent<TMP_Text>().text == "Press F to open your eyes" ? "Press F to close your eyes" : "Press F to open your eyes";

            //eye close recharge bar
            if (FInstruction.GetComponent<TMP_Text>().text == "Press F to open your eyes")
            {
                if(rechargeC != null)
                {
                    StopCoroutine(rechargeC);
                }

                rechargeC = StartCoroutine(RechargeBar());
                RedAura.color = new Color(RedAura.color.r, RedAura.color.g, RedAura.color.b, 0);
            }
            else
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

            countDown -= 1 * Time.deltaTime;
            //countdownText.text = countDown.ToString("0");

            if(countDown <= 3f)
            {
                RedAura.color = new Color(RedAura.color.r, RedAura.color.g, RedAura.color.b, (3f - countDown) / 3f );
            }

            //death when timer's out
            /*if (countDown <= 0)
            {
                Respawn.dead = true;
                countdownText.text = "";
            }*/
        }
        else
        {
            countDown = countDownTime;
        }

        //recharge bar
        if (FInstruction.GetComponent<TMP_Text>().text == "Press F to close your eyes")
        {
            //decrease when eye open
            if(TimerActive && CurrentEyeBarAmount != 0)
            {
                CurrentEyeBarAmount -= 100/countDownTime * Time.deltaTime;
                Debug.Log("speed" + 100 / countDownTime * Time.deltaTime);

            }
            
            if (CurrentEyeBarAmount < 0)
            {
                Respawn.dead = true;
            }
        }
        BarImage.fillAmount = CurrentEyeBarAmount / 100f;
    }

    //Animation
    public void ChangeSprite()
    {
        //close eye animation
        if (eye_UI.image.sprite == Eye_Open)
        {
            RedAura.color = new Color(RedAura.color.r, RedAura.color.g, RedAura.color.b, 0);
            FInstruction.GetComponent<TMP_Text>().text = "Press F to close your eyes";
            eye_UI.image.sprite = Eye_Close;
            anim.SetBool("isBegun", true);
            anim.SetBool("isOpen", false);
            TimerActive = false;
            countdownText.enabled = false;

        }
        //open eye animation
        else
        {
            eye_UI.image.sprite = Eye_Open;
            FInstruction.GetComponent<TMP_Text>().text = "Press F to open your eyes";
            //anim.SetBool("isBegun", false);
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
        yield return new WaitForSeconds(1.5f);
        Debug.Log("waited");
        while (CurrentEyeBarAmount < 100)
        {
            CurrentEyeBarAmount += RechargeSpeed * Time.deltaTime;
            yield return rechargeTick;
        }

        rechargeC = null;
    }
}
