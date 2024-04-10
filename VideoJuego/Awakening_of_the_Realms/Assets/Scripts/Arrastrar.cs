using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Arrastrar : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Card cartaArrastrar;
    private Vector2 lastMousePosition;
    private Transform padre;

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
    }


    void Start()
    {
        padre = transform.parent;

    }

}
