using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;
using UnityEngine.UI;


public class AIFunction : MonoBehaviour
{
    public AI aiScript;
    public Transform cardPlacementUI1, cardPlacementUI2;

    private Arrastrar arrastrarScript;  // Reference to the Arrastrar script
    
    public EnergyBar energyBar;
    private float cardPlacementDuration = 1.0f;
    public Transform deckTransform;
    public void InitializeAIActions()
    {
        arrastrarScript = FindObjectOfType<Arrastrar>();
        StartCoroutine(PlaceCardsRoutine());
    }

    private IEnumerator PlaceCardsRoutine()
    {
        Debug.Log("Coroutine started");
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
        Debug.Log("All cards placed, routine complete.");
    }
    public void PlaceRandomCard()
    {
        Debug.Log($"Current hand count before placement: {aiScript.handDeck.displayedCards.Count}");
        if (aiScript.handDeck.displayedCards.Count < 2) // Ensure there are at least two cards
        {
            Debug.Log("Not enough cards in hand to place two cards.");
            return; // Exit if not enough cards
        }

        Debug.Log("Attempting to place two cards.");
        int randomIndex1 = 0, randomIndex2 = 0;
        Card card1 = null, card2 = null;

        // Get the list of cards that the AI can afford to place
        Debug.Log("Checking affordable cards.");
        List<Card> affordableCards = aiScript.handDeck.displayedCards.Where(card => card.power_cost <= energyBar.currentEnergy).ToList();
        Debug.Log($"Affordable cards count: {affordableCards.Count}");

        if (affordableCards.Count < 2)
        {
            Debug.Log("Not enough affordable cards to proceed.");
            return; // Exit if there are not enough affordable cards
        }

        // Randomly select two different affordable cards
        randomIndex1 = Random.Range(0, affordableCards.Count);
        card1 = affordableCards[randomIndex1];
        energyBar.DecrementEnergy(card1.power_cost);

        affordableCards = aiScript.handDeck.displayedCards.Where(card => card.power_cost <= energyBar.currentEnergy).ToList();
        do
        {
            randomIndex2 = Random.Range(0, affordableCards.Count);
        } while (randomIndex1 == randomIndex2);
        card2 = affordableCards[randomIndex2];
        energyBar.DecrementEnergy(card2.power_cost);

        Debug.Log($"Placing card1: {card1.card_name} at index {randomIndex1}");
        Debug.Log($"Placing card2: {card2.card_name} at index {randomIndex2}");

        StartPlaceCardInUI(card1, cardPlacementUI1, cardPlacementDuration);
        StartPlaceCardInUI(card2, cardPlacementUI2, cardPlacementDuration);

        if (arrastrarScript != null)
        {
            Debug.Log("Invoking placement events.");
            arrastrarScript.InvokeOnCardPlacedInOpponentZone(card1);
            arrastrarScript.InvokeOnCardPlacedInOpponentZone(card2);

            // Hacer la animación de transparencia en la carta 1
            StartCoroutine(FadeCardToTransparentAndBack(card1.cardGameObject));

            // Hacer la animación de transparencia en la carta 2
            StartCoroutine(FadeCardToTransparentAndBack(card2.cardGameObject));

        }
        else
        {
            Debug.LogError("Arrastrar script not found.");
        }
    }

    IEnumerator FadeCardToTransparentAndBack(GameObject cardGameObject)
    {
        Image imagenCarta = cardGameObject.transform.Find("back").GetComponent<Image>();
        Color originalColor = imagenCarta.color;
        // Cambiar alfa a 0
        imagenCarta.color = new Color(imagenCarta.color.r, imagenCarta.color.g, imagenCarta.color.b, 0);
        // Esperar 2 segundos
        yield return new WaitForSeconds(2);
        // Restaurar alfa a 255
        imagenCarta.color = originalColor;
    }

    private IEnumerator PlaceCardInUI(Card card, Transform targetUI, float duration)
    {
        RectTransform cardRectTransform = card.cardGameObject.GetComponent<RectTransform>();
        cardRectTransform.SetParent(targetUI, false);

        Vector3 startPosition = deckTransform.localPosition;
        Vector3 endPosition = Vector3.zero;

        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            cardRectTransform.localPosition = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        cardRectTransform.localPosition = endPosition;
        Debug.Log("Placed card at UI: " + targetUI.name);
    }

    // Call this method instead of PlaceCardInUI
    public void StartPlaceCardInUI(Card card, Transform targetUI, float duration)
    {
        StartCoroutine(PlaceCardInUI(card, targetUI, duration));
    }
}