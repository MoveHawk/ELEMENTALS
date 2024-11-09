using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float fallDelay = 1f;
    [SerializeField] private float destroyDelay = 2f;

    private bool falling = false;

    [SerializeField] private Rigidbody2D rb;

    private void OnCollisionEnter2D(Collision2D collision)
{
    if (falling)
        return;

    // Check if the player is above the platform
    if ((collision.transform.CompareTag("Player1") || collision.transform.CompareTag("Player2")) && 
        collision.contacts[0].normal.y < -0.5f)
    {
        StartCoroutine(StartFall());
    }
}


    private IEnumerator StartFall()
    {
        falling = true;

        // Wait for a few seconds before dropping
        yield return new WaitForSeconds(fallDelay);

        // Enable rigidbody and destroy after a few seconds
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, destroyDelay);
    }
}