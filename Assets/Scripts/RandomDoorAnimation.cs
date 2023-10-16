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
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!openTrigger)
            {
                Debug.Log("open");
                myDoor.Play(openAnimationName, 0, 0.0f);
                gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y - 100f, gameObject.transform.position.z);
                openTrigger = true;
                Debug.Log(openTrigger);
            }
        }
    }

    private void Update()
    {
        actualODelay = Random.Range(OpenminDelay, OpenmaxDelay);
        actualCDelay = Random.Range(CloseminDelay, ClosemaxDelay);
        
        if(loopAnimation)
        {
            Debug.Log("start");
            StartCoroutine(animationFrequent());
        }
    }

    IEnumerator animationFrequent()
    {
        loopAnimation = false;
        if (myDoor.GetCurrentAnimatorStateInfo(0).IsName(openAnimationName))
        {
            yield return new WaitForSeconds(actualCDelay);
            myDoor.Play(closeAnimationName, 0, 0.0f);
        }

        Debug.Log("close");
        if(myDoor.GetCurrentAnimatorStateInfo(0).IsName(closeAnimationName))
        {
            yield return new WaitForSeconds(actualODelay);
            myDoor.Play(openAnimationName, 0, 0.0f);
        }
        Debug.Log("open2");
        loopAnimation = true;
    }
}
