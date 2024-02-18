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
    public GameObject placeholderDeathText;

    void Start()
    {
        dead = restarted = false;
        placeholderDeathText.SetActive(false);
        playerPos = GetComponent<Transform>();
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
        //instant restart when in tutorial level
        if (dead)
        {
            restarted = false;
            if (SceneManager.GetActiveScene().name == "TutorialLevel")
            {
                StartCoroutine(RespawnRoutine());
            }
            //else SceneManager.LoadScene("Death");
            StartCoroutine(RespawnRoutine());
        }

    }
    IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(0.01f);
        dead = false;
        yield return new WaitForSeconds(0.05f);

        //play death cutscene

        if(checkpoint != null)
        {
            playerPos.SetPositionAndRotation(checkpoint.transform.position, checkpoint.transform.rotation);

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

        yield return new WaitForSeconds(10f);
        placeholderDeathText.SetActive(false);
    }
}
