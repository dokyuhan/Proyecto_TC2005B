using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergyBar : MonoBehaviour
{
    public Slider slider;
    public int currentEnergy;
    public TextMeshProUGUI energyText;

    public void SetMaxEnergy(int energy)
    {
        slider.maxValue = energy;
        slider.value = Mathf.Min(currentEnergy, energy);
        UpdateEnergyText();
    }

    public void SetEnergy(int energy)
    {
        currentEnergy = energy;
        slider.value = energy;  // Set the slider to the new energy level
        UpdateEnergyText();
    }

    public void IncrementEnergy(int amount)
    {
        if (currentEnergy + amount > (int)slider.maxValue)
        {
            Debug.LogWarning("Energy is already at maximum and cannot be increased further.");
            return;
        }
        currentEnergy = Mathf.Min(currentEnergy + amount, (int)slider.maxValue);
        SetEnergy(currentEnergy);
    }

    public void ResetEnergy()
    {
        SetEnergy(0);  // Reset the energy to 0
    }

    public void DecrementEnergy(int amount)
    {
        if (currentEnergy - amount < 0)
        {
            Debug.LogWarning("Insufficient energy to perform this action.");
            return;
        }
        currentEnergy = Mathf.Max(currentEnergy - amount, 0);
        SetEnergy(currentEnergy);
    }

    public void UpdateEnergyText()
    {
        energyText.text = currentEnergy.ToString();  // Update the energy text to display the current energy value
    }
}
