using UnityEngine;
using UnityEngine.InputSystem;

public class Player1Controllerjof : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float wallSlideSpeed = 2f;
    public float wallHopForce = 8f;
    public float wallJumpForce = 12f;
    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;

    private Rigidbody2D rb;
    private bool isFacingRight = true;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isWallSliding;
    private bool isWallJumping;
    private float moveInput;
    public float wallJumpCooldown;

    public Transform groundCheck;
    public Transform wallCheck;
    public float groundCheckRadius;
    public float wallCheckRadius; // radius of the circle to detect walls
    public LayerMask whatIsGround;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();
    }

    void Update()
    {
        CheckInput();
        CheckSurroundings();
        CheckIfWallSliding();
        CheckMovementDirection();
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    private void CheckInput()
    {
        if (Keyboard.current.aKey.isPressed)
            moveInput = -1;
        else if (Keyboard.current.dKey.isPressed)
            moveInput = 1;
        else
            moveInput = 0;

        if (Keyboard.current.wKey.wasPressedThisFrame && (isGrounded || isWallSliding))
        {
            Jump();
        }
    }

    private void CheckSurroundings()
    {
        // Check if grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        // Update the wall check position dynamically
        Vector3 wallCheckPos = wallCheck.position;
        if (!isFacingRight)
        {
            wallCheckPos.x = -Mathf.Abs(wallCheck.position.x); // Flip the wall check to the left side
        }
        else
        {
            wallCheckPos.x = Mathf.Abs(wallCheck.position.x); // Keep wall check on the right side
        }

        // Using Physics2D.OverlapCircle for wall detection
        isTouchingWall = Physics2D.OverlapCircle(wallCheckPos, wallCheckRadius, whatIsGround);

        // Reset wall jump state after cooldown
        if (wallJumpCooldown > 0)
        {
            wallJumpCooldown -= Time.deltaTime;
        }
        else
        {
            isWallJumping = false;
        }
    }

    private void CheckIfWallSliding()
    {
        // Wall sliding occurs when the player is touching a wall and falling down
        if (isTouchingWall && !isGrounded && rb.velocity.y < 0 && !isWallJumping)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed); // restricts the falling speed during wall slide
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void CheckMovementDirection()
    {
        // Flip player when changing direction
        if ((isFacingRight && moveInput < 0) || (!isFacingRight && moveInput > 0))
        {
            Flip();
        }
    }

    private void ApplyMovement()
    {
        if (!isWallJumping)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y); // Apply horizontal movement
        }
    }

    private void Jump()
    {
        if (isWallSliding) // Wall jump
        {
            isWallSliding = false;
            isWallJumping = true;
            wallJumpCooldown = 0.2f; // Small cooldown to prevent re-entering wall slide right away

            // Determine direction for wall jump force
            Vector2 wallJumpForceDir = new Vector2(wallJumpForce * wallJumpDirection.x * moveInput, wallJumpForce * wallJumpDirection.y);
            rb.AddForce(wallJumpForceDir, ForceMode2D.Impulse);
        }
        else if (isGrounded) // Regular jump
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f); // Flip the player sprite

        // After flipping, update the wallCheck position dynamically based on the facing direction
        Vector3 wallCheckLocalPosition = wallCheck.localPosition;
        wallCheckLocalPosition.x = -wallCheckLocalPosition.x; // Flip wallCheck position
        wallCheck.localPosition = wallCheckLocalPosition;
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        if (wallCheck != null)
            Gizmos.DrawWireSphere(wallCheck.position, wallCheckRadius); // Visualize wall check area
    }
}
