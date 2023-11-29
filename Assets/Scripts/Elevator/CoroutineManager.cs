using System.Collections;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    private static CoroutineManager instance;

    private void Awake()
    {
        instance = this;
    }

    public static void StartStaticCoroutine(IEnumerator coroutine)
    {
        instance.StartCoroutine(coroutine);
    }
}