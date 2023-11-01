using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FMODUnity
{
    public class MainMenu : MonoBehaviour
    {
        public static bool Setting;
        //public GameObject settingMenu;
        FMOD.ChannelGroup mcg;
        FMOD.Studio.Bus Masterbus;

        public string sceneName;


        public void ExitButton()
        {
            Application.Quit();
            Debug.Log("Game Closed");
        }

        public void LoadScene()
        {
            FMODUnity.RuntimeManager.CoreSystem.getMasterChannelGroup(out mcg);
            mcg.stop();
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
}