using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    private Animator monsterAnim;
    private void Start()
    {
        Respawn.dead = false;
        monsterAnim = GetComponentInChildren<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Respawn.dead = true;
        }
    }

    IEnumerator MonsterLook()
    {
        yield return new WaitForSeconds(12f);
    }
}
