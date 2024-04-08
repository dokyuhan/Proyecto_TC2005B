using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class Arrastrar : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Card cartaArrastrar;
    private Vector2 lastMousePosition;
    private Transform padre;
    public List<Card> mazo = new List<Card>();


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
            mazo.Add(cartaArrastrar);
        }
        else
        {
            transform.SetParent(padre, false);
            transform.localPosition = Vector3.zero;
            if (mazo.Exists(carta => carta.card_ID == cartaArrastrar.card_ID))
            {
                mazo.RemoveAll(carta => carta.card_ID == cartaArrastrar.card_ID);
            }
            
        }
    }

    void Start()
    {
        padre = transform.parent;
    }
}
