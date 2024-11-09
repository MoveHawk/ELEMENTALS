using UnityEngine;
using UnityEngine.SceneManagement;
public class Player2 : MonoBehaviour
{
    public float horizontalSpeed = 5f;
    public float verticalSpeed = 10f;
    public float verticalVelocityDuration = 2f;  // Duration of upward movement
    public float cooldownDuration = 3f;          // Cooldown after using upward movement

    private Rigidbody2D rb;
    private bool canUseVerticalVelocity = true;
    private float verticalVelocityTimeLeft;
    private float cooldownTimeLeft;
    private bool isFacingRight = true;

    public ParticleSystem dust;
    public bool hasCollectedDiamond = false;

    // Animator reference
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        verticalVelocityTimeLeft = verticalVelocityDuration;

        // Get the Animator component
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleMovement();
        HandleCooldown();
    }

    private void HandleMovement()
    {
        // Horizontal movement with LeftArrow and RightArrow keys
        float move = 0;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            move = -horizontalSpeed;
            Flip(false); // Flip to face left
            //dust.Play();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            move = horizontalSpeed;
            Flip(true); // Flip to face right
            //dust.Play();
        }

        // Only add vertical velocity if both UpArrow and E are held and cooldown is not active
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.E) && canUseVerticalVelocity)
        {
            // Start upward velocity when conditions are met
            rb.velocity = new Vector2(move, verticalSpeed);

            // Reduce vertical velocity time
            verticalVelocityTimeLeft -= Time.deltaTime;

            // Play flying animation
            if (animator != null)
            {
                animator.SetTrigger("takeof");  // Play flying animation
            }

            if (verticalVelocityTimeLeft <= 0)
            {
                canUseVerticalVelocity = false;
                cooldownTimeLeft = cooldownDuration;
            }
        }
        else
        {
            // Default to horizontal movement only if vertical velocity is not active
            rb.velocity = new Vector2(move, rb.velocity.y);

            // Stop flying animation when vertical velocity is not active
            if (animator != null && !canUseVerticalVelocity)
            {
                animator.SetBool("IsFlying", false);  // Stop flying animation
            }
        }
    }

    private void HandleCooldown()
    {
        // Handle cooldown for vertical movement
        if (!canUseVerticalVelocity)
        {
            cooldownTimeLeft -= Time.deltaTime;
            if (cooldownTimeLeft <= 0)
            {
                canUseVerticalVelocity = true;
                verticalVelocityTimeLeft = verticalVelocityDuration;
            }
        }
    }

    private void Flip(bool facingRight)
    {
        // Flip the player's local scale based on direction
        if (facingRight != isFacingRight)
        {
            isFacingRight = facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Diamond"))
        {
            hasCollectedDiamond = true;
            Destroy(other.gameObject);
            // Notify LevelManager that Player 2 has collected their diamond
            LevelManager.Instance.OnPlayerCollectedDiamond(2);
        }
    }

    public void Player2Death()
    {
        //destroy gameObject
        SceneManager.LoadScene(3);
    }
}
