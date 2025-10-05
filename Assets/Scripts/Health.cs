using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{

    [SerializeField] private float maxHealth;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private float currentHealth;

    public UnityEvent onDied = new UnityEvent();

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void Heal(float amt)
    {
        currentHealth = Mathf.Min(currentHealth + amt, maxHealth);
    }

    public bool TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        spriteRenderer.color = Color.softRed;
        if (currentHealth <= 0.0f)
        {
            currentHealth = 0;
            Die();
            return true;
        }

        Helper.Wait(0.05f, () => spriteRenderer.color = Color.white);

        return false;
    }

    private void Die()
    {
        onDied?.Invoke();
        Destroy(gameObject);
    }
}