using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private float HorizontalInput;
    public Rigidbody2D rb;
    public bool Grounded;
    public float jumpforce = 5.0f;
    public LayerMask JumpableGround;
  

    // Update is called once per frame
    void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(HorizontalInput * 7, rb.velocity.y);

        Jump();
    }

    void Jump()
    {
      
        if (Input.GetKeyDown(KeyCode.Space))
        {
           
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);

        }
    }

    
}
