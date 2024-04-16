using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum AILevel { Human1, Human2, Monster1, Monster2, Magical1, Magical2, Celestial1, Celestial2 }

public class AI : MonoBehaviour
{
    public AILevel personality;
    public CardFetch deckAI;
    public Card aiCard;
    public HandDeck handDeck;
    public Action OnAIDeckReady;

    private void OnEnable()
    {
        CardFetch.CardsFetched += DisplayFetchedCards;
    }

    private void OnDisable()
    {
        CardFetch.CardsFetched -= DisplayFetchedCards;
    }


    void DisplayFetchedCards(List<Card> fetchedCards)
    {
        int[] deck = new int[10];
        switch (personality)
        {
            case AILevel.Human1:
                deck = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                break;
            case AILevel.Human2:
                deck = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                break;
            case AILevel.Monster1:
                deck = new int[] { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
                break;
            case AILevel.Monster2:
                deck = new int[] { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
                break;
            case AILevel.Magical1:
                deck = new int[] { 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
                break;
            case AILevel.Magical2:
                deck = new int[] { 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
                break;
            case AILevel.Celestial1:
                deck = new int[] { 31, 32, 33, 34, 35, 36, 37, 38, 39, 40 };
                break;
            case AILevel.Celestial2:
                deck = new int[] { 31, 32, 33, 34, 35, 36, 37, 38, 39, 40 };
                break;
        }
        AiPlay(fetchedCards, deck);
    }

    void AiPlay(List<Card> fetchedCards, int[] deck)
    {

        foreach (int cardID in deck)
        {
            aiCard = deckAI.cards.Find(card => card.card_ID == cardID);
            
            // Check if a card with the given ID was found
            if (aiCard != null)
            {
                handDeck.handCards.Add(aiCard); // Add the card to the hand deck
            }
            else
            {
                Debug.Log("Card with ID " + cardID + " not found.");
            }
        }
        Debug.Log("AI deck loaded.");
        handDeck.ShuffleAndDisplayHand(); // Shuffle and display the hand deck
        Debug.Log("AI hand shuffled and displayed.");
        OnAIDeckReady?.Invoke();  // Notify that AI deck is ready

    }
    

}
