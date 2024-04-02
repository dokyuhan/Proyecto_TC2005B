//script para poder arrastrar las cartas

using UnityEngine;
using UnityEngine.EventSystems;

public class Arrastrar : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 lastMousePosition;

    public Carta cartaArrastrar;


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
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRectTransform, eventData.position, eventData.pressEventCamera, out position);

        cardRectTransform.localPosition = position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    void Start()
    {
        lastMousePosition = Input.mousePosition;
    }
}
