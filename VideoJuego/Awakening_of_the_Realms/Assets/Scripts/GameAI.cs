using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public HealthAndShield healthAndShield;

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

    void RealizarCombate()
    {
        // Implement combat logic as previously defined
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
