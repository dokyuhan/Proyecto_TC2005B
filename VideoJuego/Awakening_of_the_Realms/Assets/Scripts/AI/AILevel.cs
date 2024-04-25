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

    private void OnEnable()
    {
        deckAI.cardsFetched += AIFetchedCards;
    }

    private void OnDisable()
    {
        deckAI.cardsFetched -= AIFetchedCards;
    }

    void AIFetchedCards(string deckIdentifier, List<Card> fetchedCards)
    {
        if (deckIdentifier == personality.ToString() + "Deck")
        {
            DisplayFetchedCards(fetchedCards);
        }
    }



    void DisplayFetchedCards(List<Card> fetchedCards)
    {
        int[] deck = new int[10];
        switch (personality)
        {
            case AILevel.Human1:  
                deck = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 33, 34};
                break;
            case AILevel.Human2:
                deck = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 33, 34};
                break;
            case AILevel.Monster1:
                deck = new int[] { 9, 10, 11, 12, 13, 14, 15, 16, 35, 36 };
                break;
            case AILevel.Monster2:
                deck = new int[] { 9, 10, 11, 12, 13, 14, 15, 16, 35, 36 };
                break;
            case AILevel.Magical1:
                deck = new int[] { 17, 18, 19, 20, 21, 22, 23, 24, 37, 38 };
                break;
            case AILevel.Magical2:
                deck = new int[] { 17, 18, 19, 20, 21, 22, 23, 24, 37, 38 };
                break;
            case AILevel.Celestial1:
                deck = new int[] { 25, 26, 27, 28, 29, 30, 31, 32, 39, 40 };
                break;
            case AILevel.Celestial2:
                deck = new int[] { 25, 26, 27, 28, 29, 30, 31, 32, 39, 40 };
                break;
        }
        AiPlay(fetchedCards, deck);
    }

    void AiPlay(List<Card> fetchedCards, int[] deck)
    {
        foreach (int cardID in deck)
        {
            aiCard = fetchedCards.Find(card => card.card_ID == cardID);
            
            // Check if a card with the given ID was found
            if (aiCard != null)
            {
                handDeck.handCards.Add(aiCard); // Add the card to the hand deck
            }
            else
            {
            }
        }
        handDeck.ShuffleAndDisplayHand(); // Shuffle and display the hand deck

    }
    

}
