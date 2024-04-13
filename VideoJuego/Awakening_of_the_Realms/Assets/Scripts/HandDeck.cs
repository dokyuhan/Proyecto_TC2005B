using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDeck : MonoBehaviour
{
    
    public List<Card> handCards = new List<Card>(); // List to hold cards in hand
    public CardDisplayManager cardDisplayManager; // Reference to the CardDisplayManager to display cards
    public List<Card> displayedCards = new List<Card>(); // List to hold displayed cards
    public int displayCount = 5; // Default to 5, can be adjusted in the Inspector


    public void ShuffleAndDisplayHand()
    {
        // Shuffle the hand
        for (int i = 0; i < handCards.Count; i++)
        {
            Card temp = handCards[i];
            int randomIndex = Random.Range(i, handCards.Count);
            handCards[i] = handCards[randomIndex];
            handCards[randomIndex] = temp;
        }

        // Display the shuffled hand
        DisplayHand();
    }

    public void DisplayHand()
    {
        displayedCards.Clear();
        int countToDisplay = Mathf.Min(displayCount, handCards.Count);
        for (int i = 0; i < countToDisplay; i++)
        {
            cardDisplayManager.DisplayCards(handCards[i]);
            displayedCards.Add(handCards[i]);
        }
    }

    public void RefillDisplayedCards()
    {
        int cardsNeeded = displayCount - displayedCards.Count;
        for (int i = 0; i < cardsNeeded && handCards.Count > displayedCards.Count; i++)
        {
            Card newCard = handCards[displayedCards.Count];
            displayedCards.Add(newCard);
            cardDisplayManager.DisplayCards(newCard);
        }
        UpdateHandUI();
    }


    public void UpdateHandUI()
    {
        cardDisplayManager.ClearCardsUI();  // Clear current UI elements
        DisplayHand();
    }
}