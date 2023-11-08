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
            if (SceneManager.GetActiveScene().name == "TutorialLevel")
            {
                StartCoroutine(RespawnRoutiine());
            }
            //else SceneManager.LoadScene("Death");
            StartCoroutine(RespawnRoutiine());
        }

    }
    IEnumerator RespawnRoutiine()
    {
        dead = false;
        yield return new WaitForSeconds(0.1f);
        if(checkpoint != null)
        {
            playerPos.SetPositionAndRotation(checkpoint.transform.position, checkpoint.transform.rotation);

            //playerPos = checkpoint;
            /*transform.position = checkpoint.position;
            transform.rotation = checkpoint.rotation;*/

            Debug.Log("respawned");
            Debug.Log(playerPos);

            if (placeholderDeathText != null)
            {
                placeholderDeathText.SetActive(true);
            }
        }
    }
}
