using UnityEngine;

public class CoOpMechanicTrigger : MonoBehaviour
{
    public GameObject coOpMechanicObject; // Assign the GameObject containing CoOpMechanicUnlock in the Inspector
    private CoOpMechanicUnlock coOpMechanicScript;

    private int playersInArea = 0; // Counter to track how many players are in the area

    private void Awake()
    {
        if (coOpMechanicObject != null)
        {
            coOpMechanicScript = coOpMechanicObject.GetComponent<CoOpMechanicUnlock>();
        }

        if (coOpMechanicScript != null)
        {
            coOpMechanicScript.enabled = false; // Disable by default
        }
        else
        {
            Debug.LogError("CoOpMechanicUnlock component not found on the specified GameObject.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure the collider has the "Player" tag
        {
            playersInArea++; // Increment the counter when a player enters

            if (playersInArea == 2 && coOpMechanicScript != null) // Check if both players are in the area
            {
                coOpMechanicScript.enabled = true; // Enable the mechanic script
                Debug.Log("CoOpMechanicUnlock enabled!");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure the collider has the "Player" tag
        {
            playersInArea--; // Decrement the counter when a player exits

            if (playersInArea < 2 && coOpMechanicScript != null) // If less than 2 players are in the area
            {
                coOpMechanicScript.enabled = false; // Disable the mechanic script
                Debug.Log("CoOpMechanicUnlock disabled!");
            }
        }
    }
}
