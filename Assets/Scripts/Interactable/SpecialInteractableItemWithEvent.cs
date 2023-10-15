using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SpecialInteractableItemWithEvent : InteractableItemWithEvent
{
    Coroutine coroutineRef;
    private bool hasEventPostInteractionTriggered = false;
    public UnityEvent EventPostInteraction;        //Disable player movement input here && Zoom into the interacted object here
    public UnityEvent EventOnPressEscape;          //Re-enables player movement input here && Zoom out from selected object

    //Input method booleans
    [Header ("Input Type")]
    public bool Input_LMBClick = false;
    public bool Input_LMBSlideLeft = false;
    public bool Input_LMBSlideRight = false;
    public bool Input_LMBSlideUp = false;
    public bool Input_LMBSlideDown = false;

    public bool Input_ScrollWheelRollForward = false;
    public bool Input_ScrollWheelRollBackward = false;
    public bool Input_LMBAndRMBClick = false;
    public bool Input_LMBAndRMBHold = false;
    public bool Input_MouseShake_LeftRight = false;
    public bool Input_MouseShake_UpDown = false;

    [Header ("Input Modifiers")]
    public bool rage_MouseShake = false;             //Controls whether player needs to shake the mouse <X> number of times within a short time frame
    public int MouseShake_LeftRight_Threshold = 20;
    public int MouseShake_UpDown_Threshold = 100;

    //Required button hold length for hold inputs
    [SerializeField] float buttonHoldTime;

    //variables
    private float doubleClickTime = 0.2f, lastClickTime;
    private bool mouseShakeWaitForRight = false;
    private bool mouseShakeWaitForLeft = false;
    private int shakeCount = 0;

    //Events Corresponding to input method
    public UnityEvent EventOnSpecialInteraction_LMBClick;
    public UnityEvent EventOnSpecialInteraction_LMBDoubleClick;
    public UnityEvent EventOnSpecialInteraction_LMBSlideLeft;
    public UnityEvent EventOnSpecialInteraction_LMBSlideRight;
    public UnityEvent EventOnSpecialInteraction_LMBSlideUp;
    public UnityEvent EventOnSpecialInteraction_LMBSlideDown;
    public UnityEvent EventOnSpecialInteraction_ScrollWheelRollForward;
    public UnityEvent EventOnSpecialInteraction_ScrollWheelRollBackward;
    public UnityEvent EventOnSpecialInteraction_LMBAndRMBClick;
    public UnityEvent EventOnSpecialInteraction_LMBAndRMBHold;
    public UnityEvent EventOnSpecialInteraction_MouseShakeLeftRight;
    public UnityEvent EventOnSpecialInteraction_MouseShakeUpDown;




    private void Awake()
    {
    }

    public new void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Escaped");
            EventOnPressEscape.Invoke();
            interactedAndClicked = false;
            hasEventPostInteractionTriggered = false;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked; UnityEngine.Cursor.visible = false;
        }

        if (playerInteract.hasRecorderTurnedOn)
        {
            if (interactedAndClicked)
            {
                //if (Input.GetMouseButtonDown(0)) coroutineRef = StartCoroutine(RunRecordEvents());
                //if (Input.GetMouseButtonUp(0)) StopCoroutine(coroutineRef);                           //In case we want a hold to record input
            }
        }
        else
        {
            if (interactedAndClicked)
            {
                if (!hasEventPostInteractionTriggered)
                {
                    EventPostInteraction.Invoke();
                    UnityEngine.Cursor.lockState = CursorLockMode.None; UnityEngine.Cursor.visible = true;
                    Debug.Log("PAused");
                    hasEventPostInteractionTriggered = true; 
                }

                else if (hasEventPostInteractionTriggered)
                {
                    #region Run Event based on Input Type
                    //Input_LMBClick
                    if (Input_LMBClick)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            float timeSinceLastClick = Time.time - lastClickTime;

                            if (timeSinceLastClick <= doubleClickTime) coroutineRef = StartCoroutine(RunDoubleClickEvent()); //Debug.Log("Double click");
                            else coroutineRef = StartCoroutine(RunClickEvent());                                             //Debug.Log("Normal click");

                            lastClickTime = Time.time;
                        }
                    }

                    //Input_LMBSlideLeft
                    if (Input_LMBSlideLeft)
                    {
                        if (Input.GetMouseButton(0))
                        {
                            if (Input.GetAxis("Mouse X") < 0) coroutineRef = StartCoroutine(RunSlideLeftEvent());
                            if (Input.GetMouseButtonUp(0)) StopCoroutine(coroutineRef);
                        }
                    }

                    //Input_LMBSlideRight
                    if (Input_LMBSlideRight)
                    {
                        if (Input.GetMouseButton(0))
                        {
                            if (Input.GetAxis("Mouse X") > 0) coroutineRef = StartCoroutine(RunSlideRightEvent());
                            if (Input.GetMouseButtonUp(0)) StopCoroutine(coroutineRef);
                        }
                    }

                    //Input_LMBHoldUp
                    if (Input_LMBSlideUp)
                    {
                        if (Input.GetMouseButton(0))
                        {
                            if (Input.GetAxis("Mouse Y") > 0) coroutineRef = StartCoroutine(RunSlideUpEvent());
                            if (Input.GetMouseButtonUp(0)) StopCoroutine(coroutineRef);
                        }
                    }

                    //Input_LMBSlideDown
                    if (Input_LMBSlideDown)
                    {
                        if (Input.GetMouseButton(0))
                        {
                            if (Input.GetAxis("Mouse Y") < 0) coroutineRef = StartCoroutine(RunSlideDownEvent());
                            if (Input.GetMouseButtonUp(0)) StopCoroutine(coroutineRef);
                        }
                    }

                    //Input_ScrollWheelRollForward
                    if (Input_ScrollWheelRollForward)
                    {
                        if (Input.mouseScrollDelta.y > 0) coroutineRef = StartCoroutine(RunScrollWheelRollForwardEvent());
                    }

                    //Input_ScrollWheelRollBackward
                    if (Input_ScrollWheelRollBackward)
                    {
                        if (Input.mouseScrollDelta.y < 0) coroutineRef = StartCoroutine(RunScrollWheelRollBackwardEvent());
                    }

                    //Input_LMBAndRMBClick
                    if (Input_LMBAndRMBClick)
                    {
                        if (Input.GetMouseButton(0) && Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0) && Input.GetMouseButton(1))
                            coroutineRef = StartCoroutine(RunLMBAndRMBClickEvent());
                    }

                    //Input_LMBAndRMBHold
                    if (Input_LMBAndRMBHold)
                    {
                        if (Input.GetMouseButton(0) && Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0) && Input.GetMouseButton(1))
                        { coroutineRef = StartCoroutine(RunLMBAndRMBHoldEvent()); }
                        else if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)) StopCoroutine(coroutineRef);
                    }

                    //Input_MouseShake_LeftRight
                    if (Input_MouseShake_LeftRight)
                    {
                        if (rage_MouseShake)         //If player needs to shake mouse <X> number of times within a short time frame. If set to false, number of shake becomes cumulative overtime.
                        {
                            if (shakeCount != 0) StartCoroutine(ResetShakeCount());
                        }

                        if (Input.GetMouseButton(0))
                        {
                            if (Input.GetAxis("Mouse X") > 0)
                            {
                                StartCoroutine(MouseShakeWaitForLeft());
                            }
                            if (Input.GetAxis("Mouse X") < 0)
                            {
                                StartCoroutine(MouseShakeWaitForRight());
                            }

                            if (mouseShakeWaitForLeft)
                            {
                                if (Input.GetAxis("Mouse X") < 0)
                                {
                                    shakeCount++;
                                    Debug.Log(shakeCount);
                                }
                            }
                            if (mouseShakeWaitForRight)
                            {
                                if (Input.GetAxis("Mouse X") > 0)
                                {
                                    shakeCount++;
                                    Debug.Log(shakeCount);
                                }
                            }

                        }

                        if (shakeCount > MouseShake_LeftRight_Threshold)
                        {
                            coroutineRef = StartCoroutine(RunMouseShakeLeftRightEvent());
                        }
                    }

                    //Input_MouseShake_UpDown
                    if (Input_MouseShake_UpDown)
                    {
                        if (rage_MouseShake)         //If player needs to shake mouse <X> number of times within a short time frame. If set to false, number of shake becomes cumulative overtime.
                        {
                            if (shakeCount != 0) StartCoroutine(ResetShakeCount());
                        }

                        if (Input.GetMouseButton(0))
                        {
                            if (Input.GetAxis("Mouse Y") > 0)
                            {
                                StartCoroutine(MouseShakeWaitForLeft());
                            }
                            if (Input.GetAxis("Mouse Y") < 0)
                            {
                                StartCoroutine(MouseShakeWaitForRight());
                            }

                            if (mouseShakeWaitForLeft)
                            {
                                if (Input.GetAxis("Mouse Y") < 0)
                                {
                                    shakeCount++;
                                    Debug.Log(shakeCount);
                                }
                            }
                            if (mouseShakeWaitForRight)
                            {
                                if (Input.GetAxis("Mouse Y") > 0)
                                {
                                    shakeCount++;
                                    Debug.Log(shakeCount);
                                }
                            }

                        }

                        if (shakeCount > MouseShake_UpDown_Threshold)
                        {
                            coroutineRef = StartCoroutine(RunMouseShakeUpDownEvent());
                        }
                    }
                    #endregion
                }

            }
        }
    }

    //Enumerators for mouse shake
    IEnumerator MouseShakeWaitForRight()
    {
        mouseShakeWaitForRight = true;
        yield return new WaitForSeconds(0.2f);
        mouseShakeWaitForRight = false;
    }

    IEnumerator MouseShakeWaitForLeft()
    {
        mouseShakeWaitForLeft = true;
        yield return new WaitForSeconds(0.2f);
        mouseShakeWaitForLeft = false;
    }

    IEnumerator ResetShakeCount()
    {
        yield return new WaitForSeconds(1f);
        shakeCount = 0;
    }

    //---------------------------------------------------------------------------------


    //Triggered Event
    #region Declare Event based on Input Type
    IEnumerator RunClickEvent()
    {
        EventOnSpecialInteraction_LMBClick.Invoke();
        yield return new WaitForSeconds(1);                                                       //Put this line on top or middle of this section of code in case we want a hold input to complete the recording
    }

    IEnumerator RunDoubleClickEvent()
    {
        EventOnSpecialInteraction_LMBDoubleClick.Invoke();
        yield return new WaitForSeconds(1);                                                       //Put this line on top or middle of this section of code in case we want a hold input to complete the recording
    }

    IEnumerator RunSlideLeftEvent()
    {
        yield return new WaitForSeconds(buttonHoldTime);
        EventOnSpecialInteraction_LMBSlideLeft.Invoke();                                                      
    }

    IEnumerator RunSlideRightEvent()
    {
        yield return new WaitForSeconds(buttonHoldTime);
        EventOnSpecialInteraction_LMBSlideRight.Invoke();
    }

    IEnumerator RunSlideUpEvent()
    {
        yield return new WaitForSeconds(buttonHoldTime);
        EventOnSpecialInteraction_LMBSlideUp.Invoke();
    }

    IEnumerator RunSlideDownEvent()
    {
        yield return new WaitForSeconds(buttonHoldTime);
        EventOnSpecialInteraction_LMBSlideDown.Invoke();
    }

    IEnumerator RunScrollWheelRollForwardEvent()
    {
        EventOnSpecialInteraction_ScrollWheelRollForward.Invoke();
        yield return new WaitForSeconds(buttonHoldTime);
    }

    IEnumerator RunScrollWheelRollBackwardEvent()
    {
        EventOnSpecialInteraction_ScrollWheelRollBackward.Invoke();
        yield return new WaitForSeconds(buttonHoldTime);
    }

    IEnumerator RunLMBAndRMBClickEvent()
    {
        EventOnSpecialInteraction_LMBAndRMBClick.Invoke();
        yield return new WaitForSeconds(1);                                                       //Put this line on top or middle of this section of code in case we want a hold input to complete the recording
    }

    IEnumerator RunLMBAndRMBHoldEvent()
    {
        yield return new WaitForSeconds(buttonHoldTime);
        EventOnSpecialInteraction_LMBAndRMBHold.Invoke();
    }

    IEnumerator RunMouseShakeLeftRightEvent()
    {
        EventOnSpecialInteraction_MouseShakeLeftRight.Invoke();
        yield return new WaitForSeconds(1);                                                       //Put this line on top or middle of this section of code in case we want a hold input to complete the recording
    }

    IEnumerator RunMouseShakeUpDownEvent()
    {
        EventOnSpecialInteraction_MouseShakeUpDown.Invoke();
        yield return new WaitForSeconds(1);                                                       //Put this line on top or middle of this section of code in case we want a hold input to complete the recording
    }
    #endregion

    //
    IEnumerator RunEventOnSpecialInteractionWithDelay()
    {
        yield return new WaitForSeconds(buttonHoldTime);
        EventOnSpecialInteraction_LMBClick.Invoke();
    }


}
