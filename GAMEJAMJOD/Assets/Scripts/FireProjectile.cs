using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    //Projectile prefab
    public GameObject fireprojectilePrefab;
    //shooting point
    public Transform shootPoint;
    //projectile speed
    public float fireprojectileSpeed = 10f;

    //for flip logic
    private bool isFacingRight = true;

    //Player2controller reference
    public Player1hu pc1;

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.LeftShift)&& pc1.isGrounded==true)
        {
            Shoot(false);
        }

        else if(Input.GetKeyDown(KeyCode.LeftShift) && pc1.isGrounded == false)
        {
            Shoot(true);
        }

        // Flip logic
        if (Input.GetKeyDown(KeyCode.A)) // Left key
        {
            Flip(false);
        }
        else if (Input.GetKeyDown(KeyCode.D)) // Right key
        {
            Flip(true);
        }
    }

    private void Shoot(bool shootupward)
    {

        GameObject projectile = Instantiate(fireprojectilePrefab, shootPoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if(shootupward)
        {
            rb.velocity = new Vector2(0, fireprojectileSpeed);
        }

        else
        {
            if (isFacingRight)
            {
                rb.velocity = new Vector2(fireprojectileSpeed, 0);
            }
            else
            {
                rb.velocity = new Vector2(-fireprojectileSpeed, 0);

                //flipping the projectile when player is facing left
                Vector3 projectileScale = projectile.transform.localScale;
                projectileScale.x = -Mathf.Abs(projectileScale.x);
                projectile.transform.localScale = projectileScale;
            }
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
