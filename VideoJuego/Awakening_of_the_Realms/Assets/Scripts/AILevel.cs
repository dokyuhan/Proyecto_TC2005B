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
        switch (personality)
        {
            case AILevel.Human1:
                int [] human1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                AiPlay(fetchedCards, human1);
                break;
            case AILevel.Human2:
                int [] human2 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                AiPlay(fetchedCards, human2);
                break;
            case AILevel.Monster1:
                int [] monster1 = { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
                AiPlay(fetchedCards, monster1);
                break;
            case AILevel.Monster2:
                int [] monster2 = { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
                AiPlay(fetchedCards, monster2);
                break;
            case AILevel.Magical1:
                int [] magical1 = { 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
                AiPlay(fetchedCards, magical1);
                break;
            case AILevel.Magical2:
                int [] magical2 = { 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
                AiPlay(fetchedCards, magical2);
                break;
            case AILevel.Celestial1:
                int [] celestial1 = { 31, 32, 33, 34, 35, 36, 37, 38, 39, 40 };
                AiPlay(fetchedCards, celestial1);
                break;
            case AILevel.Celestial2:
                int [] celestial2 = { 31, 32, 33, 34, 35, 36, 37, 38, 39, 40 };
                AiPlay(fetchedCards, celestial2);
                break;
        }
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
