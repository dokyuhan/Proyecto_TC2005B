//script para creación de cartas en el inventario
//script para creación de cartas
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Inventario : MonoBehaviour
{
    [SerializeField] string apiURL = "localhost:3200"; // Updated to match Express.js server
    [SerializeField] string cardEndpoint = "/api/awakening/cards/"; // Endpoint adjusted
    int cardId = 1;

    Card card;


    public CardDisplayManager cardDisplayManager;
    public List<Card> cards = new List<Card>();

    // Assuming Card class is correctly structured to match the JSON response from Express.js
    // Make sure this class matches the server's response structure, especially the naming

    IEnumerator GetCard()
    {
        // We create a UnityWebRequest object and make a GET request to the API
        // We use the cardEndpoint and cardId to fetch the card data
        UnityWebRequest www = UnityWebRequest.Get(apiURL + cardEndpoint + cardId);

        Debug.Log("URL: " + apiURL + cardEndpoint + cardId);

        yield return www.SendWebRequest();

        // If the request fails, we log the error
        if(www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log($"Request failed: {www.error}");
        }
        else 
        {
            Debug.Log("se conecto");
            // If the request is successful, we parse the JSON data and store it in the card object
            // The response of the request is stored in the downloadHandler property of the UnityWebRequest object
            string data = www.downloadHandler.text;

            // Using the JsonUtility class, we can parse the JSON data and store it in the card object
            // It is important to note that the JSON data must match the structure of the Card class
            card = JsonUtility.FromJson<Card>(data);

            card.desbloqueada = true;

            cards.Add(card);

            cardId++;




            // Using the card object we can display the card data in the UI
            /*id.text = card.card_ID.ToString();
            nombre.text = card.card_name;
            descripcion.text = card.card_description;
            ataque.text = card.attack.ToString();
            defensa.text = card.defense.ToString();
            curacion.text = card.healing.ToString();
            reino.text = card.card_realm;
            poderCosto.text = card.power_cost.ToString();
            expCosto.text = card.exp_cost.ToString();
            rareza.text = card.rarity;
            nivel.text = card.card_level.ToString();
            efecto.text = card.Effect_type;*/


            //cardImage.sprite = Resources.Load<Sprite>($"Spells/{card.card_id}");
            
            //Debug.Log($"Name: {card.card_name} Description: {card.card_description} Cost: {card.card_cost}");
        }
    }


    IEnumerator Start()
    {
        for (int i = 1; i <= 40; i++)
        {
            // Espera a que GetCard termine antes de continuar
            yield return StartCoroutine(GetCard());
        }

        foreach (Card card in cards)
        {
            cardDisplayManager.DisplayCards(card);
        }
    }


    public void Back()
    {
        SceneManager.LoadScene("MainScreen");
    }

    public void Save()
    {
        //aqui se debe hacer el post de la lista mazo;
    }
}


