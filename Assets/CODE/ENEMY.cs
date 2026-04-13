using UnityEngine;

public class ENEMY : MonoBehaviour
{
    public float maxHealth = 150f;
    private float currentHealth;

    public Animator animator;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;
        animator.SetTrigger("Die");
        Destroy(gameObject, 3f); 
    }
}