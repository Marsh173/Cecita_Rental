using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FMODUnity
{
    public class MainMenu : MonoBehaviour
    {
        public static bool Setting;
        FMOD.ChannelGroup mcg;
        FMOD.Studio.Bus Masterbus;

        public bool startMenu;

        private void Start()
        {
            Time.timeScale = 1;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                startMenu = true;
            }
        }

        public void ExitButton()
        {
            Application.Quit();
            Debug.Log("Game Closed");
        }

        public void LoadScene(string sceneName)
        {
            FMODUnity.RuntimeManager.CoreSystem.getMasterChannelGroup(out mcg);
            mcg.stop();
            SceneManager.LoadScene(sceneName);
        }

        
    }
}