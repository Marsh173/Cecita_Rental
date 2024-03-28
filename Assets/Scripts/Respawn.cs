using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Respawn : MonoBehaviour
{
    //public GameObject playerInScene;
    [SerializeField] private Transform checkpoint;
    public static bool dead, restarted;
    private Transform playerPos;
    private Animator playerAnim;
    public GameObject placeholderDeathText, deathCanvas, deathTimeLine, system;
    [SerializeField] GameObject checkpointReachedUI;

    void Start()
    {
        dead = restarted = false;
        placeholderDeathText.SetActive(false);
        playerPos = GetComponent<Transform>();

        playerAnim = transform.GetChild(0).GetComponent<Animator>();
        
        deathCanvas.SetActive(false);
        //deathTimeLine.SetActive(false);
        system.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //check death status
        deathSituations();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("checkpoint"))
        {
            if (checkpoint != other.transform)
            {
                checkpoint = other.transform;
                if(checkpointReachedUI!= null)StartCoroutine(checkpointani());
            }
            
        }
    }

    public IEnumerator checkpointani()
    {
        checkpointReachedUI.GetComponent<Animator>().SetBool("checkpoint", true);
        yield return new WaitForSeconds(1.5f);
        checkpointReachedUI.GetComponent<Animator>().SetBool("checkpoint", false);

    }

    public void deathSituations()
    {
        if (dead)
        {
            restarted = false;
            StartCoroutine(DeathSequence());
        }

    }
    IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(0.01f);
        dead = false;

        GetComponent<FirstPersonAIO>().speed = 0;
        GetComponent<FirstPersonAIO>().enabled = false;
        system.SetActive(false);

        //play death cutscen
        playerAnim.SetBool("Dead", true);
        //deathTimeLine.SetActive(true);
        //deathTimeLine.GetComponent<PlayableDirector>().Play();
        yield return new WaitForSeconds(9f);
        Debug.Log("Animation finished playing");
        Cursor.visible = true;
        deathCanvas.SetActive(true);
    }

    public void RespawnPlayer()
    {
        if (checkpoint != null)
        {
            Cursor.visible = false;
            system.SetActive(true);
            deathCanvas.SetActive(false);
            //deathTimeLine.SetActive(false);
            playerAnim.SetBool("Dead", false);

            playerPos.SetPositionAndRotation(checkpoint.transform.position, checkpoint.transform.rotation);
            GetComponent<FirstPersonAIO>().enabled = true;

            if (placeholderDeathText != null)
            {
                placeholderDeathText.SetActive(true);
            }
        }
        restarted = true;
        Debug.Log("restarted? "+restarted);
    }
}
