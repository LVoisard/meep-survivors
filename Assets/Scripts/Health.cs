using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private float maxHealth;
    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void Heal(float amt)
    {
        currentHealth = Mathf.Min(currentHealth+amt, maxHealth);
    }

    public bool TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0.0f)
        {
            currentHealth = 0;
            Die();
            return true;
        }

        return false;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}