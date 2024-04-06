//script para creación de cartas en el inventario
//script para creación de cartas
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class Inventario : MonoBehaviour
{
    [SerializeField] string apiURL = "localhost:3200"; // Updated to match Express.js server
    [SerializeField] string cardEndpoint = "/api/awakening/cards/"; // Endpoint adjusted
    int cardId = 1;

    Card card;

    public GameObject cardPrefab; 
    public Transform inv;

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

        // Una vez completado todo, muestra las cartas
        DisplayCards();
    }

    void DisplayCards()
    {
        foreach (Card card in cards)
        {
            GameObject objeto = Instantiate(cardPrefab, inv);

            Arrastrar arrastrarScript = objeto.GetComponent<Arrastrar>();

            if (arrastrarScript != null)
            {
                arrastrarScript.cartaArrastrar = card; // Asigna la carta actual
            }

            //Image imagenCarta = objeto.transform.Find("background").GetComponent<Image>(); 

            Image imagenCarta = objeto.transform.Find("background").GetComponent<Image>(); 
            Sprite cardSprite = Resources.Load<Sprite>("Cards/" + card.card_name);
            if (cardSprite != null)
            {
                imagenCarta.sprite = cardSprite;
            }
            else
            {
                Debug.LogWarning("No se encontro la imagen para: " + card.card_name);
            }
            
            Image imagenMarco = objeto.transform.Find("marco").GetComponent<Image>(); 
            Sprite frameSprite = Resources.Load<Sprite>("Frames/" + card.card_realm);
            if (frameSprite != null)
            {
                imagenMarco.sprite = frameSprite;
            }
            else
            {
                Debug.LogWarning("No se encontro el marco para: " + card.card_realm);
            }

            TextMeshProUGUI energia = objeto.transform.Find("Energy").GetComponent<TextMeshProUGUI>();
            if (energia != null)
            {
                energia.text = card.power_cost.ToString();
            }

            TextMeshProUGUI ataque = objeto.transform.Find("Attack").GetComponent<TextMeshProUGUI>();
            if (ataque != null)
            {
                ataque.text = card.attack.ToString();
            }

            TextMeshProUGUI defensa = objeto.transform.Find("Defense").GetComponent<TextMeshProUGUI>();
            if (defensa != null)
            {
                defensa.text = card.defense.ToString();
            }

            TextMeshProUGUI heal = objeto.transform.Find("Healing").GetComponent<TextMeshProUGUI>();
            if (heal != null)
            {
                heal.text = card.healing.ToString();
            }

            TextMeshProUGUI descripcion = objeto.transform.Find("Descript").GetComponent<TextMeshProUGUI>();
            if (descripcion != null)
            {
                descripcion.text = card.card_description.ToString();
            }

            TextMeshProUGUI nombre = objeto.transform.Find("Nombre").GetComponent<TextMeshProUGUI>();
            if (nombre != null)
            {
                nombre.text = card.card_name.ToString();
            }


            

            if (!card.desbloqueada)
            {
                imagenCarta.color = new Color(imagenCarta.color.r, imagenCarta.color.g, imagenCarta.color.b, 0.7f); 
                imagenMarco.color = new Color(imagenMarco.color.r, imagenMarco.color.g, imagenMarco.color.b, 0.7f); 
            }
        }
    }
}



public class Controll : MonoBehaviour
{

}
