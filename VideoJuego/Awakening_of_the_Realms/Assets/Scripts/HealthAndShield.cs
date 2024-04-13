using UnityEngine;
using UnityEngine.UI;

public class HealthAndShield : MonoBehaviour
{
    public Image healthBar;
    public Image shieldBar;

    private float maxHealth = 100f;
    private float currentHealth;
    private float maxShield = 50f;
    private float currentShield;

    void Start()
    {
        currentHealth = maxHealth;
        currentShield = maxShield;
        UpdateHealthUI();
        UpdateShieldUI();
    }

    void UpdateHealthUI()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    void UpdateShieldUI()
    {
        shieldBar.fillAmount = currentShield / maxShield;
    }

    public void TakeDamage(float damage)
    {
        if (currentShield > 0)
        {
            float remainingDamage = damage - currentShield;
            currentShield = Mathf.Max(currentShield - damage, 0);
            UpdateShieldUI();

            if (remainingDamage > 0)
            {
                currentHealth = Mathf.Max(currentHealth - remainingDamage, 0);
                UpdateHealthUI();
            }
        }
        else
        {
            currentHealth = Mathf.Max(currentHealth - damage, 0);
            UpdateHealthUI();
        }
    }

    public void Heal(float health)
    {
        currentHealth = Mathf.Min(currentHealth + health, maxHealth);
        UpdateHealthUI();
    }

    public void AddShield(float shield)
    {
        currentShield = Mathf.Min(currentShield + shield, maxShield);
        UpdateShieldUI();
    }
}
