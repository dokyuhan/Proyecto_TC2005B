using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIFunction : MonoBehaviour
{
    public AI aiScript;
    public Transform cardPlacementUI1, cardPlacementUI2;
    private bool isTurnComplete = false;
    public bool IsTurnComplete => isTurnComplete;

    private Arrastrar arrastrarScript;  // Reference to the Arrastrar script

    public void InitializeAIActions()
    {
        arrastrarScript = FindObjectOfType<Arrastrar>();
        StartCoroutine(PlaceCardsRoutine());
    }

    private IEnumerator PlaceCardsRoutine()
    {
        Debug.Log("Coroutine started");
        isTurnComplete = false;
        Debug.Log($"Initial card count: {aiScript.handDeck.displayedCards.Count}"); // Check the initial count of cards
        if (aiScript.handDeck.displayedCards.Count < 1)
        {
            yield return new WaitForSeconds(2f);
            Debug.Log("No hay cartas en el mazo");
        }
        else {
            Debug.Log("Inicializando el funcionamiento de la IA");
            PlaceRandomCard();
        }
        isTurnComplete = true;
        Debug.Log("All cards placed, routine complete.");
    }
    public void PlaceRandomCard()
    {
        Debug.Log($"Current hand count before placement: {aiScript.handDeck.displayedCards.Count}");
        if (aiScript.handDeck.displayedCards.Count >= 2) // Ensure there are at least two cards
        {
            Debug.Log("Attempting to place two cards.");
            int randomIndex1 = Random.Range(0, aiScript.handDeck.displayedCards.Count);
            Card card1 = aiScript.handDeck.displayedCards[randomIndex1];
            //aiScript.handDeck.displayedCards.RemoveAt(randomIndex1);
            Debug.Log($"Placing card1: {card1.card_name} at index {randomIndex1}");

            // Recalculate randomIndex2 safely after removal of the first card
            int randomIndex2 = Random.Range(0, aiScript.handDeck.displayedCards.Count);
            Card card2 = aiScript.handDeck.displayedCards[randomIndex2];
            //aiScript.handDeck.displayedCards.RemoveAt(randomIndex2);
            Debug.Log($"Placing card2: {card2.card_name} at index {randomIndex2}");

            PlaceCardInUI(card1, cardPlacementUI1);
            PlaceCardInUI(card2, cardPlacementUI2);

            if (arrastrarScript != null)
            {
                Debug.Log("Invoking placement events.");
                arrastrarScript.InvokeOnCardPlacedInOpponentZone(card1);
                arrastrarScript.InvokeOnCardPlacedInOpponentZone(card2);
            }
            else
            {
                Debug.LogError("Arrastrar script not found.");
            }
        }
        else
        {
            Debug.Log("Not enough cards in hand to place two cards.");
        }
    }



    private void PlaceCardInUI(Card card, Transform targetUI)
    {
        RectTransform cardRectTransform = card.cardGameObject.GetComponent<RectTransform>();
        cardRectTransform.SetParent(targetUI, false);
        cardRectTransform.localPosition = Vector3.zero;
        Debug.Log("Placed card at UI: " + targetUI.name);
    }
}
