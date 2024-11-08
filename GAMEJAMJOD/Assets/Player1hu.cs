using UnityEngine;

public class Player1hu : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float wallSlideSpeed = 2f;
    public float wallJumpForce = 7f;
    public float wallHopForce = 5f;
    public Vector2 wallHopDirection = new Vector2(1, 1);
    public Vector2 wallJumpDirection = new Vector2(1, 1);

    // Variable jump
    public float variableJumpMultiplier = 0.5f;

    // Coyote time
    public float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    public bool isGrounded;
    private bool isWalled;
    private bool isWallSliding;
    private bool canJump;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();
    }

    private void Update()
    {
        HandleInput();
        Move();
        CheckWallSliding();
        CheckCoyoteTime();
    }

    private void HandleInput()
    {
        // Horizontal movement (A/D or Arrow keys)
        moveInput.x = 0;

        if (Input.GetKey(KeyCode.A))
        {
            moveInput.x = -1; // Move left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveInput.x = 1; // Move right
        }

        // Jump (W or Up Arrow)
        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
        if (Input.GetKeyUp(KeyCode.W) && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpMultiplier); // Apply variable jump when releasing
        }
    }

    private void Move()
    {
        if (!isWallSliding)
        {
            rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        }
    }

    private void Jump()
    {
        if (isGrounded || coyoteTimeCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            coyoteTimeCounter = 0f; // Reset coyote time after jumping
        }
        else if (isWallSliding || isWalled)
        {
            isWallSliding = false;
            WallJump();
        }
    }

    private void WallJump()
    {
        float jumpDirection = rb.velocity.x < 0 ? 1 : -1; // Automatically flip direction
        if (moveInput.x == 0)  // Wall hop
        {
            Vector2 wallHop = new Vector2(wallHopForce * wallHopDirection.x * jumpDirection, wallHopForce * wallHopDirection.y);
            rb.velocity = wallHop;
        }
        else  // Wall jump
        {
            Vector2 wallJump = new Vector2(wallJumpForce * wallJumpDirection.x * jumpDirection, wallJumpForce * wallJumpDirection.y);
            rb.velocity = wallJump;
        }
    }

    private void CheckWallSliding()
    {
        // Check if player is grounded
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, LayerMask.GetMask("Ground"));

        // Check if player is touching a wall on either side (left or right)
        isWalled = Physics2D.Raycast(transform.position, Vector2.right, 0.6f, LayerMask.GetMask("Ground")) ||
                   Physics2D.Raycast(transform.position, Vector2.left, 0.6f, LayerMask.GetMask("Ground"));

        // If player is touching a wall and falling, start wall sliding
        if (isWalled && !isGrounded && rb.velocity.y < 0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void CheckCoyoteTime()
    {
        // Update coyote time counter
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime; // Reset coyote time when grounded
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime; // Count down when not grounded
        }
    }
}
