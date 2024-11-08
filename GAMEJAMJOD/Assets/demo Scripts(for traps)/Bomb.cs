using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("Explosion Settings")]
    public float explosionRadius = 3f; // Radius of the explosion effect
    public float explosionForce = 500f; // Force applied to objects in the explosion
    public int explosionDamage = 50; // Damage caused by the explosion
    public float explosionDelay = 0.5f; // Time delay before the bomb explodes
    public float playerTriggerDistance = 2.0f;

    [Header("References")]
    public GameObject explosionEffect; // Particle effect prefab for the explosion

    private bool hasExploded = false; // Check if the bomb has already exploded
    public Transform Player1;
    //public Transform Player2;

    private void Update()
    {
        // Start the explosion countdown
        float distancetoPlayer1 = Vector2.Distance(Player1.transform.position, transform.position);
       // float distancetoPlayer2 = Vector2.Distance(Player2.transform.position, transform.position);
        if (distancetoPlayer1 < playerTriggerDistance)
        {
            Invoke("Explode", explosionDelay);
        }

        //else if(distanceToPlayer2 < playerTriggerDistance) 
        //{
        //    Invoke("Explode", explosionDelay);
        //}
    }

    private void Explode()
    {
        Debug.Log("Explode");
        Destroy(Player1.gameObject);
        if (hasExploded) return; // Prevent multiple explosions

        hasExploded = true;

        // Show explosion effect
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Find all nearby objects within the explosion radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        
        foreach (Collider2D nearbyObject in colliders)
        {
            
            Rigidbody2D rb = nearbyObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 explosionDirection = rb.position - (Vector2)transform.position;
                rb.AddForce(explosionDirection.normalized * explosionForce);

            }
        }

        // Destroy the bomb object after the explosion
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the explosion radius in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerTriggerDistance);
    }
}
