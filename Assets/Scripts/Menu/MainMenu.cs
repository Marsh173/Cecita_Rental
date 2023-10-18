using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static bool Setting;
    //public GameObject settingMenu;

    public string sceneName;
    

    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    //public void BackButton()
    //{
    //    SceneManager.LoadScene("Start_Screen");
    //}
    /*
    public void SettingButton()
    {
        Setting = true;
        settingMenu.SetActive(true);
    }

    public void BackButton()
    {
        Setting = false;
        settingMenu.SetActive(false);
    }
    */
}
