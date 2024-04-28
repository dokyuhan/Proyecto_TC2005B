using UnityEngine;
using TMPro; // Import the TextMeshPro namespace
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;
using System;

public class EffectsDebug : MonoBehaviour
{
    public Image playerEffectsContainer;
    public Image aiEffectsContainer;
    public TextMeshProUGUI playerEffectDescription; // TextMeshPro for player effects
    public TextMeshProUGUI aiEffectDescription; // TextMeshPro for AI effects
    private Game game;

    [SerializeField] private List<string> effectNames;
    [SerializeField] private List<Sprite> effectSprites;
    [SerializeField] private List<string> effectDescriptions;

    private Dictionary<string, Sprite> effectIcons = new Dictionary<string, Sprite>();
    private Dictionary<string, string> effectDescriptionsDict = new Dictionary<string, string>();

    public static event Action OnTurnEnd;
    public static void TriggerTurnEnd()
    {
        OnTurnEnd?.Invoke();
    }

    private void OnEnable() {
        OnTurnEnd += ClearEffectDescriptions;
    }

    private void OnDisable() {
        OnTurnEnd -= ClearEffectDescriptions;
    }


    void Start()
    {
        game = Game.Instance;
        InitializeEffectIcons();
    }

    void InitializeEffectIcons()
    {
        effectIcons.Clear();
        effectDescriptionsDict.Clear();
        for (int i = 0; i < Mathf.Min(effectNames.Count, effectSprites.Count, effectDescriptions.Count); i++)
        {
            effectIcons.Add(effectNames[i], effectSprites[i]);
            effectDescriptionsDict.Add(effectNames[i], effectDescriptions[i]);
        }
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
        UpdateEffectIcons(game.playerEffects, playerEffectsContainer, playerEffectDescription);
        UpdateEffectIcons(game.aiEffects, aiEffectsContainer, aiEffectDescription);
    }

    void UpdateEffectIcons(Dictionary<string, int> effects, Image container, TextMeshProUGUI descriptionText)
    {
        // Remove all children that are not currently active effects
        foreach (Transform child in new List<Transform>(container.transform.Cast<Transform>()))
        {
            if (!effects.ContainsKey(child.name))
            {
                Destroy(child.gameObject);
            }
        }

        // Create or update icons for active effects
        foreach (var effect in effects)
        {
            Transform existingIcon = container.transform.Find(effect.Key);
            if (existingIcon == null)
            {
                // Create new icon if it doesn't exist
                GameObject iconObject = new GameObject(effect.Key);
                iconObject.transform.SetParent(container.transform, false);
                Image iconImage = iconObject.AddComponent<Image>();
                iconImage.sprite = effectIcons[effect.Key];
                AddEventTrigger(iconObject, effect.Key, descriptionText);
            }
            else
            {
                // Update existing icon if necessary (e.g., update tooltip, etc.)
                // Note: In this specific code block, nothing needs updating yet, but you could add logic here if needed.
            }
        }
    }

    void AddEventTrigger(GameObject iconObject, string effectKey, TextMeshProUGUI descriptionText)
    {
        Debug.Log("Adding event trigger to: " + effectKey);
        EventTrigger trigger = iconObject.AddComponent<EventTrigger>();
        var entry = new EventTrigger.Entry { eventID = EventTriggerType.PointerClick };
        entry.callback.AddListener((data) => { OnEffectIconClicked(effectKey, descriptionText); });
        trigger.triggers.Add(entry);
    }

    public void OnEffectIconClicked(string effectKey, TextMeshProUGUI descriptionText)
    {
        Debug.Log("Effect icon clicked: " + effectKey);
        if (effectDescriptionsDict.TryGetValue(effectKey, out string description))
        {
            descriptionText.text = description;
        }
    }

    public void ClearEffectDescriptions()
    {
        playerEffectDescription.text = "";
        aiEffectDescription.text = "";
    }

}
