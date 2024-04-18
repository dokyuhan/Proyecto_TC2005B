using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDeck : MonoBehaviour
{
    public List<Card> handCards = new List<Card>();
    public CardDisplayManager cardDisplayManager;
    public List<Card> displayedCards = new List<Card>();
    public int displayCount = 5;

    public void ShuffleAndDisplayHand()
    {
        Debug.Log("Shuffling Hand");
        int n = handCards.Count;
        for (int i = 0; i < n; i++)
        {
            int r = i + Random.Range(0, n - i);
            Card temp = handCards[r];
            handCards[r] = handCards[i];
            handCards[i] = temp;
        }
        DisplayHand();
    }

    public void DisplayHand()
    {
        Debug.Log("Displaying Hand");
        displayedCards.Clear();
        cardDisplayManager.ClearCardsUI();
        HashSet<Card> alreadyDisplayed = new HashSet<Card>();

        int countToDisplay = Mathf.Min(displayCount, handCards.Count);
        for (int i = 0, j = 0; i < countToDisplay && j < handCards.Count; j++)
        {
            if (!alreadyDisplayed.Contains(handCards[j]))
            {
                cardDisplayManager.DisplayCards(handCards[j]);
                displayedCards.Add(handCards[j]);
                alreadyDisplayed.Add(handCards[j]);
                i++;
            }
        }
        Debug.Log($"Displayed Cards Count: {displayedCards.Count}");
    }

    public void RefillDisplayedCards()
    {
        Debug.Log("Refilling Displayed Cards");
        if (displayedCards.Count < displayCount)
        {
            List<Card> availableCards = new List<Card>(handCards);
            availableCards.RemoveAll(card => displayedCards.Contains(card));

            while (displayedCards.Count < displayCount && availableCards.Count > 0)
            {
                int randomIndex = Random.Range(0, availableCards.Count);
                Card newCard = availableCards[randomIndex];
                cardDisplayManager.DisplayCards(newCard);
                displayedCards.Add(newCard);
                availableCards.RemoveAt(randomIndex);
            }
        }

        if (displayedCards.Count < displayCount)
        {
            ShuffleAndDisplayHand();
        }
    }
}
