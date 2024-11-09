using UnityEngine;

public class CamFollow2 : MonoBehaviour
{
    public Transform player1; // Reference to the first player
    public Transform player2; // Reference to the second player
    public float smoothSpeed = 0.125f; // Speed of the camera's smooth movement

    private bool isFollowing = false; // Flag to determine if the camera should follow

    void LateUpdate()
    {
        // If following, update the camera position
        if (isFollowing)
        {
            // Calculate the midpoint between the players
            Vector3 midpoint = (player1.position + player2.position) / 2f;

            // Calculate the target position based on the midpoint
            Vector3 targetPosition = new Vector3(transform.position.x, midpoint.y, transform.position.z);

            // Smoothly interpolate the camera's position to the target
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        }
    }

    // Method to start following the players
    public void StartFollowing()
    {
        isFollowing = true;
    }

    // Method to stop following the players
    public void StopFollowing()
    {
        isFollowing = false;
    }
}