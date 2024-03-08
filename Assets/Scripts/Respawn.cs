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
    public GameObject placeholderDeathText, deathCutScene, deathCanvas, system;

    void Start()
    {
        dead = restarted = false;
        placeholderDeathText.SetActive(false);
        playerPos = GetComponent<Transform>();

        deathCutScene.SetActive(false);
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

        GetComponent<FirstPersonAIO>().enabled = false;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        //play death cutscen
        deathCutScene.transform.position = playerPos.position;
        deathCutScene.transform.rotation = playerPos.rotation;
        system.SetActive(false);
        deathCutScene.SetActive(true);
        deathCutScene.GetComponent<Animator>().Play("death");

       /* if (deathCutScene.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("death"))
        {*/
            yield return new WaitForSeconds(12f);
            Debug.Log("Animation finished playing");
            Cursor.visible = true;
            deathCanvas.SetActive(true);
        //}
    }

    public void RespawnPlayer()
    {
        if (checkpoint != null)
        {
            Cursor.visible = false;
            system.SetActive(true);
            deathCutScene.SetActive(false);
            deathCanvas.SetActive(false);
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }

            playerPos.SetPositionAndRotation(checkpoint.transform.position, checkpoint.transform.rotation);
            GetComponent<FirstPersonAIO>().enabled = true;

            //playerPos = checkpoint;
            /*transform.position = checkpoint.position;
            transform.rotation = checkpoint.rotation;*/

            //Debug.Log("respawned");
            //Debug.Log(playerPos.rotation);

            if (placeholderDeathText != null)
            {
                placeholderDeathText.SetActive(true);
            }
        }

        restarted = true;
    }
}
