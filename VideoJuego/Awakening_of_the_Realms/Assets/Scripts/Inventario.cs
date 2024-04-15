using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class Inventario : MonoBehaviour
{   
    public static Inventario Instance { get; private set; }

    public CardDisplayManager cardDisplayManager; 
    public APIConnection conexion;

    IEnumerator Start()
    {
        for (int i = 1; i <= 40; i++)
        {
            yield return StartCoroutine(conexion.GetCards(i, Cards.cards));
        }

        foreach (Card card in Cards.cards)
        {
            cardDisplayManager.DisplayCards(card);
        }
    }



    public void Back()
    {
        SceneManager.LoadScene("MainScreen");

        StartCoroutine(conexion.GetCardIdsForPlayer(Usuario.usuario.player_ID, ProcessCardIds));
        Debug.Log("hola");

    }

    public void ProcessCardIds(List<int> cardIds)
    {
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
        yield return StartCoroutine(conexion.AddCardsToDeck("/api/awakening/players/inventory/deck", jsonData, CallbackDeResultado));
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
