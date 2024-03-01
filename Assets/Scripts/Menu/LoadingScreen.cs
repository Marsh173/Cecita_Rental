using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Linq.Expressions;

public class LoadingScreen : MonoBehaviour
{
    public GameObject LoadingScreenUI;
    public TextMeshProUGUI loadingText;
    public Image LoadingBackground;

    public float text_stay_second = 3.0f;
    private Coroutine loadingTextCoroutine;

    [SerializeField, TextArea(3, 10)] private string[] tips;
    [SerializeField] private Sprite[] loadingImages;

    public int GetSceneBuildIndex(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneNameInBuildSettings = System.IO.Path.GetFileNameWithoutExtension(scenePath);

            if (sceneName == sceneNameInBuildSettings)
            {
                return i;
            }
        }

        return -1; // Scene not found
    }

    public void LoadSceneWithLoadingScreen(string sceneName)
    {
        // Debug.Log("TEST");
        int sceneId = GetSceneBuildIndex(sceneName);

        SetRandomLoadingText();
        SetRandomLoadingImage();

        StartCoroutine(LoadSceneAsync(sceneId));
    }

    private void SetRandomLoadingText()
    {
        // Randomly select a loading text from the array
        int randomIndex = UnityEngine.Random.Range(0, tips.Length);

        // Start loop through texts every x seconds
        loadingTextCoroutine = StartCoroutine(LoopTips(randomIndex));


    }

    private IEnumerator LoopTips(int randomIndex)
    {
        while (true)
        {
            string randomLoadingText = tips[randomIndex];
            loadingText.text = randomLoadingText;
            yield return new WaitForSeconds(text_stay_second);
            randomIndex = (randomIndex + 1) % tips.Length;
        }

    }

    private void SetRandomLoadingImage()
    {
        // Randomly select a loading image from the array
        int randomIndex = UnityEngine.Random.Range(0, loadingImages.Length);
        Sprite randomLoadingImage = loadingImages[randomIndex];

        LoadingBackground.sprite = randomLoadingImage;
    }


    private IEnumerator LoadSceneAsync(int sceneId)
    {
        LoadingScreenUI.SetActive(true);

        yield return new WaitForSeconds(1f);
        //Debug.Log("1111");

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        

        while (!operation.isDone){
            yield return null;
        }

        if (loadingTextCoroutine != null)
            StopCoroutine(loadingTextCoroutine);

        LoadingScreenUI.SetActive(false);

    }
}
