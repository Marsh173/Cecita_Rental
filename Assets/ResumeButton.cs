using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResumeButton : MonoBehaviour
{
    public PauseGameController p;
    public GameObject settings_ui;

    public Sprite selected;
    public Sprite normal;

    private void Start()
    {
        p = GameObject.FindObjectOfType<PauseGameController>();
        changesprite(false);
    }
    public void resume()
    {
        p.ResumeGame();
    }

    public void home()
    {
        SceneManager.LoadScene("TemporaryMenu", LoadSceneMode.Single);
    }
    public void settings()
    {
        settings_ui.SetActive(!settings_ui.activeSelf);
    }

    public void LastCheckPoint()
    {

    }

    public void changesprite(bool select)
    {
        Sprite button = select ? selected : normal;
        GetComponent<Image>().sprite = button;
    }


}
