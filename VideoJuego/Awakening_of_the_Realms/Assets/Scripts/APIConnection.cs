using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class APIConnection : MonoBehaviour
{
    //Variables para AddUser
    private string apiURL = "http://127.0.0.1:3200";

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


    public IEnumerator AddCoins(string playerID, System.Action<bool, string> callback)
    {
        string url = apiURL + "/api/awakening/players/" + playerID + "/coins/add";

        WWWForm form = new WWWForm();

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                callback(true, "");
            }
            else
            {
                if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
                {
                    callback(false, www.error);
                }
                else
                {
                    callback(false, www.error);
                }
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

    public IEnumerator CreateGameMatch(string endpoint, string jsonData, System.Action<bool, string> callback)
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

    public IEnumerator UpdatePlayerRecord(string endpoint, string jsonData, System.Action<bool, string> callback)
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

    public IEnumerator DeletePlayerInventory(string playerId, System.Action<bool, string> callback)
    {
        string endpoint = playerId + "/deck";

        using (UnityWebRequest www = UnityWebRequest.Delete(apiURL + "/api/awakening/players/"  + endpoint))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                callback(true, " ");
            }
            else
            {
                callback(false, www.error);
            }
        }
    }

    public IEnumerator GetCardIdsForPlayer(string playerId, Action<List<int>> callback)
    {
        UnityWebRequest www = UnityWebRequest.Get(apiURL + "/api/awakening/players/" + playerId + "/deck");
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


    public IEnumerator getPlayerInventory(string playerId, Action<List<int>> callback)
    {
        UnityWebRequest www = UnityWebRequest.Get(apiURL + "/api/awakening/players/" + playerId + "/inventory");
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

   public IEnumerator BuyCard(int userId, Action<Card, string, int> onCardReceived)
    {
        string endpoint = "/api/awakening/players/" + userId + "/inventory/buyCard";
        using (UnityWebRequest www = UnityWebRequest.Put(apiURL + endpoint, ""))
        {
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                CardResponse response = JsonUtility.FromJson<CardResponse>(www.downloadHandler.text);
                Debug.LogError($"Failed to buy card for user {userId}: {response.message}");
                onCardReceived?.Invoke(null, response.message, 0); // Assuming error is a string and coins default to 0
            }
            else
            {
                Debug.Log("Response received successfully for user " + userId);
                CardResponse response = JsonUtility.FromJson<CardResponse>(www.downloadHandler.text);
                if (response.card != null)
                {
                    response.card.desbloqueada = true;
                    onCardReceived?.Invoke(response.card, response.message, response.coins.coins);
                }
                else
                {
                    Debug.LogError("No card data found in the response.");
                    int coinsAmount = (response.coins != null) ? response.coins.coins : 0;
                    onCardReceived?.Invoke(null, response.message, coinsAmount);
                }
            }
        }
    }

    public IEnumerator GetCoins(int playerId, Action<int> callback)
    {
        UnityWebRequest www = UnityWebRequest.Get(apiURL + "/api/awakening/players/" + playerId + "/inventory/coins");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Failed to fetch coins for player {playerId}: {www.error}");
            callback(0); // Or any other default value or error handling.
        }
        else
        {
            string data = www.downloadHandler.text;
            CoinResponse coins = JsonUtility.FromJson<CoinResponse>(data);
            Debug.Log("Coins: " + coins.coins);
            callback(coins.coins); // Use the callback to return the fetched coins.
        }
    }


}

