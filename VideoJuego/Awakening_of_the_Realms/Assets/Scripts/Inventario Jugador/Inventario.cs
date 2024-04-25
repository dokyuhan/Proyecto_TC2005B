using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using System.Linq;
using TMPro;

public class Inventario : MonoBehaviour
{   
    public static Inventario Instance { get; private set; }

    public CardDisplayManager cardDisplayManager; 
    public APIConnection conexion;
    public List<Card> available = new List<Card>();
    public List<int> playerInv = new List<int>();

    public TextMeshProUGUI mensajes;

    IEnumerator Start()
    {

        Cards.cards.Clear();
        available.Clear();
        playerInv.Clear();

        yield return StartCoroutine(conexion.getPlayerInventory(Usuario.usuario.player_ID, ProcessCardIds));
        // Obtener todas las cartas
        for (int i = 1; i <= 40; i++)
        {
            yield return StartCoroutine(conexion.GetCards(i, Cards.cards));
        }

        for (int f = 0; f < playerInv.Count; f++)
        {        
            yield return conexion.GetCards(playerInv[f], available);
        }

        List<Card> unlockedCards = new List<Card>();
        List<Card> lockedCards = new List<Card>();

        // Actualizar el estado de desbloqueo de cada carta
        foreach (Card card in Cards.cards)
        {
            if (available.Any(a => a.card_ID == card.card_ID))
            {
                card.desbloqueada = true;
                unlockedCards.Add(card); // Add to unlocked list if it's available
            }
            else
            {
                card.desbloqueada = false;
                lockedCards.Add(card); // Otherwise, add to locked list
            }
        }

        // Display all unlocked cards first
        foreach (Card unlocked in unlockedCards)
        {
            cardDisplayManager.DisplayCards(unlocked);
        }

        // Followed by all locked cards
        foreach (Card locked in lockedCards)
        {
            cardDisplayManager.DisplayCards(locked);
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

    IEnumerator ShowMessageForSeconds(string message, float seconds)
    {
        mensajes.text = message;
        yield return new WaitForSeconds(seconds);
        mensajes.text = "";
    }

    public void IniciarSave()
    {

        int sum = 0;

        foreach (Card carta in ControladorDeMazo.cartasEnMazo)
        {
            if(carta.rarity == "Legendary"){
                sum = sum +1;
            }
        }

        if ((ControladorDeMazo.cartasEnMazo.Count == 10) && (sum <= 2)){
            StartCoroutine(Save());
        }else if(ControladorDeMazo.cartasEnMazo.Count != 10){
            StartCoroutine(ShowMessageForSeconds("Tu mazo debe contener 10 cartas.", 4));
        }else if(sum > 2){
            StartCoroutine(ShowMessageForSeconds("Tu mazo no puede tener mas de 2 cartas legendarias.", 4));
        }
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
