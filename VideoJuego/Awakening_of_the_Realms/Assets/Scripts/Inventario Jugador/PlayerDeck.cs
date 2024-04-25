using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    public APIConnection conexion;
    public Card playerCard;
    public CardFetch playerDeck;
    public HandDeck handDeck;
    public List<int> deck = new List<int>();

    private void OnEnable()
    {
        playerDeck.cardsFetched += PlayerCards;
    }

    private void OnDisable()
    {
        playerDeck.cardsFetched -= PlayerCards;
    }

    // Adjusted to accept deckIdentifier
    public void PlayerCards(string deckIdentifier, List<Card> fetchedCards)
    {
        // Check if the fetched cards are for the player's deck
        if (deckIdentifier == "playerDeck") // Assuming "playerDeck" is the identifier used for player's cards
        {
            StartCoroutine(GetAndProcessCardIds(fetchedCards));
        }
    }

    private IEnumerator GetAndProcessCardIds(List<Card> fetchedCards)
    {
        yield return StartCoroutine(conexion.GetCardIdsForPlayer(Usuario.usuario.player_ID, ProcessCardIds));

        PlayerCardDeck(fetchedCards, deck);
    }

    public void ProcessCardIds(List<int> cardIds)
    {
        deck.Clear(); 
        foreach (int cardId in cardIds)
        {
            deck.Add(cardId);
        }
    }

    public void PlayerCardDeck(List<Card> fetchedCards, List<int> deck)
    {
        foreach (int cardID in deck)
        {
            playerCard = fetchedCards.Find(card => card.card_ID == cardID);

            if (playerCard != null)
            {
                handDeck.handCards.Add(playerCard);
            }
            else
            {
                Debug.Log("Card with ID " + cardID + " not found.");
            }
        }
        handDeck.ShuffleAndDisplayHand();
    }
}