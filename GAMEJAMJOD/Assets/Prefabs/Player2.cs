using UnityEngine;

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

   public ParticleSystem dust;

    public bool hasCollectedDiamond = false;
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
        // Horizontal movement with A and D keys
        float move = 0;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            move = -horizontalSpeed;
            //dust.Play();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            move = horizontalSpeed;
            //dust.Play();
        }

        // Only add vertical velocity if both Space and LeftShift are held and cooldown is not active
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.E) && canUseVerticalVelocity)
        {
            // Start upward velocity when conditions are met
            rb.velocity = new Vector2(move, verticalSpeed);

            // Reduce vertical velocity time
            verticalVelocityTimeLeft -= Time.deltaTime;

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
}
