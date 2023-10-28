using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    public GameObject playerInScene, /*playerPrefab,*/ checkpoint;
    public static bool dead, restarted;
    // Start is called before the first frame update
    void Start()
    {
        dead = restarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        //check death status
        deathSituations();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            checkpoint = gameObject;
        }
    }

    public void deathSituations()
    {
        //instant restart when in tutorial level
        if (dead)
        {
            if (SceneManager.GetActiveScene().name == "TutorialLevel" || SceneManager.GetActiveScene().name == "TutorialLevel restart")
            {
                //SceneManager.LoadScene("TutorialLevel restart");
                StartCoroutine(RespawnRoutiine());
            }
            else SceneManager.LoadScene("Death");
        }

    }
    IEnumerator RespawnRoutiine()
    {
        dead = false;
        //playerInScene = GameObject.Find("FirstPerson-AIO");
        //Destroy(playerInScene, 1f);
        yield return new WaitForSeconds(1f);
        //Instantiate(playerPrefab, checkpoint.transform);
        Debug.Log("respawned");
        playerInScene.transform.position = checkpoint.transform.position;
    }
}
