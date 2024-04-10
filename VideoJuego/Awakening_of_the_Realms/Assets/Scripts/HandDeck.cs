using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDeck : MonoBehaviour
{
    public List<Card> handCards = new List<Card>(); // List to hold cards in hand
    public CardDisplayManager cardDisplayManager; // Reference to the CardDisplayManager to display cards
    public List<Card> displayedCards = new List<Card>(); // List to hold displayed cards



    public void ShuffleAndDisplayHand()
    {
        for (int i = 0; i < handCards.Count; i++)
        {
            Card temp = handCards[i];
            int randomIndex = Random.Range(i, handCards.Count);
            handCards[i] = handCards[randomIndex];
            handCards[randomIndex] = temp;
        }

        // Clear the displayedCards list
        displayedCards.Clear();

        // Display the first 5 cards from the shuffled hand
        for (int i = 0; i < 5; i++)
        {
            if (i < handCards.Count)
            {
                cardDisplayManager.DisplayCards(handCards[i]);
                displayedCards.Add(handCards[i]); // Add the card to the displayedCards list
            }
            else
            {
                Debug.Log("Not enough cards in hand to display 5 cards.");
                break;
            }
        }
    }
}
