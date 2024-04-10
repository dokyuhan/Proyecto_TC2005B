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

    public IEnumerator GetCardIdsForPlayer(int playerId, Action<List<int>> callback)
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





}

