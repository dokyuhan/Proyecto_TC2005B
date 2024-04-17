using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using System.Linq;

public class Inventario : MonoBehaviour
{   
    public static Inventario Instance { get; private set; }

    public CardDisplayManager cardDisplayManager; 
    public APIConnection conexion;

    public List<Card> available = new List<Card>();
    public List<int> playerInv = new List<int>();


    IEnumerator Start()
    {

        Cards.cards.Clear();
        available.Clear();
        playerInv.Clear();

        yield return StartCoroutine(conexion.getPlayerInvnetory(Usuario.usuario.player_ID, ProcessCardIds));
        // Obtener todas las cartas
        for (int i = 1; i <= 40; i++)
        {
            yield return StartCoroutine(conexion.GetCards(i, Cards.cards));
        }

        for (int f = 1; f <= playerInv.Count; f++)
        {        
            yield return StartCoroutine(conexion.GetCards(f, available));
        }
        // Actualizar el estado de desbloqueo de cada carta
        foreach (Card card in Cards.cards)
        {
            card.desbloqueada = available.Any(a => a.card_ID == card.card_ID);  // Comprobar si la carta está en 'available'
            cardDisplayManager.DisplayCards(card);
        }

        // Limpiar el mazo actual
        ControladorDeMazo.cartasEnMazo.Clear();
    }




    public void Back()
    {
        SceneManager.LoadScene("MainScreen");
    }

    public void ProcessCardIds(List<int> cardIds)
    {
        foreach (int cardId in cardIds)
        {
            Debug.Log(cardId);
            playerInv.Add(cardId);

        }
        
    }

    public void IniciarSave()
    {
        StartCoroutine(Save());
    }


    public IEnumerator Save()
    {
        yield return StartCoroutine(conexion.DeletePlayerInventory(Usuario.usuario.player_ID, AlwaysAddCardsCallback));

    }

    private void AddCards()
    {
        CardsContainer cardsContainer = new CardsContainer();
        foreach (Card carta in ControladorDeMazo.cartasEnMazo)
        {
            cardsContainer.cards.Add(new CardData(carta.card_ID, Usuario.usuario.player_ID, 1));
        }


        string jsonData = JsonUtility.ToJson(cardsContainer);
        StartCoroutine(conexion.AddCardsToDeck("/api/awakening/players/deck", jsonData, CallbackDeResultado));
    }

    private void CallbackDeResultado(bool exito, string respuesta)
    {
        if (exito)
        {
            Debug.Log("Las cartas se agregaron correctamente: " + respuesta);
            SceneManager.LoadScene("MainScreen");

        }
        else
        {
            Debug.LogError("Error al agregar cartas: " + respuesta);
        }
    }

    private void AlwaysAddCardsCallback(bool success, string response)
    {
        if (success)
        {
            Debug.Log("Inventario eliminado correctamente: " + response);
            AddCards();
        }
        else
        {
            Debug.Log("Error al eliminar inventario: " + response);
            AddCards();

        }
    }


        

}
