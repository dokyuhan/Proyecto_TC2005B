using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }
    enum GameState { Start, PlayerTurn, AITurn, CheckWinConditions, EndTurn }
    GameState currentState;
    public PlayerDeck playerDeck;
    public AIFunction aiFunction;
    public Timer timer;
    public HealthBar playerHealthBar, aiHealthBar;
    public EnergyBar playerEnergyBar, aiEnergyBar;
    private int retrievalCount = 0;
    public CardManager cardManager;
    public List<Card> cartasJugadorEnJuego = new List<Card>();
    public List<Card> cartasOponenteEnJuego = new List<Card>();
    public Transform playerCardArea1, playerCardArea2, aiCardArea1, aiCardArea2;
    bool playerCardsRetrieved = false;
    bool aiCardsRetrieved = false;
    public int turnCount;
    public TextMeshProUGUI turnCounterText;


    void Awake()
    {
        Debug.Log($"Awake called on GameAI with instance ID: {GetInstanceID()} in scene: {SceneManager.GetActiveScene().name}");
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Arrastrar.OnCardPlacedInOpponentZone += HandleAICardPlacement;
            Arrastrar.OnCardPlacedInPlayZone += HandlePlayerCardPlacement;

        }
        else
        {
            Debug.Log($"Awake called on GameAI with instance ID: {GetInstanceID()} in scene: {SceneManager.GetActiveScene().name}");
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        playerHealthBar.SetMaxHealth(100);
        aiHealthBar.SetMaxHealth(100);
        playerEnergyBar.ResetEnergy();
        aiEnergyBar.ResetEnergy();
        turnCount = 0;
        turnCounterText.text = $"Turn: {turnCount}";
        
        SetGameState(GameState.PlayerTurn);
    }

    void OnDestroy()
    {
        Arrastrar.OnCardPlacedInOpponentZone -= HandleAICardPlacement;
        Arrastrar.OnCardPlacedInPlayZone -= HandlePlayerCardPlacement;
    }

    void HandlePlayerCardPlacement(Card card)
    {
        if (!cartasJugadorEnJuego.Contains(card)) {
            cartasJugadorEnJuego.Add(card);
            Debug.Log("Carta agregada a la zona de juego del jugador");
        }
    }

    void HandleAICardPlacement(Card card)
    {
        if (!cartasOponenteEnJuego.Contains(card)) {
            cartasOponenteEnJuego.Add(card);
            Debug.Log("Carta agregada a la zona de juego del oponente");
        }
    }

    public void ClearCardsUI(Transform cardArea)
    {
        foreach (Transform child in cardArea)
        {
            Destroy(child.gameObject);
        }
        Debug.Log("Cleared all card UI elements from " + cardArea.name);
    }


    private void SetGameState(GameState newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case GameState.Start:
                StartGame();
                break;
            case GameState.PlayerTurn:
                // Player's turn to play cards
                break;
            case GameState.AITurn:
                aiFunction.InitializeAIActions();
                SetGameState(GameState.CheckWinConditions);
                break;
            case GameState.CheckWinConditions:
                CheckActionsCompleted();
                break;
            case GameState.EndTurn:
                EndTurn();
                break;
        }
    }

    void StartGame()
    {
        Debug.Log("Game started.");
        SetGameState(GameState.PlayerTurn);
    }

    public void OnEndTurnButtonPressed()
    {
        Debug.Log("Player has ended their turn.");
        SetGameState(GameState.AITurn); // Transition to AI turn
    }

    void CheckActionsCompleted()
    {
        if (cartasJugadorEnJuego.Count >= 2 && cartasOponenteEnJuego.Count >= 2)
        {
            SetGameState(GameState.EndTurn);
        }
    }

    void EndTurn()
    {
        Debug.Log("[GameAI] Ending turn, starting combat and resetting game state.");
        playerEnergyBar.IncrementEnergy(1); 
        aiEnergyBar.IncrementEnergy(1);
        RealizarCombate();
        ResetGameState();
    }


    public void RealizarCombate()
    {
        int attackTotalPlayer = 0, defenseTotalPlayer = 0, healingTotalPlayer = 0;
        int attackTotalAI = 0, defenseTotalAI = 0, healingTotalAI = 0;

        // Aggregate stats from player cards
        foreach (var card in cartasJugadorEnJuego)
        {
            attackTotalPlayer += card.attack;
            defenseTotalPlayer += card.defense;
            healingTotalPlayer += card.healing;
        }

        // Aggregate stats from AI cards
        foreach (var card in cartasOponenteEnJuego)
        {
            attackTotalAI += card.attack;
            defenseTotalAI += card.defense;
            healingTotalAI += card.healing;
        }
    
        int damageToAI = Math.Max(0, attackTotalPlayer - defenseTotalAI);
        int damageToPlayer = Math.Max(0, attackTotalAI - defenseTotalPlayer);

        aiHealthBar.TakeDamage(damageToAI);
        aiHealthBar.Heal(healingTotalPlayer); // Assuming player can heal AI as a strategy (?)
    
        playerHealthBar.TakeDamage(damageToPlayer);
        playerHealthBar.Heal(healingTotalAI); // Assuming AI can heal player as a strategy (?)
        Debug.Log("Damage to Player: " + damageToPlayer);
        Debug.Log("Damage to AI: " + damageToAI);
        Debug.Log("Player Healing: " + healingTotalAI);
        Debug.Log("AI Healing: " + healingTotalPlayer);
    }

    // Reset game state modification
    public void ResetGameState()
    {
        
        retrievalCount = 0;
        Action onCardsRetrieved = () =>
        {

            ClearCardsUI(playerCardArea1);
            ClearCardsUI(playerCardArea2);
            ClearCardsUI(aiCardArea1);
            ClearCardsUI(aiCardArea2);
            // Actions to take after cards are successfully retrieved
            cartasJugadorEnJuego.Clear();
            cartasOponenteEnJuego.Clear();
            
            timer.StartCountdown(); // Restart the timer
            SetGameState(GameState.PlayerTurn); // Loop back to player turn
            Debug.Log("[GameAI] Game state reset completed.");
            retrievalCount = 0;
            turnCount++;

            if (turnCounterText != null)
                turnCounterText.text = $"Turn: {turnCount}";
        };

        RetrieveCardsFromPlay(onCardsRetrieved);
        
    }

    
    public void RetrieveCardsFromPlay(Action onComplete)
    {
        StartCoroutine(RetrieveCards(cartasJugadorEnJuego, playerDeck.handDeck, 2.0f, () => CheckAllRetrievals(onComplete))); // Player's cards
        StartCoroutine(RetrieveCards(cartasOponenteEnJuego, aiFunction.aiScript.handDeck, 2.0f, () => CheckAllRetrievals(onComplete))); // AI's cards
    }

    private void CheckAllRetrievals(Action onComplete)
    {
        retrievalCount++;
        if (retrievalCount == 1) {
            playerCardsRetrieved = true;
        } else if (retrievalCount == 2) {
            aiCardsRetrieved = true;
        }

        if (playerCardsRetrieved && aiCardsRetrieved)
        {
            onComplete?.Invoke();
            // Reset flags for next time
            playerCardsRetrieved = false;
            aiCardsRetrieved = false;
            retrievalCount = 0; 
        }
    }

    // Generalized coroutine to handle card retrieval
    private IEnumerator RetrieveCards(List<Card> cards, HandDeck deck, float delay, Action onComplete)
    {
        yield return new WaitForSeconds(delay);

        foreach (Card card in cards)
        {
            if (deck.displayedCards.Contains(card))
            {
                deck.displayedCards.Remove(card);
                Destroy(card.cardGameObject);
            }
            else
            {
                Debug.LogWarning($"[GameAI] Card {card.card_name} NOT found in display. Current displayed cards:");
                foreach (Card displayedCard in deck.displayedCards)
                {
                    Debug.Log($"Displayed Card: {displayedCard.card_name}");
                }
            }
        }

        if (deck.displayedCards.Count < 5)
        {
            deck.RefillDisplayedCards();
        }

        cards.Clear(); // Clear the original list
        onComplete?.Invoke();
    }

}
