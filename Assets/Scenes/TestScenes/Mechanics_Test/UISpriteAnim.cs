using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UISpriteAnim : MonoBehaviour
{
    Animator anim;
    public string animatorBool;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            anim.SetBool(animatorBool, true);
        }
        else
        {
            anim.StopPlayback();
        }
    }
}
