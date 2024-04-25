using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class CardFetch : MonoBehaviour
{
    [SerializeField] private string apiURL = "127.0.0.1:3200";
    [SerializeField] private string cardEndpoint = "/api/awakening/cards/";

    Card card;

    // Use a dictionary to keep track of which deck is getting updated
    public Dictionary<string, List<Card>> cardsByDeck = new Dictionary<string, List<Card>>();
    public delegate void OnCardsFetched(string deckIdentifier, List<Card> cards);
    public event OnCardsFetched cardsFetched;

    private void Start()
    {
        // Dynamically fetching cards based on AILevel enum
        foreach (AILevel level in Enum.GetValues(typeof(AILevel)))
        {
            string deckIdentifier = level.ToString() + "Deck";
            StartCoroutine(FetchCards(deckIdentifier));
        }
        StartCoroutine(FetchCards("playerDeck"));
    }

    IEnumerator FetchCards(string deckIdentifier)
    {
        List<Card> cards = new List<Card>();
        for (int i = 1; i <= 40; i++)
        {
            yield return StartCoroutine(GetCard(i, cards));
        }
        cardsByDeck[deckIdentifier] = cards; // Assign the fetched cards to the right deck
        cardsFetched?.Invoke(deckIdentifier, cards);
    }

    IEnumerator GetCard(int id, List<Card> cards)
    {
        UnityWebRequest www = UnityWebRequest.Get($"{apiURL}{cardEndpoint}{id}");
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.Success)
        {
            string data = www.downloadHandler.text;
            card = JsonUtility.FromJson<Card>(data);
            card.uniqueID = UnityEngine.Random.Range(100000, 999999);
            card.desbloqueada = true;
            cards.Add(card);
        }
        else
        {
            Debug.Log($"Request failed: {www.error}");
        }
    }
}
