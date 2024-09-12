using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    // Animator component for controlling animations
    private Animator animator;
    public float PlayerHealth=100.0f;
    public float currenthealth=0.0f;
    public float movementSpeed=10.0f;
    private int damage;



    // Health of the boss
    public int health = 200;
    public LayerMask PlayerLayers;
    public Vector3 attackpoint1;

    // Damage values for each attack
    public int meleeAttack1Damage = 10;
    public int meleeAttack2Damage = 15;
    public int magicSphereDamage = 30;
    public int magicArrowDamage = 5;

    // Cooldowns for each attack
    public float meleeAttack1Cooldown = 1f;
    public float meleeAttack2Cooldown = 2f;
    public float magicSphereCooldown = 5f;
    public float magicArrowCooldown = 1.5f;

    // Timer variables to track attack cooldowns
    private float meleeAttack1Timer = 0f;
    private float meleeAttack2Timer = 0f;
    private float magicSphereTimer = 0f;
    private float magicArrowTimer = 0f;

    // Reference to player object (assuming there's a player to attack)
    private GameObject player;

    // Range within which boss attacks player
    public float attackRange = 3f;

    void Start()
    {
        // Get the Animator component attached to the boss object
        animator = GetComponent<Animator>();
        currenthealth=PlayerHealth;

        // Find the player object by tag
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // Update attack cooldown timers
        meleeAttack1Timer -= Time.deltaTime;
        meleeAttack2Timer -= Time.deltaTime;
        magicSphereTimer -= Time.deltaTime;
        magicArrowTimer -= Time.deltaTime;

        // Check distance between boss and player
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        // Determine direction of the player relative to the boss
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

        // // Face towards the player's direction
        if (directionToPlayer.x < 0)
        {
            // Player is to the right
            transform.localScale = new Vector3(1, 1, 1); // Face right
        }
        else
        {
            // Player is to the left
            transform.localScale = new Vector3(-1, 1, 1); // Face left
        }
        if (distanceToPlayer <= attackRange)
        {
            // Stop moving towards the player
            // Add attack logic here
            // Check which attack to perform based on cooldowns
            if (meleeAttack1Timer <= 0)
            {
                MeleeAttack1();
                // MeleeAttack1();
                // MeleeAttack1();
            }
            else if (magicArrowTimer <= 0)
            {
                MagicArrowAttack();
                // MagicArrowAttack();
            }
            else if (meleeAttack2Timer <= 0)
            {
                MeleeAttack2();
                // MeleeAttack2();
            }
            else if (magicSphereTimer <= 0)
            {
                MagicSphereAttack();
            }
        }
        else
        {
            // Move towards the player
            transform.position += directionToPlayer * movementSpeed * Time.deltaTime;
            animator.SetTrigger("Run");
        }

        // If player is within attack range, trigger attack
    
    }

    void MeleeAttack1()
    {
        // Play melee attack 1 animation
        animator.SetTrigger("MeleeAttack1");
        damage=meleeAttack1Damage;

        // Deal damage to the player
        // currentHealth.TakeDamage(meleeAttack1Damage);
        attack();
        

        // Reset the cooldown timer
        meleeAttack1Timer = meleeAttack1Cooldown;
    }

    void MeleeAttack2()
    {
        // Play melee attack 2 animation
        animator.SetTrigger("MeleeAttack2");
        damage=meleeAttack2Damage;
        // Deal damage to the player
        //currentHealth.TakeDamage(meleeAttack2Damage);
        attack();

        // Reset the cooldown timer
        meleeAttack2Timer = meleeAttack2Cooldown;
    }

    void MagicSphereAttack()
    {
        // Play magic sphere attack animation
        animator.SetTrigger("MagicSphereAttack");
        damage=magicSphereDamage ;
        // Deal damage to the player
        //currentHealth.TakeDamage(magicSphereDamage);
        attack();

        // Reset the cooldown timer
        magicSphereTimer = magicSphereCooldown;
    }

    void MagicArrowAttack()
    {
        // Play magic arrow attack animation
        animator.SetTrigger("MagicArrowAttack");
        damage=magicSphereDamage;
        // Deal damage to the player
        //currentHealth.TakeDamage(magicArrowDamage);
        attack();

        // Reset the cooldown timer
        magicArrowTimer = magicArrowCooldown;
    }
    void attack()
    {
        Vector3 pos = transform.position;
		pos += transform.right * attackpoint1.x;
		pos += transform.up * attackpoint1.y;

    //    Collider2D[] players = Physics2D.OverlapCircleAll(attackpoint1.transform.position, attackRange, PlayerLayers);
    //    foreach (Collider2D playerCollider in players)
    //    {
        Collider2D colInfo = Physics2D.OverlapCircle(attackpoint1, attackRange, PlayerLayers);
		if (colInfo != null)
		{
			colInfo.GetComponent<PlayerHealth>().TakeDamage(damage);
		}
    }
    
}