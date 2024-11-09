using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    private bool player1Collected = false;
    private bool player2Collected = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Method to be called when a player collects a diamond
    public void OnPlayerCollectedDiamond(int playerID)
    {
        if (playerID == 1) player1Collected = true;
        else if (playerID == 2) player2Collected = true;

        // Check if both players have collected their diamonds
        if (player1Collected && player2Collected)
        {
            ProceedToNextLevel();
        }
    }

    private void ProceedToNextLevel()
    {
        Debug.Log("Both players collected diamonds! Proceeding to next level.");
        // Add code to load the next level or complete the current level
    }
}
