using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public int currentHealth;  // Maintaining health as an int, as health is usually represented as an integer
    public TextMeshProUGUI healthText;  // TextMeshProUGUI for displaying health

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;  // slider.value is a float, but there's no issue with assigning an int to a float
        currentHealth = health;
        UpdateHealthText();
    }
    
    public void SetHealth(int health)
    {
        currentHealth = health;
        slider.value = health;  // Again, assigning int to float is fine
        UpdateHealthText();
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0);  // Subtract damage and ensure health doesn't go below 0
        SetHealth(currentHealth);
    }

    public void Heal(int healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, (int)slider.maxValue);  // Add healing but do not exceed max health
        SetHealth(currentHealth);
    }
    public void UpdateHealthText()
    {
        healthText.text = currentHealth.ToString();  // Update the health text to display the current health value
    }
}
