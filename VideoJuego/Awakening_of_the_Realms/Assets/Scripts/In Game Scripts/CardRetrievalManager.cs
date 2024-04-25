using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRetrievalManager : MonoBehaviour
{
    public Transform playerCardArea1, playerCardArea2, aiCardArea1, aiCardArea2;

    public void ClearCardsUI(Transform cardArea)
    {
        foreach (Transform child in cardArea)
        {
            Destroy(child.gameObject);
        }
    }

    public void ClearAllCardsUI()
    {
        ClearCardsUI(playerCardArea1);
        ClearCardsUI(playerCardArea2);
        ClearCardsUI(aiCardArea1);
        ClearCardsUI(aiCardArea2);
    }

    public IEnumerator RetrieveCards(List<Card> cards, HandDeck deck, float delay, System.Action onComplete)
    {
        Debug.Log($"Waiting {delay} seconds to retrieve cards...");
        yield return new WaitForSeconds(delay);

        Debug.Log($"Retrieving cards for deck. Total cards to check: {cards.Count}");
        foreach (Card card in cards)
        {
            if (deck.displayedCards.Contains(card))
            {
                Debug.Log($"Removing card {card.card_name} from display.");
                deck.displayedCards.Remove(card);
                Debug.Log($"Destroying GameObject for card {card.card_name}");
                if (card.cardGameObject != null && card.cardGameObject.activeInHierarchy) {
                    Destroy(card.cardGameObject);
                }
            }
            else
            {
                Debug.LogWarning($"Card {card.card_name} NOT found in display. Current displayed cards count: {deck.displayedCards.Count}");
            }
        }

        if (deck.displayedCards.Count < 5)
        {
            Debug.Log("Refilling displayed cards due to insufficient count.");
            deck.RefillDisplayedCards();
        }

        cards.Clear();
        onComplete();
    }
}