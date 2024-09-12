using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks1 : MonoBehaviour
{
    private Animator animator;
    public Transform player;
    public float movementSpeed = 10.0f;
    public float meleeAttackRange = 1.5f;
    public float rangedAttackRange = 5f;
    public float attackCooldown = 2f;
    public int meleeAttackDamage = 10;
    public int rangedAttackDamage = 5;
    public float meleeAttackDuration = 0.5f;
    public float rangedAttackDuration = 0.5f;

    private bool isPlayerInRange = false;
    private float attackTimer = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null)
            return;

        // Check distance between enemy and player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Check if player is within attack range
        isPlayerInRange = distanceToPlayer <= meleeAttackRange || distanceToPlayer <= rangedAttackRange;

        if (isPlayerInRange && attackTimer <= 0)
        {
            if (distanceToPlayer <= meleeAttackRange)
            {
                StartCoroutine(MeleeAttack());
                attackTimer = attackCooldown;
            }
            else if (distanceToPlayer <= rangedAttackRange)
            {
                StartCoroutine(RangedAttack());
                attackTimer = attackCooldown;
            }
        }
        else
        {
            // Move towards the player if not in range
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            transform.position += directionToPlayer * movementSpeed * Time.deltaTime;
            animator.SetTrigger("Run");
        }

        // Update attack cooldown timer
        attackTimer -= Time.deltaTime;
    }

    IEnumerator MeleeAttack()
    {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(meleeAttackDuration);

        // Perform the melee attack
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, meleeAttackRange);
        foreach (Collider2D collider in hitColliders)
        {
            PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(meleeAttackDamage);
            }
        }
    }

    IEnumerator RangedAttack()
    {
        animator.SetTrigger("RangedAttack");
        yield return new WaitForSeconds(rangedAttackDuration);

        // Perform the ranged attack
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, rangedAttackRange);
        foreach (Collider2D collider in hitColliders)
        {
            PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(rangedAttackDamage);
            }
        }
    }

    // Visualize the attack ranges in the scene view
    void OnDrawGizmosSelected()
    {
        // Draw melee attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeAttackRange);

        // Draw ranged attack range
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rangedAttackRange);
    }
}
