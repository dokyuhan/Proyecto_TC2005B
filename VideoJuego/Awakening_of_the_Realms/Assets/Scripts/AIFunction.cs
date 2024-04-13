using System.Collections;
using UnityEngine;

public class AIFunction : MonoBehaviour
{
    public AI aiScript;
    public float cardPlacementInterval; // Interval in seconds between card placements
    public Transform cardPlacementUI1, cardPlacementUI2;
    public float cardDisplayDuration; // Duration in seconds for which the card is displayed in the UI

    private bool isTurnComplete = false;
    public bool IsTurnComplete {
        get { return isTurnComplete; }
    }

    public void InitializeAIActions()
    {
        StartCoroutine(PlaceCardsRoutine());
    }

    private IEnumerator PlaceCardsRoutine()
    {
        while (aiScript.handDeck.displayedCards.Count > 1) // Ensure there are at least two cards to place
        {
            yield return new WaitForSeconds(2f);
            PlaceRandomCard();
            yield return new WaitForSeconds(cardPlacementInterval);
        }
        isTurnComplete = true; // Mark the end of AI's turn actions
    }

    public bool AITurnComplete()
    {
        return isTurnComplete;
    }

    public void PlaceRandomCard()
    {
        if (aiScript.handDeck.displayedCards.Count > 1) // Ensure there are at least two cards to place
        {
            int randomIndex1 = Random.Range(0, aiScript.handDeck.displayedCards.Count);
            Card card1 = aiScript.handDeck.displayedCards[randomIndex1];
            aiScript.handDeck.displayedCards.RemoveAt(randomIndex1); // Remove the first card from the hand

            int randomIndex2 = Random.Range(0, aiScript.handDeck.displayedCards.Count);
            Card card2 = aiScript.handDeck.displayedCards[randomIndex2];
            aiScript.handDeck.displayedCards.RemoveAt(randomIndex2); // Remove the second card from the hand

            // Place the cards in both UI spaces
            PlaceCardInUI(card1, cardPlacementUI1);
            PlaceCardInUI(card2, cardPlacementUI2);

            // Start a coroutine to retrieve the cards after a certain duration
            StartCoroutine(RetrieveCardAfterDuration(card1, cardDisplayDuration));
            StartCoroutine(RetrieveCardAfterDuration(card2, cardDisplayDuration));
        }
        else
        {
            Debug.Log("Not enough cards in hand to place.");
        }
    }

    private void PlaceCardInUI(Card card, Transform targetUI)
    {
        if (card.cardGameObject == null)
        {
            Debug.LogError("Card GameObject is null for card: " + card.card_name);
            return; // Exit the method to avoid a NullReferenceException
        }

        RectTransform cardRectTransform = card.cardGameObject.GetComponent<RectTransform>();
        cardRectTransform.SetParent(targetUI, false);
        cardRectTransform.localPosition = Vector3.zero; // Center the card in the target UI
    }

    private IEnumerator RetrieveCardAfterDuration(Card card, float duration)
    {
        yield return new WaitForSeconds(duration);

        // Check if the card is still in the displayedCards list
        if (aiScript.handDeck.displayedCards.Contains(card))
        {
            // Nullify the GameObject reference before removing the card
            int index = aiScript.handDeck.displayedCards.IndexOf(card);
            if (index != -1)  // Make sure the index is valid
            {
                aiScript.handDeck.displayedCards[index].cardGameObject = null;
                aiScript.handDeck.displayedCards.RemoveAt(index);
            }
        }

        // Safely destroy the GameObject
        Destroy(card.cardGameObject);

        // Check if we need to refill the displayed cards to maintain a minimum count
        if (aiScript.handDeck.displayedCards.Count < 5)
        {
            aiScript.handDeck.RefillDisplayedCards();  // Refill if necessary
        }
    }

}
