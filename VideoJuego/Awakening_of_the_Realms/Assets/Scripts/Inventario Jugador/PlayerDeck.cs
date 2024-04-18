using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerDeck : MonoBehaviour
{
    //public APIConnection conexion;
    public Card playerCard;
    public CardFetch playerDeck;
    public HandDeck handDeck;

    private void OnEnable()
    {
        CardFetch.CardsFetched += PlayerCards;
    }

    private void OnDisable()
    {
        CardFetch.CardsFetched -= PlayerCards;
    }
    public void PlayerCards(List<Card> fetchedCards)
    {
        int [] deck = {11, 12, 13, 14, 15, 16, 17, 18, 19, 20}; // Example player deck
        PlayerCardDeck(fetchedCards, deck);
    }

    
    public void PlayerCardDeck(List<Card> fetchedCards, int[] deck)
    {
        foreach (int cardID in deck)
        {
            playerCard = playerDeck.cards.Find(card => card.card_ID == cardID);
            
            // Check if a card with the given ID was found
            if (playerCard != null)
            {
                handDeck.handCards.Add(playerCard); // Add the card to the hand deck
            }
            else
            {
                Debug.Log("Card with ID " + cardID + " not found.");
            }
        }
        handDeck.ShuffleAndDisplayHand(); // Shuffle and display the hand deck
    }
    
}