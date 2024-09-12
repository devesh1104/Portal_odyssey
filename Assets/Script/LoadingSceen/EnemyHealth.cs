using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    private int currentHealth;
    private Animator anim;
    public GameObject objectToDisappear;
    public HealthBar healthBar;

    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = health;
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(health);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (anim != null)
        {
            anim.SetTrigger("dead");
        }
        Debug.Log("Enemy is dead");

        if (objectToDisappear != null)
        {
            objectToDisappear.SetActive(false);
        }

        StartCoroutine(DieCoroutine(2.0f));
    }

    IEnumerator DieCoroutine(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        gameObject.SetActive(false);
    }
}
