using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDoorAnimation : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;
    private bool openTrigger = false;
    private bool loopAnimation = true;

    [SerializeField] private AnimationClip open;
    [SerializeField] private AnimationClip close;
    private string openAnimationName;
    private string closeAnimationName;

    [SerializeField] private float OpenmaxDelay = 3;
    [SerializeField] private float OpenminDelay = 2f;
    [SerializeField] private float ClosemaxDelay = 2;
    [SerializeField] private float CloseminDelay = 1f;
    private float actualODelay;
    private float actualCDelay;

    private void Start()
    {
        openAnimationName = open.name;
        closeAnimationName = close.name;
        loopAnimation = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {/*
            if (!openTrigger)
            {*/
                Debug.Log("open");
                myDoor.Play(openAnimationName, 0, 0.0f);
                gameObject.GetComponent<BoxCollider>().enabled = false;
                openTrigger = true;

            //}
        }
    }

    private void Update()
    {
        if(loopAnimation)
        {
            loopAnimation = false;
            StartCoroutine(animationFrequent());
        }
    }

    IEnumerator animationFrequent()
    {
        actualCDelay = Random.Range(CloseminDelay, ClosemaxDelay);

        if (myDoor.GetCurrentAnimatorStateInfo(0).IsName(openAnimationName))
        {
            yield return new WaitForSeconds(actualCDelay); 
            myDoor.Play(closeAnimationName, 0, 0.0f);
            Debug.Log("close");
        }
        
        actualODelay = Random.Range(OpenminDelay, OpenmaxDelay);
        if (myDoor.GetCurrentAnimatorStateInfo(0).IsName(closeAnimationName))
        {
            yield return new WaitForSeconds(actualODelay);
            myDoor.Play(openAnimationName, 0, 0.0f);
            Debug.Log("open2");
        }
        loopAnimation = true;
    }
}
