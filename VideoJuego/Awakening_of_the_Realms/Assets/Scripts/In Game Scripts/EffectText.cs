using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EffectText : MonoBehaviour
{
    public TextMeshProUGUI effectText;
    public float displayTime;

    // Use a dictionary to map effect types to their descriptions for easier access
    public Dictionary<string, string> effectsDescriptions = new Dictionary<string, string>();

    // Start is called before the first frame update
    void Start()
    {
        effectText.text = "";

        // Initialize your effects descriptions here
        effectsDescriptions["Princess Blessing"] = "Enhances healing for 25% for 1 turn and reduces the opponent's energy by 2 immediately.";
        effectsDescriptions["King Arthur wrath"] = "Ignores the opponent's defense for 1 turn.";
        effectsDescriptions["Skyfall"] = "Dodge Enemy's attack for 1 turn.";
        effectsDescriptions["Hell Fire"] = "Inflicts dot damage of 10 for 3 rounds and reduces healing by 50% for 3 rounds.";
        effectsDescriptions["Fortress"] = "Creates a barrier that adds 100 defense for 2 rounds.";
        effectsDescriptions["Shadow Strike"] = "Weakens opponent's attacks by 20% for 2 rounds and life steal 30 health from the enemy.";
        effectsDescriptions["Mighty Soldier"] = "Reflects all damage taken this turn and heals 10 life points for 3 rounds.";
        effectsDescriptions["Demon Curse"] = "Doubles the damage for 1 round, applies a curse causing 10 damage over 2 rounds, and reduces healing by 20% for 2 rounds.";
        // Add more effects as needed
    }

    // This method accepts an EffectType object and displays its details
    public void ShowEffect(string effectType)
    {
        StartCoroutine(DisplayEffect(effectType));
    }

    // Coroutine to display the effect text and clear it after a delay
    IEnumerator DisplayEffect(string effectType)
    {
        if (effectsDescriptions.TryGetValue(effectType, out string description))
        {
            effectText.text = $"Effect: {effectType} - {description}";
        }
        else
        {
            effectText.text = $"Effect: {effectType} - Description not available.";
        }

        yield return new WaitForSeconds(displayTime);
        effectText.text = ""; // Clear text after displaying
    }
}
