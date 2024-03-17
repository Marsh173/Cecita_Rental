using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    private Animator monsterAnim;
    private GameObject footstep, glitch;
    private Vector3 originalPosF, originalPosG;
    private void Start()
    {
        Respawn.dead = false;
        monsterAnim = GetComponentInChildren<Animator>();
        footstep = transform.GetChild(1).gameObject;
        glitch = transform.GetChild(2).gameObject;
        originalPosF = footstep.transform.localPosition;
        originalPosG = glitch.transform.localPosition;

        Debug.Log("F " + originalPosF + "G " + originalPosG);

    }

    private void OnEnable()
    {
        footstep.transform.localPosition = originalPosF;
        glitch.transform.localPosition = originalPosG;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Respawn.dead = true;
            footstep.transform.localPosition = new Vector3(originalPosF.x, originalPosF.y-7, originalPosF.z);
            glitch.transform.localPosition = new Vector3(originalPosG.x, originalPosG.y-7, originalPosG.z);
        }
    }
    
    IEnumerator MonsterLook()
    {
        yield return new WaitForSeconds(9f);
    }
}
