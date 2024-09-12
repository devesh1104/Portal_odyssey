using UnityEngine;

public class hi : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;
    public Rigidbody2D rb;

    private void Start()
    {
        if (!animator)
        {
            Debug.LogError("Animator component is not assigned!");
            enabled = false; // Disable the script to prevent further errors
            return;
        }

        if (!rb)
        {
            rb = GetComponent<Rigidbody2D>();
            if (!rb)
            {
                Debug.LogError("Rigidbody2D component is missing!");
                enabled = false; // Disable the script to prevent further errors
                return;
            }
        }
    }

    private void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        Move(moveInput);
        FlipSprite(moveInput);
        UpdateAnimator(moveInput);
    }

    private void Move(float moveInput)
    {
        // Set the velocity of the Rigidbody2D based on the input
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    private void FlipSprite(float moveInput)
    {
        // Flip the player sprite if moving left
        if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        // Flip the player sprite if moving right
        else if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void UpdateAnimator(float moveInput)
    {
        // Update animator parameters based on movement input
        animator.SetBool("Run", Mathf.Abs(moveInput) > 0);
    }
}
