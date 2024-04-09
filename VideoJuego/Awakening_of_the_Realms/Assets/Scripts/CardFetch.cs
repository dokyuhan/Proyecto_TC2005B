using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CardFetch : MonoBehaviour
{
    [SerializeField] private string apiURL = "localhost:3200";
    [SerializeField] private string cardEndpoint = "/api/awakening/cards/";
    private int cardId = 1;
    Card card;

    public List<Card> cards = new List<Card>(); 
    /* delegate y event son clases que permiten utilizar los metodos como objetos en #C, en donde el delegate define una firma (OnCardsFetched) y recibe el tipo de parametro (en este caso seria la Lista) y regresa un void
    Hay muchas maneras de uso del delegate y event, pero para este caso se esta utilizando para la visualizacion de las cartas especificas para cada mazo. Se utiliza la clase de delegate porque para otros casos (dentro del juego)
    solo se necesitaria visualizar las cartas del deck y no todo el inventario. En resumen estas clases son utilizadas para mandar los datos especificos para cada mazo al igual de tener un mejor mantenimiento */
    public delegate void OnCardsFetched(List<Card> cards);
    public static event OnCardsFetched CardsFetched;

    private void Start()
    {
        StartCoroutine(FetchCards());
    }

    IEnumerator FetchCards()
    {
        for (int i = 1; i <= 40; i++)
        {
            yield return StartCoroutine(GetCard(i));
        }
        
        /* CardsFetched proviene del evento OnCardsFetched, que este evento en si contiene argumentos especificos (en este caso seria la List<Card> cards),
        porque se pasan parametros especificos, cuando este evento es llamado en otra parte agrega dentro del evento el metodo que se desea correr o implementar (un ejemplo seria en el script del inventario en donde se programa un codigo 
        utilizando la clase de CardFetch el evento CardsFetched y un operador que suma (+=) la funcion de  DisplayAllCards encontrada en inventario.) Con esos parametros recibidos se ejecuta los metodos del evento (en este caso se estaria ejecutando
        la visualizacion de las cartas que la funcion mando, analiznado la funcion DisplayAllCards sabemos que se desea implementar una visualizacion de todas las cartas. En resumen el evento guardaria cardDisplayManager.DisplayCards(card) de cada carta que se mando en la lista de cartas.)
        La clase Invoke sirve para encapsular exepciones, esto se refiere a que si cuando un evento no recibe ningun metodo o valor en lugar de mandar el error de un null, no se realize nada y no mande ningun output  */
        CardsFetched?.Invoke(cards);
    }

    IEnumerator GetCard(int id)
    {
        UnityWebRequest www = UnityWebRequest.Get($"{apiURL}{cardEndpoint}{id}");

        Debug.Log("URL: " + apiURL + cardEndpoint + cardId);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
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
        }

        else
        {
            Debug.Log($"Request failed: {www.error}");
        }
    }
}