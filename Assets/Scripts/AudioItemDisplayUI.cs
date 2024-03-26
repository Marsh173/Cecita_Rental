using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioItemDisplayUI : MonoBehaviour
{
    public float progress = 0;
    public RectTransform t;
    public float maxLength = 1222;
    public float xpos;

    public TextMeshProUGUI audiolength;

    public AudioSource a;
    public Slider s;

    private void Start()
    {
        t = GetComponent<RectTransform>();
        xpos = GetComponent<RectTransform>().anchoredPosition.x;
    }
    private void Update()
    {
        if(a.clip != null)
        {
            progress = a.time / a.clip.length;
            t.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, progress * maxLength);
            t.anchoredPosition = new Vector3(xpos + progress * maxLength * 0.5f, t.anchoredPosition.y, 0);
            var timestamp = Mathf.RoundToInt(a.clip.length * (1 - progress));
            string min = Mathf.FloorToInt(timestamp / 60).ToString();
            int sec = timestamp % 60;
            string second = "";

            if (min.Length < 2) min = "0" + min;
            if (sec < 10) second = "0" + sec;
            else second = sec.ToString();
            

            audiolength.text = min + " : " + second;
            if (a.isPlaying)
            {
                s.value = progress;
            }

        }

        
       
    }
}
