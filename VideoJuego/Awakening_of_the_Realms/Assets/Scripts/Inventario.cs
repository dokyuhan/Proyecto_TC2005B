using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class Inventario : MonoBehaviour
{   
    /* Inventario es declarado como una Instancia que propiedades de un public getter y un private setter
    Esta instancia es utilizada para implementar un singleton design pattern, que se refiere a que el inventario es unico para todo el juego */
    public static Inventario Instance { get; private set; }

    public CardDisplayManager cardDisplayManager; 
    public APIConnection conexion;
    

    /* Funcion privada que utiliza Awake (inicializa cualquiera condicion antes de iniciar el juego)
    Dentro de la funcion se esta inicializando condiciones para verificar si el inventario ya existe o no, si existe lo elimina, si no lo mantiene */
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            CardFetch.CardsFetched += DisplayInvCards;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Funcion que corre cuando el obejto es destruido
    private void OnDestroy()
    {
        CardFetch.CardsFetched -= DisplayInvCards;
    }

    public void DisplayInvCards(List<Card> cards)
    {
        foreach (Card card in cards)
        {
            cardDisplayManager.DisplayCards(card);
        }
    }


    public void Back()
    {
        //SceneManager.LoadScene("MainScreen");

        StartCoroutine(conexion.GetCardIdsForPlayer(1, ProcessCardIds));


    }

    public void ProcessCardIds(List<int> cardIds)
    {
        // Este código ahora se ejecutará después de que se hayan recibido y procesado los cardIds.
        foreach (int cardId in cardIds)
        {
            Debug.Log(cardId);
        }
        
        // Si quieres cambiar de escena después de procesar los IDs, coloca la llamada aquí.
        // SceneManager.LoadScene("MainScreen");
    }

    public void IniciarSave()
    {
        StartCoroutine(Save());
    }



    public IEnumerator Save()
    {
        CardsContainer cardsContainer = new CardsContainer();

        foreach (Card carta in ControladorDeMazo.cartasEnMazo)
        {
            cardsContainer.cards.Add(new CardData(carta.card_ID, Usuario.usuario.player_ID, 1));
        }

        string jsonData = JsonUtility.ToJson(cardsContainer);

        // Asume que conexion es una instancia de la clase que tiene AddCardsToDeck y que esta clase hereda de MonoBehaviour.
        yield return StartCoroutine(conexion.AddCardsToDeck("/api/awakening/inventory/deck", jsonData, CallbackDeResultado));
    }

    private void CallbackDeResultado(bool exito, string respuesta)
    {
        if (exito)
        {
            Debug.Log("Las cartas se agregaron correctamente: " + respuesta);
        }
        else
        {
            Debug.LogError("Error al agregar cartas: " + respuesta);
        }
    }


        

}
