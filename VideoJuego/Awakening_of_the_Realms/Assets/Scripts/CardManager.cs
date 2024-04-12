using System.Collections;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public void MoveCard(GameObject cardObject, string tag)
    {
        Transform targetLocation = GameObject.FindGameObjectWithTag(tag)?.transform;
        if (targetLocation != null)
        {
            StartCoroutine(MoveToPosition(cardObject, targetLocation));
        }
        else
        {
            Debug.LogError("No se encontró ningún objeto con el tag: " + tag);
        }
    }

    private IEnumerator MoveToPosition(GameObject cardObject, Transform target)
    {
        float timeToMove = 1.0f;
        Vector3 originalPosition = cardObject.transform.position;
        float elapsedTime = 0;

        while (elapsedTime < timeToMove)
        {
            cardObject.transform.position = Vector3.Lerp(originalPosition, target.position, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cardObject.transform.position = target.position;
    }
}

