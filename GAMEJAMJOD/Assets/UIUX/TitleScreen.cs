using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class TitleScreen : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Assign the Video Player in the Inspector
    public string MainMenu; // Name of the main menu scene

    void Start()
    {
        videoPlayer.Play();  // Start playing the video
    }

    void Update()
    {
        // Check if any key is pressed
        if (Input.anyKeyDown)
        {
            LoadMainMenu();
        }
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene(MainMenu);  // Load the main menu scene
    }
}
