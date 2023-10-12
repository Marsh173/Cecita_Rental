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

    //Input method booleans
    [Header ("Input Type")]
    public bool Input_LMBClick = false;
    public bool Input_LMBDoubleClick = false;
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
    public UnityEvent EventOnSpecialInteraction;




    private void Awake()
    {
    }

    public void Update()
    {
        if (playerInteract.hasRecorderTurnedOn)
        {
            if (interacted)
            {
                if (Input.GetMouseButtonDown(0)) coroutineRef = StartCoroutine(RunRecordEvents());
                //if (Input.GetMouseButtonUp(0)) StopCoroutine(coroutineRef);                           //In case we want a hold to record input
            }
        }
        else
        {
            if (interacted)
            {
                //Input_LMBClick
                if (Input_LMBClick)
                { if (Input.GetMouseButtonDown(0)) coroutineRef = StartCoroutine(RunEventsOnSpecialInteraction()); }

                //Input_LMBDoubleClick
                if (Input_LMBDoubleClick)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        float timeSinceLastClick = Time.time - lastClickTime;

                        if (timeSinceLastClick <= doubleClickTime) coroutineRef = StartCoroutine(RunEventsOnSpecialInteraction());
                        //Debug.Log("Double click");
                        //Debug.Log("Normal click");

                        lastClickTime = Time.time;
                    }
                }

                //Input_LMBSlideLeft
                if (Input_LMBSlideLeft)
                {
                    if (Input.GetMouseButton(0))
                    {
                        if (Input.GetAxis("Mouse X") < 0) coroutineRef = StartCoroutine(RunEventOnSpecialInteractionWithDelay());
                        if (Input.GetMouseButtonUp(0)) StopCoroutine(coroutineRef);
                    }
                }

                //Input_LMBSlideRight
                if (Input_LMBSlideRight)
                {
                    if (Input.GetMouseButton(0))
                    {
                        if (Input.GetAxis("Mouse X") > 0) coroutineRef = StartCoroutine(RunEventOnSpecialInteractionWithDelay());
                        if (Input.GetMouseButtonUp(0)) StopCoroutine(coroutineRef);
                    }
                }

                //Input_LMBHoldUp
                if (Input_LMBSlideUp)
                {
                    if (Input.GetMouseButton(0))
                    {
                        if (Input.GetAxis("Mouse Y") > 0) coroutineRef = StartCoroutine(RunEventOnSpecialInteractionWithDelay());
                        if (Input.GetMouseButtonUp(0)) StopCoroutine(coroutineRef);
                    }
                }

                //Input_LMBSlideDown
                if (Input_LMBSlideDown)
                {
                    if (Input.GetMouseButton(0))
                    {
                        if (Input.GetAxis("Mouse Y") < 0) coroutineRef = StartCoroutine(RunEventOnSpecialInteractionWithDelay());
                        if (Input.GetMouseButtonUp(0)) StopCoroutine(coroutineRef);
                    }
                }

                //Input_ScrollWheelRollForward
                if (Input_ScrollWheelRollForward)
                {
                    if (Input.mouseScrollDelta.y > 0) coroutineRef = StartCoroutine(RunEventOnSpecialInteractionWithDelay());
                }

                //Input_ScrollWheelRollBackward
                if (Input_ScrollWheelRollBackward)
                {
                    if (Input.mouseScrollDelta.y < 0) coroutineRef = StartCoroutine(RunEventOnSpecialInteractionWithDelay());
                }

                //Input_LMBAndRMBClick
                if (Input_LMBAndRMBClick)
                {
                    if (Input.GetMouseButton(0) && Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0) && Input.GetMouseButton(1))
                        coroutineRef = StartCoroutine(RunEventsOnSpecialInteraction());
                }

                //Input_LMBAndRMBHold
                if (Input_LMBAndRMBHold)
                {
                    if (Input.GetMouseButton(0) && Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0) && Input.GetMouseButton(1))
                    { coroutineRef = StartCoroutine(RunEventOnSpecialInteractionWithDelay()); }
                    else if (Input.GetMouseButtonUp(0) ||  Input.GetMouseButtonUp(1)) StopCoroutine(coroutineRef);
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
                        if (Input.GetAxis("Mouse X")> 0)
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
                        coroutineRef = StartCoroutine(RunEventsOnSpecialInteraction());
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
                        coroutineRef = StartCoroutine(RunEventsOnSpecialInteraction());
                    }
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
    IEnumerator RunEventsOnSpecialInteraction()
    {
        EventOnSpecialInteraction.Invoke();
        yield return new WaitForSeconds(0);                                                       //Put this line on top or middle of this section of code in case we want a hold input to complete the recording
    }

    IEnumerator RunEventOnSpecialInteractionWithDelay()
    {
        yield return new WaitForSeconds(buttonHoldTime);
        EventOnSpecialInteraction.Invoke();
    }




}
