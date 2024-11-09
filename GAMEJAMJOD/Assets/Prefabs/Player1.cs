using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalSpeed = 5f;
    public float verticalSpeed = 10f;
    public float verticalVelocityDuration = 2f;
    public float cooldownDuration = 3f;

    private Rigidbody2D rb;
    private bool canUseVerticalVelocity = true;
    private float verticalVelocityTimeLeft;
    private float cooldownTimeLeft;

    public bool hasCollectedDiamond = false;
    public Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        verticalVelocityTimeLeft = verticalVelocityDuration;
    }

    private void Update()
    {
        HandleMovement();
        HandleCooldown();
    }

    private void HandleMovement()
    {
        float move = 0;
        if (Input.GetKey(KeyCode.A))
        {
            move = -horizontalSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            move = horizontalSpeed;
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.RightShift) && canUseVerticalVelocity)
        {
            rb.velocity = new Vector2(move, verticalSpeed);

            verticalVelocityTimeLeft -= Time.deltaTime;

            if (verticalVelocityTimeLeft <= 0)
            {
                canUseVerticalVelocity = false;
                cooldownTimeLeft = cooldownDuration;
            }

            if (animator != null)
            {
                animator.SetTrigger("takeof");
            }
        }
        else
        {
            rb.velocity = new Vector2(move, rb.velocity.y);
        }
    }

    private void HandleCooldown()
    {
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Diamond"))
        {
            hasCollectedDiamond = true;
            Destroy(other.gameObject);
            LevelManager.Instance.OnPlayerCollectedDiamond(1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            // Set the player as a child of the moving platform
            transform.SetParent(collision.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            // Detach the player from the moving platform
            transform.SetParent(null);
        }
    }

    public void Player1Death()
    {
        Debug.Log("Destroy player1");
        Destroy(this.gameObject, 0.5f);
    }
}
