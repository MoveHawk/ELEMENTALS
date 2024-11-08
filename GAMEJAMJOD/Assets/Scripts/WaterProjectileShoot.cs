using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterProjectileShoot : MonoBehaviour
{
    //Projectile prefab
    public GameObject projectilePrefab;
    //shooting point
    public Transform shootPoint;
    //projectile speed
    public float projectileSpeed = 10f;

    //for flip logic
    private bool isFacingRight = true; 

    void Update()
    {
        // Check for shoot input (e.g., "Fire1" is often left mouse button or Ctrl key)
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            Shoot();
        }

        // Flip logic
        if (Input.GetKeyDown(KeyCode.LeftArrow)) // Left key
        {
            Flip(false);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) // Right key
        {
            Flip(true);
        }
    }

    private void Shoot()
    {
        
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (isFacingRight)
        {
            rb.velocity = new Vector2(projectileSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-projectileSpeed, 0);

            //flipping the projectile when player is facing left
            Vector3 projectileScale = projectile.transform.localScale;
            projectileScale.x = -Mathf.Abs(projectileScale.x);
            projectile.transform.localScale = projectileScale;
        }
    }

    private void Flip(bool facingRight)
    {
        isFacingRight = facingRight;
        Vector3 scale = transform.localScale;
        scale.x = facingRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
}
