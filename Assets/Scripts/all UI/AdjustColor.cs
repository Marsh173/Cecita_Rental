using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AdjustColor : MonoBehaviour
{
    public RawImage bgImage;
    private Slider blackS;
    [SerializeField] public static float sliderValueHolder = 0.2f;
    [SerializeField] public static Color eyeBgColor;
    public static bool settingsOn;
    public float holderVisualizer;
    public string sceneName;

    void Start()
    {
        settingsOn = false;
        blackS = GetComponent<Slider>();
        if (sliderValueHolder != 0.2f)
        {
            blackS.value = sliderValueHolder;
        }
        else blackS.value = 0.2f;

        bgImage.color = eyeBgColor = new Color(blackS.value, blackS.value, blackS.value, 1f);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            settingsOn = true;
            bgImage.color = new Color(blackS.value, blackS.value, blackS.value, 1f);
            eyeBgColor = bgImage.color;
            sliderValueHolder = blackS.value;
            holderVisualizer = sliderValueHolder;
        }
        else settingsOn = false;
    }

    public void NextScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
