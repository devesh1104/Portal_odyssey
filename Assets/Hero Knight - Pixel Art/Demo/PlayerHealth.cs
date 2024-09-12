using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public HealthBar healthBar;
	private Animator anim;
    public GameObject deathEffect;

    void Start()
    {
		anim = GetComponent<Animator>();
        healthBar.SetMaxHealth(health);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);

        StartCoroutine(DamageAnimation());

        if (health <= 0)
        {
			anim.SetTrigger("Death");
            StartCoroutine(DieWithDelay());
        }
    }

    IEnumerator DieWithDelay()
    {
        yield return new WaitForSeconds(1.05f); // Adjust the delay as needed
        Die();
    }

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator DamageAnimation()
    {
        SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < 3; i++)
        {
            foreach (SpriteRenderer sr in srs)
            {
                Color c = sr.color;
                c.a = 0;
                sr.color = c;
            }

            yield return new WaitForSeconds(0.1f);

            foreach (SpriteRenderer sr in srs)
            {
                Color c = sr.color;
                c.a = 1;
                sr.color = c;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
