using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class APIConnection : MonoBehaviour
{
    //Variables para AddUser
    [SerializeField] private string apiURL = "http://localhost:3200";

    public List<int> cardIds = new List<int>();
    Card card;



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



    public IEnumerator AddCardsToDeck(string endpoint, string jsonData, System.Action<bool, string> callback)
    {

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

    public IEnumerator GetCardIdsForPlayer(string playerId, Action<List<int>> callback)
    {
        UnityWebRequest www = UnityWebRequest.Get(apiURL + "/api/awakening/inventory/" + playerId);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Failed to fetch card IDs for player {playerId}: {www.error}");
        }
        else
        {
            string jsonResponse = www.downloadHandler.text;
            CardIdListResponse response = JsonUtility.FromJson<CardIdListResponse>(jsonResponse);

            cardIds.Clear(); // Limpiar la lista antes de agregar nuevos IDs
            if (response.cardIds != null && response.cardIds.Length > 0)
            {
                cardIds.AddRange(response.cardIds);
            }

            callback?.Invoke(cardIds); // Ejecutar el callback con los IDs obtenidos
        }
    }


    public IEnumerator LogIn(string endpoint, string jsonData, System.Action<bool, string> callback)
    {
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

    public IEnumerator GetCards(int id, List<Card> lista)
    {

        UnityWebRequest www = UnityWebRequest.Get(apiURL + "/api/awakening/cards/" + id);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Failed to fetch card {id}: {www.error}");
        }
        else
        {
            string data = www.downloadHandler.text;
            card = JsonUtility.FromJson<Card>(data);
            card.desbloqueada = true;
            lista.Add(card);
        }
    }

    public IEnumerator BuyCard(int userId, Action<Card, string> onCardReceived)
    {
        string endpoint = "/api/awakening/players/" + userId + "/inventory/buyCard";
        using (UnityWebRequest www = UnityWebRequest.Put(apiURL + endpoint, ""))
        {
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed to buy card for user {userId}: {www.error}");
                onCardReceived?.Invoke(null, www.error); // Invoke with null card and error message
            }
            else
            {
                Debug.Log("Card bought successfully for user " + userId);
                CardResponse response = JsonUtility.FromJson<CardResponse>(www.downloadHandler.text);
                if (response.card != null)
                {
                    response.card.desbloqueada = true; 
                    onCardReceived?.Invoke(response.card, response.message); 
                }
                else
                {
                    Debug.LogError("No card data found in the response.");
                    onCardReceived?.Invoke(null, "No card data found in the response."); // Invoke with null card and error message
                }
            }
        }
    }

}

