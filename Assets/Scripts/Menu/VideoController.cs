using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public UnityEvent onVideoEnd = new UnityEvent();

    private void Start()
    {
        if (videoPlayer == null)
        {
            // If VideoPlayer is not assigned, try to find one on the current GameObject
            videoPlayer = GetComponent<VideoPlayer>();
        }

        if (videoPlayer != null)
        {
            // Subscribe to the loop point reached event
            videoPlayer.loopPointReached += OnVideoEnd;
        }
        else
        {
            Debug.LogError("VideoPlayer component not found or assigned.");
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // This method will be called when the video reaches its end
        Debug.Log("Video has ended!");
        onVideoEnd.Invoke();
    }

    public void SkipToVideoEnd()
    {
        if (videoPlayer != null)
        {
            // Skip the video to the end
            videoPlayer.time = videoPlayer.length;

            // Trigger the end event manually (in case it didn't naturally reach the end)
            OnVideoEnd(videoPlayer);
        }
        else
        {
            Debug.LogError("VideoPlayer component not found.");
        }
    }
}