//script para creación de cartas en el inventario

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controll : MonoBehaviour
{
    public GameObject cardPrefab; 
    public Transform inv;

    private List<Carta> cards = new List<Carta>();

    void Start()
    {
        // Datos dummy para las cartas
        cards.Add(new Carta("Ejemplo2", "Ejemplo2", true));
        cards.Add(new Carta("Ejemplo3", "Ejemplo3", true));
        cards.Add(new Carta("Ejemplo4", "Ejemplo4", false));

        DisplayCards();
    }

    void DisplayCards()
    {
        foreach (Carta card in cards)
        {
            GameObject objeto = Instantiate(cardPrefab, inv);

            Arrastrar arrastrarScript = objeto.GetComponent<Arrastrar>();

            if (arrastrarScript != null)
            {
                arrastrarScript.cartaArrastrar = card; // Asigna la carta actual
            }

            //Image imagenCarta = objeto.transform.Find("background").GetComponent<Image>(); 

            Image imagenCarta = objeto.transform.Find("background").GetComponent<Image>(); 
            Sprite cardSprite = Resources.Load<Sprite>("Cards/" + card.nombre);
            if (cardSprite != null)
            {
                imagenCarta.sprite = cardSprite;
            }
            else
            {
                Debug.LogWarning("No se encontro la imagen para: " + card.nombre);
            }
            
            Image imagenMarco = objeto.transform.Find("marco").GetComponent<Image>(); 
            Sprite frameSprite = Resources.Load<Sprite>("Frames/frame_default");
            if (frameSprite != null)
            {
                imagenMarco.sprite = frameSprite;
            }

            if (!card.desbloqueada)
            {
                imagenCarta.color = new Color(imagenCarta.color.r, imagenCarta.color.g, imagenCarta.color.b, 0.7f); 
                imagenMarco.color = new Color(imagenMarco.color.r, imagenMarco.color.g, imagenMarco.color.b, 0.7f); 
            }
        }
    }
}
