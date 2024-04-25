using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;


public class EffectsDebug : MonoBehaviour
{
    public TextMeshProUGUI playerEffectsText;
    public TextMeshProUGUI aiEffectsText;
    private Game game;

    void Start()
    {
        game = Game.Instance;  // Ensure Game is a singleton and accessible
    }

    void Update()
    {
        if (game != null)
        {
            UpdateEffectsDisplay();
        }
    }

    void UpdateEffectsDisplay()
    {
        if (game.playerEffects != null && game.aiEffects != null)
        {
            playerEffectsText.text = "Player Effects:\n" + GetEffectsText(game.playerEffects);
            aiEffectsText.text = "AI Effects:\n" + GetEffectsText(game.aiEffects);
        }
        else
        {
            Debug.Log("Effects dictionaries are null.");
        }
    }

    string GetEffectsText(Dictionary<string, int> effects)
    {
        string effectsText = "";
        foreach (var effect in effects)
        {
            effectsText += $"{effect.Key}: {effect.Value} rounds remaining\n";
        }
        return effectsText;
    }
}
