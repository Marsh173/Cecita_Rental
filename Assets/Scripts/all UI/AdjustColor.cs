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
    public float gamma = 2.2f;


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
            
            //bgImage.color = new Color(blackS.value, blackS.value, blackS.value, 1f);

            bgImage.color = ApplyGammaCorrection(gamma);

            eyeBgColor = bgImage.color;
            sliderValueHolder = blackS.value;
            holderVisualizer = sliderValueHolder;
        }
        else settingsOn = false;
    }

    private Color ApplyGammaCorrection(float gamma)
    {
        float gammaCorrection = 1f / gamma;

        float r = Mathf.Pow(blackS.value, gammaCorrection);
        float g = Mathf.Pow(blackS.value, gammaCorrection);
        float b = Mathf.Pow(blackS.value, gammaCorrection);

        return new Color(r, g, b, 1);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
