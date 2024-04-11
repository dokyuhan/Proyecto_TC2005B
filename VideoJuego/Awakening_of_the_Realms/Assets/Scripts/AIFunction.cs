using System.Collections;
using UnityEngine;

public class AIFunction : MonoBehaviour
{
    public AI aiScript;
    public float cardPlacementInterval = 2f; // Example interval
    public Transform cardPlacementUI1;
    public Transform cardPlacementUI2;
    public float cardDisplayDuration = 5f; // Example display duration

    private WaitForSeconds placementIntervalWait;
    private WaitForSeconds cardDisplayDurationWait;
    private Coroutine cardPlacementCoroutine;

    private void Start()
    {
        // Initialize WaitForSeconds objects to be reused
        placementIntervalWait = new WaitForSeconds(cardPlacementInterval);
        cardDisplayDurationWait = new WaitForSeconds(cardDisplayDuration);

        // Start the card placement coroutine
        cardPlacementCoroutine = StartCoroutine(PlaceCardAtIntervals());
    }

    private IEnumerator PlaceCardAtIntervals()
    {
        bool useFirstUI = true;
        while (true)
        {
            yield return placementIntervalWait; // Reuse the WaitForSeconds instance
            Transform targetUI = useFirstUI ? cardPlacementUI1 : cardPlacementUI2;
            PlaceRandomCard(targetUI);
            useFirstUI = !useFirstUI;
        }
    }

    public void PlaceRandomCard(Transform targetUI)
    {
        if (aiScript.handDeck.displayedCards.Count > 0)
        {
            int randomIndex = Random.Range(0, aiScript.handDeck.displayedCards.Count);
            Card randomCard = aiScript.handDeck.displayedCards[randomIndex];

            PlaceCardInUI(randomCard, targetUI);
            StartCoroutine(RetrieveCardAfterDuration(randomCard)); // Modified to not pass duration every time
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
            Debug.LogError($"Card GameObject is null for card: {card.card_name}");
            return;
        }
        RectTransform cardRectTransform = card.cardGameObject.GetComponent<RectTransform>();
        cardRectTransform.SetParent(targetUI, false);
        cardRectTransform.localPosition = Vector3.zero;
    }

    private IEnumerator RetrieveCardAfterDuration(Card card)
    {
        yield return cardDisplayDurationWait; // Reuse the WaitForSeconds instance

        RectTransform cardRectTransform = card.cardGameObject.GetComponent<RectTransform>();
        cardRectTransform.SetParent(null); // Detach from the current parent

        // Logic to reinsert the card into the deck can go here
    }

    private void OnDestroy()
    {
        if (cardPlacementCoroutine != null)
        {
            StopCoroutine(cardPlacementCoroutine);
        }
    }
}
