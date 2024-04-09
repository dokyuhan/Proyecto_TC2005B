using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class APIConnection : MonoBehaviour
{
    //Variables para AddUser
    [SerializeField] private string apiURL = "http://localhost:3200";

    //variables para GetCard()
    int cardId = 1;
    Card card;
    public List<Card> cards = new List<Card>();


    public IEnumerator AddUser(string endpoint, string jsonData, System.Action<bool, string> callback)
    {
        using (UnityWebRequest www = UnityWebRequest.Put(apiURL + endpoint, jsonData))
        {
            www.method = "POST"; 
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                callback(true, ""); 
            }
            else
            {
                callback(false, www.error); 
            }
        }
    }

    public IEnumerator GetCards()
    {
        for (int i = 1; i <= 40; i++)
        {
            UnityWebRequest www = UnityWebRequest.Get(apiURL + "/api/awakening/cards/" + cardId);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed to fetch card {cardId}: {www.error}");
            }
            else
            {
                string data = www.downloadHandler.text;
                card = JsonUtility.FromJson<Card>(data);
                card.desbloqueada = true;
                cards.Add(card);
            }

            cardId++;
        }
    }


    public IEnumerator AddCardsToDeck(string endpoint, List<int> cardIDs, int playerID, int deckID, Action<bool, string> callback)
    {
        // Crear la lista de objetos card para el JSON basado en cardIDs
        var cardsData = new List<object>();
        foreach (var cardID in cardIDs)
        {
            cardsData.Add(new { card_ID = cardID, player_ID = playerID, deck_ID = deckID });
        }
        
        // Crear el objeto a serializar
        var payload = new { cards = cardsData };
        string jsonData = JsonUtility.ToJson(payload, true);

        using (UnityWebRequest www = UnityWebRequest.Put(apiURL + endpoint, jsonData))
        {
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                callback(true, www.downloadHandler.text);
            }
            else
            {
                callback(false, www.error);
            }
        }
    }

}

