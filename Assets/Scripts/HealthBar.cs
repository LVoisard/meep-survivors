using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Slider healthbar;
    Health health;

    void Awake()
    {
        health = GetComponent<Health>();
        health.onHealthChanged.AddListener(UpdateBar);
    }

    void OnDestroy()
    {
        health.onHealthChanged.RemoveListener(UpdateBar);
    }
    void UpdateBar()
    {
        healthbar.value = health.GetHealthPercentage();
    }
}
