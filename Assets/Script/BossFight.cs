using UnityEngine;

public class BossFight : MonoBehaviour
{
    // Animator component for controlling animations
    private Animator animator;

    // Health of the boss
    public int health = 100;

    // Damage values for each attack
    public int meleeAttack1Damage = 10;
    public int meleeAttack2Damage = 15;
    public int magicSphereDamage = 30;
    public int magicArrowDamage = 5;

    // Cooldowns for each attack
    public float meleeAttack1Cooldown = 1f;
    public float meleeAttack2Cooldown = 1.5f;
    public float magicSphereCooldown = 5f;
    public float magicArrowCooldown = 2f;

    // Timer variables to track attack cooldowns
    private float meleeAttack1Timer = 0f;
    private float meleeAttack2Timer = 0f;
    private float magicSphereTimer = 0f;
    private float magicArrowTimer = 0f;

    // Reference to player object (assuming there's a player to attack)
    private GameObject player;

    // Range within which boss attacks player
    public float attackRange = 3f;

    // Speed at which the boss moves towards the player
    public float movementSpeed = 2f;

    // Boolean to track if the boss is alive
    private bool isAlive = true;

    void Start()
    {
        // Get the Animator component attached to the boss object
        animator = GetComponent<Animator>();

        // Find the player object by tag
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // If the boss is not alive, do nothing
        if (!isAlive)
            return;

        // Update attack cooldown timers
        meleeAttack1Timer -= Time.deltaTime;
        meleeAttack2Timer -= Time.deltaTime;
        magicSphereTimer -= Time.deltaTime;
        magicArrowTimer -= Time.deltaTime;

        // Check distance between boss and player
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        // Determine direction of the player relative to the boss
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

        // Face towards the player's direction
        if (directionToPlayer.x > 0)
        {
            // Player is to the right
            transform.localScale = new Vector3(1, 1, 1); // Face right
        }
        else
        {
            // Player is to the left
            transform.localScale = new Vector3(-1, 1, 1); // Face left
        }

        // If player is within attack range, trigger attack
        if (distanceToPlayer <= attackRange)
        {
            // Check which attack to perform based on cooldowns
            if (meleeAttack1Timer <= 0)
            {
                MeleeAttack1();
            }
            else if (meleeAttack2Timer <= 0)
            {
                MeleeAttack2();
            }
            else if (magicSphereTimer <= 0)
            {
                MagicSphereAttack();
            }
            else if (magicArrowTimer <= 0)
            {
                MagicArrowAttack();
            }
            else if(health <= 0)
            {
                animator.SetTrigger("Die");
            }
        }
        else
        {
            // Move towards the player if not within attack range
            transform.position += directionToPlayer * movementSpeed * Time.deltaTime;
        }
    }

    void MeleeAttack1()
    {
        // Play melee attack 1 animation
        animator.SetTrigger("MeleeAttack1");

        // Deal damage to the player
        player.GetComponent<PlayerHealth>().TakeDamage(meleeAttack1Damage);

        // Reset the cooldown timer
        meleeAttack1Timer = meleeAttack1Cooldown;
    }

    void MeleeAttack2()
    {
        // Play melee attack 2 animation
        animator.SetTrigger("MeleeAttack2");

        // Deal damage to the player
        player.GetComponent<PlayerHealth>().TakeDamage(meleeAttack2Damage);

        // Reset the cooldown timer
        meleeAttack2Timer = meleeAttack2Cooldown;
    }

    void MagicSphereAttack()
    {
        // Play magic sphere attack animation
        animator.SetTrigger("MagicSphereAttack");

        // Deal damage to the player
        player.GetComponent<PlayerHealth>().TakeDamage(magicSphereDamage);

        // Reset the cooldown timer
        magicSphereTimer = magicSphereCooldown;
    }

    void MagicArrowAttack()
    {
        // Play magic arrow attack animation
        animator.SetTrigger("MagicArrowAttack");

        // Deal damage to the player
        player.GetComponent<PlayerHealth>().TakeDamage(magicArrowDamage);

        // Reset the cooldown timer
        magicArrowTimer = magicArrowCooldown;
    }

    // Method to handle boss damage
    public void TakeDamage(int damage)
    {
        // Reduce boss health
        health -= damage;

        // Check if the boss is dead
        if (health <= 0)
        {
            animator.SetTrigger("Die");
        }
    }

    // Method to handle boss death
   
}