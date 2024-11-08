using UnityEngine;

public class MovingUpwards : MonoBehaviour
{
    public float speed = 2f; // Speed at which the empty object moves upward

    private void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime; // Move the object up on the Y-axis
    }
}
