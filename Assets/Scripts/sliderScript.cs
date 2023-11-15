using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sliderScript : MonoBehaviour
{
    private float pressSeconds = 3f;
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 0;
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
            slider.value -= 100 / 4f * Time.deltaTime;
        }

        if(slider.value >= 100)
        {
            SceneManager.LoadScene("TutorialLevel");
        }
    }
}
