using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneController : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Reference to the Video Player component
    public string firstLevelSceneName = "Level 1"; // Name of the first level scene

    void Start()
    {
        // Start playing the video on scene load
        videoPlayer.Play();

        // Subscribe to the loopPointReached event to detect when the video ends
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void Update()
    {
        // Check if any key is pressed to skip the cutscene
        if (Input.anyKeyDown)
        {
            SkipCutscene();
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Unsubscribe from the event to prevent multiple calls
        videoPlayer.loopPointReached -= OnVideoEnd;

        // Load the first level after the video ends
        LoadFirstLevel();
    }

    void SkipCutscene()
    {
        // Stop the video and immediately load the first level
        videoPlayer.Stop();
        LoadFirstLevel();
    }

    void LoadFirstLevel()
    {
        SceneManager.LoadScene(firstLevelSceneName);
    }
}
