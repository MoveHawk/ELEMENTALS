using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float smoothSpeed = 0.125f;
    private Vector3 offset;

    void Start()
    {
        // Set initial offset between camera and players' midpoint
        Vector3 midpoint = (player1.position + player2.position) / 2f;
        offset = transform.position - midpoint;
    }

    void LateUpdate()
    {
        // Calculate the midpoint between the players
        Vector3 midpoint = (player1.position + player2.position) / 2f;

        // Ensure the camera only moves vertically based on players' average height
        Vector3 targetPosition = new Vector3(transform.position.x, midpoint.y + offset.y, transform.position.z);

        // Smoothly interpolate the camera's position to the target
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
    }
}
