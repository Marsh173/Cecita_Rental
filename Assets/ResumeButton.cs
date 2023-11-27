using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResumeButton : MonoBehaviour
{
    public PauseGameController p;
    public GameObject settings_ui;

    private void Start()
    {
        p = GameObject.FindObjectOfType<PauseGameController>();
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


}
