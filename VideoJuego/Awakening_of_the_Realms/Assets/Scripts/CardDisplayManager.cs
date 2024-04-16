using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CardDisplayManager : MonoBehaviour
{

    [SerializeField] private GameObject cardPrefab; 
    [SerializeField] private Transform inv;

    public void DisplayCards(Card card)
    {
        Debug.Log("Displaying card: " + card.card_name);
        GameObject objeto = Instantiate(cardPrefab, inv);
        card.cardGameObject = objeto;
        
        if (SceneManager.GetActiveScene().name == "Shop")
        {
            RectTransform rectTransform = objeto.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(0, 0);
        }

        SetupCardDisplay(objeto, card);
    }


    private void AdjustCardPosition(GameObject cardObject)
    {
        cardObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }

    public void ClearCardsUI()
    {
        foreach (Transform child in inv)
        {
            Destroy(child.gameObject);
        }
    }

    public void SetupCardDisplay(GameObject objeto, Card card)
    {
        Arrastrar arrastrarScript = objeto.GetComponent<Arrastrar>();

        if (arrastrarScript != null)
        {
            arrastrarScript.cartaArrastrar = card;
        }

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