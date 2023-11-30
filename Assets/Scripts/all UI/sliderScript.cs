using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sliderScript : MonoBehaviour
{
    private float pressSeconds = 2f;
    private Slider slider;
    public string sceneName;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            slider.value += 100 / pressSeconds * Time.deltaTime;
        }
        else
        {
            slider.value -= 100 / (pressSeconds+1f) * Time.deltaTime;
        }

        if(slider.value >= 100)
        {
            NextScene();
        }
    }

    private void NextScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
