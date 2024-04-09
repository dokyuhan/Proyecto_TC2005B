using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AILevel { Human1, Human2, Monster1, Monster2, Magical1, Magical2, Celestial1, Celestial2 }

public class AI : MonoBehaviour
{
    public AILevel personality;
    public CardFetch deckAI;
    public Card aiCard;
    public CardDisplayManager cardDisplayManager;

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
                PlayHuman1(fetchedCards);
                break;
            case AILevel.Human2:
                PlayHuman2(fetchedCards);
                break;
            case AILevel.Monster1:
                PlayMonster1(fetchedCards);
                break;
            case AILevel.Monster2:
                PlayMonster2(fetchedCards);
                break;
            case AILevel.Magical1:
                PlayMagical1(fetchedCards);
                break;
            case AILevel.Magical2:
                PlayMagical2(fetchedCards);
                break;
            case AILevel.Celestial1:
                PlayCelestial1(fetchedCards);
                break;
            case AILevel.Celestial2:
                PlayCelestial2(fetchedCards);
                break;
        }
    }

    void PlayHuman1(List<Card> fetchedCards)
    {
        int [] human1Deck = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        foreach (int cardID in human1Deck)
        {
            aiCard = deckAI.cards.Find(card => card.card_ID == cardID);
            
            // Check if a card with the given ID was found
            if (aiCard != null)
            {
                cardDisplayManager.DisplayCards(aiCard);
            }
            else
            {
                Debug.Log("Card with ID " + cardID + " not found.");
            }
        }

    }

    void PlayHuman2(List<Card> fetchedCards)
    {
        int [] human2Deck = { 11, 12, 13, 14, 15, 20, 22, 23, 25, 30 };

        foreach (int cardID in human2Deck)
        {
            aiCard = deckAI.cards.Find(card => card.card_ID == cardID);
            
            // Check if a card with the given ID was found
            if (aiCard != null)
            {
                cardDisplayManager.DisplayCards(aiCard);
            }
            else
            {
                Debug.Log("Card with ID " + cardID + " not found.");
            }
        }

    }

    void PlayMonster1(List<Card> fetchedCards)
    {

    }

    void PlayMonster2(List<Card> fetchedCards)
    {

    }

    void PlayMagical1(List<Card> fetchedCards)
    {

    }

    void PlayMagical2(List<Card> fetchedCards)
    {

    }

    void PlayCelestial1(List<Card> fetchedCards)
    {

    }

    void PlayCelestial2(List<Card> fetchedCards)
    {

    }
}