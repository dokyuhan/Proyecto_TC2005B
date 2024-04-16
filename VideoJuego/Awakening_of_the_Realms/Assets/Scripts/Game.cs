
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Game : MonoBehaviour
{

    public static Game Instance { get; private set; }

    public CardDisplayManager cardDisplayManagerJugador; 
    public CardDisplayManager cardDisplayManagerAI; 

    public APIConnection conexion;
    public List<int> lista = new List<int>();
    int [] aiLista = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

    public List<Card> cartasJugadorEnJuego = new List<Card>();
    public List<Card> cartasOponenteEnJuego = new List<Card>();

    public CardManager cardManager;
    public GameObject cardGameObject;

    public TimerPrueba timer;

    public int turnos = 0;

    public HealthAndShield healthAndShield;



    public void Start()
    {
        //La linea comentada solo sirve si se inicia sesion
        //StartCoroutine(conexion.GetCardIdsForPlayer(Usuario.usuario.player_ID, ProcessCardIds));
        StartCoroutine(conexion.GetCardIdsForPlayer("2", ProcessCardIds));

        timer.OnCountdownFinished += CheckForCombat; // Suscripción al evento
        timer.StartCountdown(); 

    }


    public void ProcessCardIds(List<int> cardIds)
    {
        foreach (int cardId in cardIds)
        {
            lista.Add(cardId);
        }
    }


    IEnumerator next()
    {
        foreach (int id in lista)
        {
            yield return StartCoroutine(conexion.GetCards(id, playerMazo.player));
        }

        foreach (Card card in playerMazo.player)
        {
            cardDisplayManagerJugador.DisplayCards(card);
        }
    }

    IEnumerator hola()
    {
        foreach (int id in aiLista)
        {
            yield return StartCoroutine(conexion.GetCards(id, AIMazo.AI));
        }

        foreach (Card card in AIMazo.AI)
        {
            cardDisplayManagerAI.DisplayCards(card);
        }
    }

    public void OnClick(){

        StartCoroutine(next());
        StartCoroutine(hola());

    }


    void OnEnable()
    {
        Arrastrar.OnCardPlacedInPlayZone += AddCardToPlayerZone;
        Arrastrar.OnCardPlacedInOpponentZone += AddCardToOpponentZone;
    }

    void OnDisable()
    {
        Arrastrar.OnCardPlacedInPlayZone -= AddCardToPlayerZone;
        Arrastrar.OnCardPlacedInOpponentZone -= AddCardToOpponentZone;
    }

    void AddCardToPlayerZone(Card card)
    {
        if (!cartasJugadorEnJuego.Contains(card))
        {
            cartasJugadorEnJuego.Add(card);
            Debug.Log("Carta agregada a la zona de juego del jugador");
        }
    }

    void AddCardToOpponentZone(Card card)
    {
        if (!cartasOponenteEnJuego.Contains(card))
        {
            cartasOponenteEnJuego.Add(card);
            Debug.Log("Carta agregada a la zona de juego del oponente");
        }
    }

    void CheckForCombat()
    {
        // Verifica si hay suficientes cartas en juego para iniciar el combate
        if (cartasJugadorEnJuego.Count >= 2 && cartasOponenteEnJuego.Count >= 2)
        {
            RealizarCombate();
        }
    }

    public void RealizarCombate()
    {
        int ataqueTotalJugador = 0, defensaTotalJugador = 0, curacionTotalJugador = 0;
        int ataqueTotalRival = 0, defensaTotalRival = 0, curacionTotalRival = 0;

        // Asegúrate de que sólo accedes a los índices disponibles
        foreach (var card in cartasJugadorEnJuego)
        {
            ataqueTotalJugador += card.attack;
            defensaTotalJugador += card.defense;
            curacionTotalJugador += card.healing;
        }

        foreach (var card in cartasOponenteEnJuego)
        {
            ataqueTotalRival += card.attack;
            defensaTotalRival += card.defense;
            curacionTotalRival += card.healing;
        }

        // Ejecuta la lógica de combate, como comparar ataque con defensa, aplicar curación, etc.
        int dañoAJugador = Math.Max(0, ataqueTotalRival - defensaTotalJugador);
        int dañoARival = Math.Max(0, ataqueTotalJugador - defensaTotalRival);

        // Usar la referencia para aplicar daño y curación
        if (healthAndShield != null) // Verifica que la referencia no sea nula
        {
            healthAndShield.TakeDamage(dañoAJugador);
            healthAndShield.Heal(curacionTotalJugador);
        }

        Debug.LogError(dañoAJugador);
        Debug.LogError(dañoARival);

        // Aplicar curaciones o cualquier otra lógica adicional

        // Devuelve todas las cartas a sus posiciones iniciales o al contenedor de cartas
        MoveAllCardsToTag("usedJugador", cartasJugadorEnJuego);
        MoveAllCardsToTag("usedAI", cartasOponenteEnJuego);

        // Limpia la lista
        cartasJugadorEnJuego.Clear();
        cartasOponenteEnJuego.Clear();

        turnos++;

        if(turnos == 2){
            MoveAllCardsToTag("gameUser", playerMazo.player);
            MoveAllCardsToTag("gameAI",  AIMazo.AI);
            turnos = 0;
        }
                
        timer.StartCountdown(); 

    }



    public void MoveAllCardsToTag(string tag, List<Card> lista)
    {
        foreach (Card card in lista)
        {
            if (card.cardGameObject != null) // Verifica que el GameObject esté asignado
            {
                cardManager.MoveCard(card.cardGameObject, tag);
            }
            else
            {
                Debug.LogError("El GameObject de la carta no está asignado.");
            }
        }
    }







}

