using System.Collections;
using UnityEngine;

public class AIFunction : MonoBehaviour
{
    public AI aiScript;
    public float cardPlacementInterval; // Interval in seconds between card placements
    public Transform cardPlacementUI1; // Reference to the first UI element where placed cards will be displayed
    public Transform cardPlacementUI2; // Reference to the second UI element where placed cards will be displayed
    public float cardDisplayDuration; // Duration in seconds for which the card is displayed in the UI

    private Coroutine cardPlacementCoroutine;

    private void Start()
    {
        StartCardPlacementRoutine();
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
        while (true)
        {
            PlaceRandomCard();
            yield return new WaitForSeconds(cardPlacementInterval);
        }
    }

    private void PlaceRandomCard()
    {
        if (aiScript.deckAI.cards.Count > 0)
        {
            int randomIndex = Random.Range(0, aiScript.deckAI.cards.Count);
            Card randomCard = aiScript.deckAI.cards[randomIndex];

            // Determine which UI space to place the card in
            Transform targetUI = (Random.Range(0, 2) == 0) ? cardPlacementUI1 : cardPlacementUI2;

            // Place the card in the target UI space
            PlaceCardInUI(randomCard, targetUI);

            // Start a coroutine to retrieve the card after a certain duration
            StartCoroutine(RetrieveCardAfterDuration(randomCard, cardDisplayDuration));

            // Optionally, add the card back to the deck at a different position to rotate the cards
            int newIndex = Random.Range(0, aiScript.deckAI.cards.Count);
            aiScript.deckAI.cards.Insert(newIndex, randomCard);
        }
        else
        {
            Debug.Log("No cards left in the deck.");
        }
    }

    private void PlaceCardInUI(Card card, Transform targetUI)
    {
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

    }

}
