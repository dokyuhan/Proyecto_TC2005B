using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Arrastrar : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Card cartaArrastrar;
    private Vector2 lastMousePosition;
    private Transform padre;

    public delegate void CardEventHandler(Card card);
    public static event CardEventHandler OnCardPlacedInPlayZone;
    public static event CardEventHandler OnCardPlacedInOpponentZone;

    private bool isCardModified = false;
    private Sprite originalSprite; 


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!cartaArrastrar.desbloqueada) return;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!cartaArrastrar.desbloqueada) return;

        RectTransform canvasRectTransform = transform.root.GetComponent<RectTransform>();
        RectTransform cardRectTransform = GetComponent<RectTransform>();
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, eventData.position, eventData.pressEventCamera, out position);

        cardRectTransform.localPosition = position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!cartaArrastrar.desbloqueada) return;

        if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.CompareTag("EspacioMazo"))
        {
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform, false);
            transform.localPosition = Vector3.zero;
            // Añade la carta al mazo si aún no está en la lista, para evitar duplicados.
            if (!ControladorDeMazo.cartasEnMazo.Contains(cartaArrastrar))
            {
                ControladorDeMazo.cartasEnMazo.Add(cartaArrastrar);
            }
            Debug.Log("Total de cartas en el mazo: " + ControladorDeMazo.cartasEnMazo.Count);
        }
        else
        {
            transform.SetParent(padre, false);
            transform.localPosition = Vector3.zero;
            // Remueve la carta del mazo basándose en su ID.
            ControladorDeMazo.cartasEnMazo.RemoveAll(carta => carta.card_ID == cartaArrastrar.card_ID);
        }

        Transform newParent = eventData.pointerCurrentRaycast.gameObject?.transform;

        if (newParent != null && newParent.CompareTag("play"))
        {
            transform.SetParent(newParent, false);
            transform.localPosition = Vector3.zero;
            OnCardPlacedInPlayZone?.Invoke(cartaArrastrar);
        }
        else if (newParent != null && newParent.CompareTag("opponent"))
        {
            transform.SetParent(newParent, false);
            transform.localPosition = Vector3.zero;
            OnCardPlacedInOpponentZone?.Invoke(cartaArrastrar);
        }
    }

    public void InvokeOnCardPlacedInOpponentZone(Card card)
    {
        Debug.Log("Invoking card placement event for: " + card.card_name);
        OnCardPlacedInOpponentZone?.Invoke(card);
        if (card == null) {
            Debug.Log("Card object is null");
            return;
        }
    }


    void Start()
    {
        padre = transform.parent;

        Transform backgroundTransform = transform.Find("background");
        if (backgroundTransform != null)
        {
            Image imagenCarta = backgroundTransform.GetComponent<Image>();
            originalSprite = imagenCarta.sprite;
        }

    }


    public void ToggleCardState()
    {
        Transform backgroundTransform = transform.Find("background");
        if (backgroundTransform != null)
        {
            Image imagenCarta = backgroundTransform.GetComponent<Image>();
            TextMeshProUGUI descripcion = transform.Find("Descript").GetComponent<TextMeshProUGUI>();

            if (imagenCarta == null)
            {
                return;
            }

            if (!isCardModified)
            {
                if (descripcion != null)
                {
                    descripcion.text = cartaArrastrar.card_description.ToString();
                }
                imagenCarta.color = new Color(0, 0, 0, 0.9f);
            }
            else
            {
                descripcion.text = "";
                imagenCarta.sprite = originalSprite;
                imagenCarta.color = new Color(1, 1, 1, 1);


                if (!cartaArrastrar.desbloqueada)
                {
                    imagenCarta.color = new Color(imagenCarta.color.r, imagenCarta.color.g, imagenCarta.color.b, 0.7f); 
                }
            }

            isCardModified = !isCardModified;
        }
        else
        {
            Debug.LogError("No se encontró un objeto hijo llamado 'background'");
        }
    }

}
