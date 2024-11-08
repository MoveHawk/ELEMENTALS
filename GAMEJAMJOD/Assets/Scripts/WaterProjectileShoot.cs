
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
    //playercontroller2 reference
    public Player2Controller pc2;

    void Update()
    {
        // Check for shoot input (e.g., "Fire1" is often left mouse button or Ctrl key)
        if (Input.GetKeyDown(KeyCode.RightShift) && pc2.isGrounded == true)
        {
            Shoot(false);
        }
        if (pc2.isGrounded == false && Input.GetKeyDown(KeyCode.RightShift))
        {
            Shoot(true);
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

    private void Shoot(bool shootupward)
    {

        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (shootupward == true)
        {
            //Debug.Log("Grounded ==false & projectile upward");
            rb.velocity = new Vector2(0, projectileSpeed);
            projectile.transform.rotation = Quaternion.Euler(0, 0, 90);
        }


        else
        {
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


    }

    private void Flip(bool facingRight)
    {
        isFacingRight = facingRight;
        Vector3 scale = transform.localScale;
        scale.x = facingRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

   
}

