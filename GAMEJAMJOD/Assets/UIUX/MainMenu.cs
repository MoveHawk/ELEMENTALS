using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string cutsceneSceneName = "CutsceneScene"; // Name of the cutscene scene

    public void PlayGame()
    {
        // Load the cutscene scene
        SceneManager.LoadScene(cutsceneSceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Game quit hoya Guru!");
        Application.Quit();
    }
}
