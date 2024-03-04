using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UISpriteAnim : MonoBehaviour
{
    [Tooltip("List of sprites to animate")]
    public Sprite[] sprites;

    [Tooltip("Frames per second")]
    public float frameRate = 12;

    [Tooltip("Loop animation when finished")]
    public bool loop = false;

    /*[Tooltip("Destroy object when animation finished")]
    public bool destroyOnFinish = false;*/

    private Image image;
    private int spriteIndex;
    private float timer;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if(this.gameObject.activeInHierarchy)
        {
            spriteIndex = 0;
            Debug.Log("Animation index start " + spriteIndex);
        }

        timer += Time.deltaTime;

        if (timer >= 1 / frameRate)
        {
            spriteIndex++;

            if (spriteIndex >= sprites.Length)
            {
                spriteIndex = loop ? 0 : sprites.Length - 1;

                /*if (destroyOnFinish)
                {
                    Destroy(gameObject);
                    return;
                }*/
            }

            image.sprite = sprites[spriteIndex];
            timer -= 1 / frameRate;
        }
        else loop = true;
    }
}
