using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public CamFollow2 cameraFollow; // Reference to the CameraFollow script

    private void OnTriggerEnter(Collider other)
    {
        // Check if both players are in the trigger area
        if (other.transform == cameraFollow.player1 || other.transform == cameraFollow.player2)
        {
            CheckPlayersInTrigger();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player leaving is one of the players
        if (other.transform == cameraFollow.player1 || other.transform == cameraFollow.player2)
        {
            CheckPlayersInTrigger();
        }
    }

    private void CheckPlayersInTrigger()
    {
        // Check if both players are within the trigger area
        if (IsPlayerInTrigger(cameraFollow.player1) && IsPlayerInTrigger(cameraFollow.player2))
        {
            cameraFollow.StartFollowing();
        }
        else
        {
            cameraFollow.StopFollowing();
        }
    }

    private bool IsPlayerInTrigger(Transform player)
    {
        // Check if the player is within the bounds of the trigger collider
        return GetComponent<Collider>().bounds.Contains(player.position);
    }
}