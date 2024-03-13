using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    //public GameObject playerInScene;
    [SerializeField] private Transform checkpoint;
    public static bool dead, restarted;
    private Transform playerPos;
    private Animator playerAnim;
    public GameObject placeholderDeathText, deathCanvas, system;

    void Start()
    {
        dead = restarted = false;
        placeholderDeathText.SetActive(false);
        playerPos = GetComponent<Transform>();

        playerAnim = transform.GetChild(0).GetComponent<Animator>();

        deathCanvas.SetActive(false);
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
            checkpoint = other.transform;
        }
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
        yield return new WaitForSeconds(12f);
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
            playerAnim.SetBool("Dead", false);

            playerPos.SetPositionAndRotation(checkpoint.transform.position, checkpoint.transform.rotation);
            GetComponent<FirstPersonAIO>().enabled = true;

            if (placeholderDeathText != null)
            {
                placeholderDeathText.SetActive(true);
            }
        }

        restarted = true;
    }
}
