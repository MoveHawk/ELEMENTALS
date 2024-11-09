using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    public float verticalSpeed = 10f;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float wallSlideSpeed = 2f;
    public float wallJumpForce = 7f;
    public float wallHopForce = 5f;
    public Vector2 wallHopDirection = new Vector2(1, 1);
    public Vector2 wallJumpDirection = new Vector2(1, 1);

    public GameObject coOpMechanicObject; // Reference to the GameObject containing CoOpMechanicUnlock
    private bool steamActiveforblue;
    public float variableJumpMultiplier = 0.5f;
    public float coyoteTime = 0.2f;

    private float coyoteTimeCounter;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    public bool isGrounded;
    private bool isWalled;
    private bool isWallSliding;

    private CoOpMechanicUnlock coOpMechanicUnlock;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Obtain reference to CoOpMechanicUnlock from the other GameObject
        if (coOpMechanicObject != null)
        {
            coOpMechanicUnlock = coOpMechanicObject.GetComponent<CoOpMechanicUnlock>();
        }

        if (coOpMechanicUnlock == null)
        {
            Debug.LogError("CoOpMechanicUnlock component not found on the specified GameObject.");
        }
    }

    private void Update()
    {
        // Dynamically update steamActiveforblue based on the mechanic state
        if (coOpMechanicUnlock != null)
        {
            steamActiveforblue = coOpMechanicUnlock.isMechanicActive;
        }

        HandleInput();
        Move();
        CheckWallSliding();
        CheckCoyoteTime();

        if (steamActiveforblue)
        {
            Debug.Log("CAN FLY");
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb.velocity = new Vector2(rb.velocity.x, verticalSpeed);
            }
        }
    }

    private void HandleInput()
    {
        moveInput.x = 0;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveInput.x = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveInput.x = 1;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && !steamActiveforblue)
        {
            Jump();
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) && rb.velocity.y > 0 && !steamActiveforblue)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpMultiplier);
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
            coyoteTimeCounter = 0f;
        }
        else if (isWallSliding || isWalled)
        {
            isWallSliding = false;
            WallJump();
        }
    }

    private void WallJump()
    {
        float jumpDirection = rb.velocity.x < 0 ? 1 : -1;
        if (moveInput.x == 0)
        {
            Vector2 wallHop = new Vector2(wallHopForce * wallHopDirection.x * jumpDirection, wallHopForce * wallHopDirection.y);
            rb.velocity = wallHop;
        }
        else
        {
            Vector2 wallJump = new Vector2(wallJumpForce * wallJumpDirection.x * jumpDirection, wallJumpForce * wallJumpDirection.y);
            rb.velocity = wallJump;
        }
    }

    private void CheckWallSliding()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, LayerMask.GetMask("Ground"));
        isWalled = Physics2D.Raycast(transform.position, Vector2.right, 0.6f, LayerMask.GetMask("Ground")) ||
                   Physics2D.Raycast(transform.position, Vector2.left, 0.6f, LayerMask.GetMask("Ground"));

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
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    public void Player2Death()
    {
        //destroy gameObject
        Debug.Log("Destroy player2");
        Destroy(this.gameObject, 2.0f);
    }
}
