using System.Collections;
using UnityEngine;

public class AIFunction : MonoBehaviour
{
    public AI aiScript;
    public float cardPlacementInterval; // Interval in seconds between card placements
    public Transform cardPlacementUI1; // Reference to the first UI element where placed cards will be displayed
    public Transform cardPlacementUI2; // Reference to the second UI element where placed cards will be displayed
    public float cardDisplayDuration; // Duration in seconds for which the card is displayed in the UI

    public Card currentCard;
    private Coroutine cardPlacementCoroutine;

    private void Start()
    {
        //StartCardPlacementRoutine();
    }

    private void OnDestroy()
    {
        StopCardPlacementRoutine();
    }

    private void StartCardPlacementRoutine()
    {
        if (cardPlacementCoroutine == null)
        {
            cardPlacementCoroutine = StartCoroutine(PlaceCardAtIntervals());
        }
    }

    private void StopCardPlacementRoutine()
    {
        if (cardPlacementCoroutine != null)
        {
            StopCoroutine(cardPlacementCoroutine);
            cardPlacementCoroutine = null;
        }
    }

    private IEnumerator PlaceCardAtIntervals()
    {
        bool useFirstUI = true; // Toggle between the two UI elements
        while (true)
        {
            yield return new WaitForSeconds(cardPlacementInterval);
            Transform targetUI = useFirstUI ? cardPlacementUI1 : cardPlacementUI2;
            PlaceRandomCard(targetUI);
            useFirstUI = !useFirstUI; // Switch to the other UI element for the next card
        }
    }

    public void PlaceRandomCard(Transform targetUI)
    {
        if (aiScript.handDeck.displayedCards.Count > 0)
        {
            int randomIndex = Random.Range(0, aiScript.handDeck.displayedCards.Count);
            Card randomCard = aiScript.handDeck.displayedCards[randomIndex];

            // Place the card in both UI spaces
            PlaceCardInUI(randomCard, targetUI);

            // Start a coroutine to retrieve the card after a certain duration
            StartCoroutine(RetrieveCardAfterDuration(randomCard, cardDisplayDuration));

            // Remove the card from the displayedCards list
            aiScript.handDeck.displayedCards.RemoveAt(randomIndex);

            // Add the card back to the main deck at a random position
            int newIndex = Random.Range(0, aiScript.deckAI.cards.Count);
            aiScript.deckAI.cards.Insert(newIndex, randomCard);
        }
        else
        {
            Debug.Log("No displayed cards to place.");
        }
    }



    private void PlaceCardInUI(Card card, Transform targetUI)
    {
        if (card.cardGameObject == null)
        {
            Debug.LogError("Card GameObject is null for card: " + card.card_name);
            return; // Exit the method to avoid the NullReferenceException
        }
        currentCard = card;
        RectTransform cardRectTransform = card.cardGameObject.GetComponent<RectTransform>();
        cardRectTransform.SetParent(targetUI, false);
        cardRectTransform.localPosition = Vector3.zero; // Center the card in the target UI
    }

    private IEnumerator RetrieveCardAfterDuration(Card card, float duration)
    {
        yield return new WaitForSeconds(duration);

        // Remove the card from its current position
        RectTransform cardRectTransform = card.cardGameObject.GetComponent<RectTransform>();
        cardRectTransform.SetParent(null); // Detach from the current parent

        // Find a new random position in the deck
        int newIndex = Random.Range(0, aiScript.deckAI.cards.Count);

        // Insert the card back into the deck at the new position
        aiScript.deckAI.cards.Insert(newIndex, card);

        // Display a new card if there are still cards left to display
        if (aiScript.handDeck.displayedCards.Count < aiScript.handDeck.handCards.Count)
        {
            Card newCard = aiScript.handDeck.handCards[aiScript.handDeck.displayedCards.Count];
            aiScript.handDeck.cardDisplayManager.DisplayCards(newCard);
            aiScript.handDeck.displayedCards.Add(newCard);
        }
    }


}
