using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class GameAI : MonoBehaviour
{
    public enum GameState
    {
        PlayerTurn,
        AITurn,
        CheckCombat,
        EndTurn
    }

    public static GameAI Instance { get; private set; }

    public GameState currentState;
    public PlayerDeck playerDeck;
    public AI aiScript;
    public AIFunction aiFunctionScript;
    public CardDisplayManager cardDisplayManagerJugador; 
    public CardDisplayManager cardDisplayManagerAI; 
    public APIConnection conexion;
    public CardManager cardManager;
    public TimerPrueba timer;
    public HealthBar playerHealthBar;
    public HealthBar aiHealthBar;


    public List<int> lista = new List<int>();
    public List<Card> cartasJugadorEnJuego = new List<Card>();
    public List<Card> cartasOponenteEnJuego = new List<Card>();

    private bool playerTurnEnded = false;
    private int turnos = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        playerHealthBar.SetMaxHealth(100);  // Assuming 100 is max health for both
        aiHealthBar.SetMaxHealth(100);
        StartCoroutine(conexion.GetCardIdsForPlayer("2", ProcessCardIds));
        timer.OnCountdownFinished += CheckForCombat;
        timer.StartCountdown();
        InitializeGame();
    }

    void ProcessCardIds(List<int> cardIds)
    {
        foreach (int cardId in cardIds)
        {
            lista.Add(cardId);
        }
    }

    void InitializeGame()
    {
        currentState = GameState.PlayerTurn;
        ProcessTurn();
    }

    void ProcessTurn()
    {
        switch (currentState)
        {
            case GameState.PlayerTurn:
                StartCoroutine(PlayerTurn());
                break;
            case GameState.AITurn:
                if (playerTurnEnded)  // Only start AI turn if player turn has ended
                {
                    StartCoroutine(AITurn());
                }
                break;
            case GameState.CheckCombat:
                CheckForCombat();
                break;
            case GameState.EndTurn:
                EndTurn();
                break;
        }
    }

    IEnumerator PlayerTurn()
    {
        Debug.Log("Player's turn.");
        playerTurnEnded = false;
        yield return new WaitUntil(() => playerTurnEnded); // Wait until player ends the turn
        currentState = GameState.AITurn;
        ProcessTurn();
    }

    IEnumerator AITurn()
    {
        Debug.Log("AI's turn.");
        aiFunctionScript.InitializeAIActions();
        yield return new WaitUntil(() => aiFunctionScript.AITurnComplete());
        currentState = GameState.CheckCombat;
        ProcessTurn();
    }

    void CheckForCombat()
    {
        Debug.Log("Checking combat...");
        if (cartasJugadorEnJuego.Count >= 2 && cartasOponenteEnJuego.Count >= 2)
        {
            RealizarCombate();
        }
        else
        {
            currentState = GameState.EndTurn;
            ProcessTurn();
        }
    }

    public void RealizarCombate()
    {
        int attackTotalPlayer = 0, defenseTotalPlayer = 0, healingTotalPlayer = 0;
        int attackTotalAI = 0, defenseTotalAI = 0, healingTotalAI = 0;

        // Aggregate stats from cards
        foreach (var card in cartasJugadorEnJuego)
        {
            attackTotalPlayer += card.attack;
            defenseTotalPlayer += card.defense;
            healingTotalPlayer += card.healing;
        }

        foreach (var card in cartasOponenteEnJuego)
        {
            attackTotalAI += card.attack;
            defenseTotalAI += card.defense;
            healingTotalAI += card.healing;
        }

        // Combat calculations
        int damageToPlayer = Math.Max(0, attackTotalAI - defenseTotalPlayer);
        int damageToAI = Math.Max(0, attackTotalPlayer - defenseTotalAI);

        // Apply damage and healing
        playerHealthBar.TakeDamage(damageToPlayer);
        playerHealthBar.Heal(healingTotalPlayer);

        aiHealthBar.TakeDamage(damageToAI);
        aiHealthBar.Heal(healingTotalAI);

        // Debug info
        Debug.LogError("Damage to Player: " + damageToPlayer);
        Debug.LogError("Damage to AI: " + damageToAI);

        // Reset game state
        ResetGameState();
    }

    private void ResetGameState()
    {
        // Clear all cards from play to prepare for the next round
        cartasJugadorEnJuego.Clear();
        cartasOponenteEnJuego.Clear();

        // Reset turn counters and prepare the game for the next phase
        turnos++;
        if (turnos == 2)
        {
            // Any specific logic to reset after every two turns
            turnos = 0;
        }

        // Reset the timer to start countdown for the next turn
        timer.StartCountdown();
    }



    void EndTurn()
    {
        Debug.Log("Ending turn and resetting...");
        turnos++;
        if (turnos == 2)
        {
            // Implement logic to manage the reset or transition of cards
            turnos = 0;
        }
        currentState = GameState.PlayerTurn;
        ProcessTurn();
    }

    public void PlayerEndsTurn()
    {
        Debug.Log("Player ends turn.");
        playerTurnEnded = true;
        ProcessTurn();  // Ensure turn processing continues after ending player turn
    }
}
