using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AdjustColor : MonoBehaviour
{
    public RawImage bgImage;
    [SerializeField] private Slider blackS;
    public static Color eyeBgColor;
    public string sceneName;

    void Start()
    {
        blackS = GetComponent<Slider>();
        bgImage.color = eyeBgColor;
        blackS.value = 0.2f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        bgImage.color = new Color(blackS.value, blackS.value, blackS.value,1f);
        eyeBgColor = bgImage.color;
    }

    public void NextScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
