using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Slider slider;
    public int currentEnergy;

    // Set the maximum energy without affecting the current energy value displayed
    public void SetMaxEnergy(int energy)
    {
        slider.maxValue = energy;
        // Only set slider.value to the lower of currentEnergy or energy to respect the new max
        slider.value = Mathf.Min(currentEnergy, energy);
    }
    
    public void SetEnergy(int energy)
    {
        currentEnergy = energy;
        slider.value = energy;  // Set the slider to the new energy level
    }

    public void IncrementEnergy(int amount)
    {
        // Ensure current energy does not exceed the maximum when incremented
        currentEnergy = Mathf.Min(currentEnergy + amount, (int)slider.maxValue);
        SetEnergy(currentEnergy);
    }

    public void ResetEnergy()
    {
        currentEnergy = 0;
        SetEnergy(currentEnergy);  // Reset the energy to 0
    }

    public void DecrementEnergy(int amount)
    {
        // Check if subtracting the amount would drop the energy below zero.
        if (currentEnergy - amount < 0)
        {
            Debug.LogWarning("You can't play the legendary card because there's not enough energy.");
            return;
        }

        // Subtract the amount from currentEnergy and ensure it doesn't go below zero.
        currentEnergy -= amount;  // This ensures that the energy is decreased by 'amount'.
        currentEnergy = Mathf.Max(currentEnergy, 0);  // This line is redundant if checks are in place, but safe to keep.

        SetEnergy(currentEnergy);  // Update the energy display or other related elements.
    }

}
