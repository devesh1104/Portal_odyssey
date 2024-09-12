using System.Collections;
using UnityEngine;

public class Stopper : MonoBehaviour
{
    public float health;
    private float currentHealth;
    private Animator anim;
    public GameObject objectToDisappear; // Reference to the object you want to disappear

    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = health;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            anim.SetTrigger("dead");
            Debug.Log("Enemy is dead");
            StartCoroutine(DieCoroutine(2.0f)); // Adjust delayInSeconds as needed
        }
    }

    private IEnumerator DieCoroutine(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        
        // Check if the object to disappear is not null
        if(objectToDisappear != null)
        {
            // Disable the GameObject
            objectToDisappear.SetActive(false);
        }
    }
}
