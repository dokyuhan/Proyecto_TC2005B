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
}
