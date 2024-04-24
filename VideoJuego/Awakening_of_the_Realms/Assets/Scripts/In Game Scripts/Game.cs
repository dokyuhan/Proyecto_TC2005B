using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

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
    //public CardManager cardManager;
    public List<Card> cartasJugadorEnJuego = new List<Card>();
    public List<Card> cartasOponenteEnJuego = new List<Card>();
    //public Transform playerCardArea1, playerCardArea2, aiCardArea1, aiCardArea2;
    bool playerCardsRetrieved = false;
    bool aiCardsRetrieved = false;
    public int turnCount;
    public TextMeshProUGUI turnCounterText;

    int attackTotalPlayer = 0, defenseTotalPlayer = 0, healingTotalPlayer = 0;
    int attackTotalAI = 0, defenseTotalAI = 0, healingTotalAI = 0;

    bool ignorePlayerDefense = false;
    bool ignoreAIDefense = false;

    public TextMeshProUGUI nombre;

    public enum GameOutcome { Win, Lose }
    public static GameOutcome gameOutcome;
    public CardRetrievalManager cardRetrievalManager;



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
        nombre.text = Usuario.usuario.user_name;
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

    /*
    public void ClearCardsUI(Transform cardArea)
    {
        foreach (Transform child in cardArea)
        {
            Destroy(child.gameObject);
        }
        Debug.Log("Cleared all card UI elements from " + cardArea.name);
    }
    */


    private void SetGameState(GameState newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case GameState.Start:
                StartGame();
                break;
            case GameState.PlayerTurn:
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

        // Effects management for player and AI
        Dictionary<string, int> playerEffects = new Dictionary<string, int>();
        Dictionary<string, int> aiEffects = new Dictionary<string, int>();

        // Process player cards
        foreach (var card in cartasJugadorEnJuego)
        {
            attackTotalPlayer += card.attack;
            defenseTotalPlayer += card.defense;
            healingTotalPlayer += card.healing;
            Debug.Log($"Player card processed: {card.card_name}, Attack: {card.attack}, Defense: {card.defense}, Healing: {card.healing}");

            if (card.rarity == "Legendary")
            {
                Debug.Log($"Applying Legendary player card effect: {card.Effect_type}");
                ApplyCardEffect(card.Effect_type, aiEffects, playerEffects, true);
            }
        }

        // Process AI cards
        foreach (var card in cartasOponenteEnJuego)
        {
            attackTotalAI += card.attack;
            defenseTotalAI += card.defense;
            healingTotalAI += card.healing;
            Debug.Log($"AI card processed: {card.card_name}, Attack: {card.attack}, Defense: {card.defense}, Healing: {card.healing}");

            if (card.rarity == "Legendary")
            {
                Debug.Log($"Applying Legendary AI card effect: {card.Effect_type}");
                ApplyCardEffect(card.Effect_type, playerEffects, aiEffects, false);
            }
        }

        // Initialize effect application (assuming some effects are pre-existing)
        Debug.Log("Effects being applied...");
        ApplyEffects(playerEffects, aiEffects);

        // Calculate and apply damages
        int damageToAI = ignoreAIDefense ? attackTotalPlayer : Math.Max(0, attackTotalPlayer - defenseTotalAI);
        int damageToPlayer = ignorePlayerDefense ? attackTotalAI : Math.Max(0, attackTotalAI - defenseTotalPlayer);

        // Update game statistics
        GameStats.TotalDamageDealt += damageToAI + damageToPlayer;
        GameStats.TotalHealthCured += healingTotalPlayer + healingTotalAI;
        GameStats.TotalDefenseMitigated += defenseTotalPlayer + defenseTotalAI;

        aiHealthBar.TakeDamage(damageToAI);
        aiHealthBar.Heal(healingTotalPlayer);
        
        playerHealthBar.TakeDamage(damageToPlayer);
        playerHealthBar.Heal(healingTotalAI);

        Debug.Log($"Combat results - Damage to Player: {damageToPlayer}, Damage to AI: {damageToAI}, Player Healing: {healingTotalAI}, AI Healing: {healingTotalPlayer}");

        attackTotalPlayer = 0;
        defenseTotalPlayer = 0;
        healingTotalPlayer = 0;
        attackTotalAI = 0;
        defenseTotalAI = 0;
        healingTotalAI = 0;
        ignorePlayerDefense = false;
        ignoreAIDefense = false;

        if (playerHealthBar.currentHealth <= 0 || aiHealthBar.currentHealth <= 0)
        {
            if (playerHealthBar.currentHealth <= 0) {
                Debug.Log("AI Wins!");
                gameOutcome = GameOutcome.Lose;
            } else {
                Debug.Log("Player Wins!");
                gameOutcome = GameOutcome.Win;
            }

            SceneManager.LoadScene("GameOver");
        }

    }

    private void ApplyCardEffect(string effectType, Dictionary<string, int> targetEffects, Dictionary<string, int> selfEffects, bool isPlayer)
    {
        Debug.Log($"Applying card effect: {effectType} (IsPlayer: {isPlayer})");
        switch (effectType)
        {
            case "Effect 1":
                selfEffects["HealingDoubling"] = 1; // Doubles healing for 1 round
                targetEffects["EnergyReduction"] = 1; // Reduces two energy levels immediately
                break;
            case "Effect 2":
                targetEffects["IgnoreDefense"] = 1; // Ignores defense for 1 round
                break;
            case "Effect 3":
                selfEffects["DodgeAttack"] = 1; // Can dodge one attack
                break;
            case "Effect 4":
                targetEffects["DotDamage"] = 3; // Applies dot damage of 10
                targetEffects["HealingReduction"] = 3; // Reduces healing by 50%
                break;
            case "Effect 5":
                selfEffects["DefenseBarrier"] = 2; // Creates a barrier adding 50 defense for 2 rounds
                break;
            case "Effect 6":
                targetEffects["AttackWeakening"] = 2; // Makes attacks 20% weaker for 2 rounds
                selfEffects["LifeSteal"] = 1; // Life steal effect of 30 points
                break;
            case "Effect 7":
                selfEffects["ReflectDamage"] = 1; // Reflects all damage taken for 1 round
                selfEffects["HealOverTime"] = 3; // Heals 10 life points for 3 rounds
                break;
            case "Effect 8":
                selfEffects["DoubleDamage"] = 1; // Doubles the damage for 1 round
                targetEffects["CurseDamage"] = 2; // 10 damage over time
                targetEffects["HealingReduction"] = 2; // 20% healing reduction
                break;
        }
    }

    private void ApplyEffects(Dictionary<string, int> playerEffects, Dictionary<string, int> aiEffects)
    {
        // Process player effects
        foreach (var effect in new List<string>(playerEffects.Keys))
        {
            Debug.Log("Processing player effect: " + effect);
            ApplyPlayerEffect(effect, playerEffects);  // Apply each effect

            // Decrement and check the effect's duration after application
            if (--playerEffects[effect] <= 0)
            {
                Debug.Log("Removing expired player effect: " + effect);
                playerEffects.Remove(effect);  // Remove expired effects
            }
        }

        // Process AI effects similarly
        foreach (var effect in new List<string>(aiEffects.Keys))
        {
            Debug.Log("Processing AI effect: " + effect);
            ApplyAIEffect(effect, aiEffects);  // Apply each effect

            // Decrement and check the effect's duration after application
            if (--aiEffects[effect] <= 0)
            {
                Debug.Log("Removing expired AI effect: " + effect);
                aiEffects.Remove(effect);  // Remove expired effects
            }
        }
    }

    private void ApplyPlayerEffect(string effect, Dictionary<string, int> effects)
    {
        Debug.Log("Applying effect: " + effect);
        switch (effect)
        {
            case "HealingDoubling":
                healingTotalPlayer *= 2;
                Debug.Log("Player healing doubled.");
                break;
            case "EnergyReduction":
                playerEnergyBar.DecrementEnergy(2);
                Debug.Log("Player energy reduced by 2.");
                break;
            case "IgnoreDefense":
                ignorePlayerDefense = true;
                Debug.Log("Player defense ignored.");
                break;
            case "DodgeAttack":
                attackTotalAI = 0;
                Debug.Log("Player dodged attack.");
                break;
            case "DotDamage":
                playerHealthBar.TakeDamage(10);
                Debug.Log("Player took 10 damage over time.");
                break;
            case "HealingReduction":
                healingTotalPlayer = healingTotalPlayer * (100 - 50) / 100;
                Debug.Log("Player healing reduced by 50%.");
                break;
            case "DefenseBarrier":
                defenseTotalPlayer += 50;
                break;
            case "AttackWeakening":
                attackTotalAI = (int)(attackTotalAI * (100 - 20) / 100f);
                Debug.Log("Player attack weakened by 20%.");
                break;
            case "LifeSteal":
                playerHealthBar.Heal(30);
                aiHealthBar.TakeDamage(30);
                Debug.Log("Player life steal effect applied.");
                break;
            case "ReflectDamage":
                aiHealthBar.TakeDamage(attackTotalAI);  // Reflects the attack value back to AI
                Debug.Log("Player reflected damage back to AI.");
                break;
            case "HealOverTime":
                playerHealthBar.Heal(10);
                Debug.Log("Player healed 10 points over time.");
                break;
            case "DoubleDamage":
                attackTotalPlayer *= 2;
                Debug.Log("Player damage doubled.");
                break;
            case "CurseDamage":
                playerHealthBar.TakeDamage(10);
                Debug.Log("Player cursed and took 10 damage.");
                break;
        }
    }

    private void ApplyAIEffect(string effect, Dictionary<string, int> effects)
    {
        Debug.Log("Applying effect: " + effect);
        switch (effect)
        {
            case "HealingDoubling":
                healingTotalAI *= 2;
                Debug.Log("AI healing doubled.");
                break;
            case "EnergyReduction":
                aiEnergyBar.DecrementEnergy(2);
                Debug.Log("AI energy reduced by 2.");
                break;
            case "IgnoreDefense":
                ignoreAIDefense = true;
                Debug.Log("AI defense ignored.");
                break;
            case "DodgeAttack":
                attackTotalPlayer = 0;
                Debug.Log("AI dodged attack.");
                break;
            case "DotDamage":
                aiHealthBar.TakeDamage(10);
                Debug.Log("AI took 10 damage over time.");
                break;
            case "HealingReduction":
                healingTotalAI = healingTotalAI * (100 - 50) / 100;
                Debug.Log("AI healing reduced by 50%.");
                break;
            case "DefenseBarrier":
                defenseTotalAI += 50;
                Debug.Log("AI defense barrier added.");
                break;
            case "AttackWeakening":
                attackTotalPlayer = (int)(attackTotalPlayer * (100 - 20) / 100f);
                Debug.Log("AI attack weakened by 20%.");
                break;
            case "LifeSteal":
                aiHealthBar.Heal(30);
                playerHealthBar.TakeDamage(30);
                Debug.Log("AI life steal effect applied.");
                break;
            case "ReflectDamage":
                playerHealthBar.TakeDamage(attackTotalPlayer);
                Debug.Log("AI reflected damage back to player.");
                break;
            case "HealOverTime":
                aiHealthBar.Heal(10);
                Debug.Log("AI healed 10 points over time.");
                break;
            case "DoubleDamage":
                attackTotalAI *= 2;
                Debug.Log("AI damage doubled.");
                break;
            case "CurseDamage":
                aiHealthBar.TakeDamage(10);
                Debug.Log("AI cursed and took 10 damage.");
                break;

        }
    }

    private void ResetGameState()
    {
        retrievalCount = 0;
        System.Action onCardsRetrieved = () =>
        {
            cardRetrievalManager.ClearAllCardsUI();

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

    public void RetrieveCardsFromPlay(System.Action onComplete)
    {
        Debug.Log("Retrieving cards from play...");
        StartCoroutine(cardRetrievalManager.RetrieveCards(cartasJugadorEnJuego, playerDeck.handDeck, 2.0f, () => CheckAllRetrievals(onComplete))); // Player's cards
        StartCoroutine(cardRetrievalManager.RetrieveCards(cartasOponenteEnJuego, aiFunction.aiScript.handDeck, 2.0f, () => CheckAllRetrievals(onComplete))); // AI's cards
    }

    private void CheckAllRetrievals(System.Action onComplete)
    {
        retrievalCount++;
        if (retrievalCount == 1) {
            playerCardsRetrieved = true;
        } else if (retrievalCount == 2) {
            aiCardsRetrieved = true;
        }

        if (playerCardsRetrieved && aiCardsRetrieved)
        {
            onComplete();
            // Reset flags for next time
            playerCardsRetrieved = false;
            aiCardsRetrieved = false;
            retrievalCount = 0; 
        }
    }

    /*
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
    */

    /*
    public void RetrieveCardsFromPlay(Action onComplete)
    {
        Debug.Log("Retrieving cards from play...");
        StartCoroutine(RetrieveCards(cartasJugadorEnJuego, playerDeck.handDeck, 2.0f, () => CheckAllRetrievals(onComplete))); // Player's cards
        Debug.Log("Retrieving AI cards from play...");
        StartCoroutine(RetrieveCards(cartasOponenteEnJuego, aiFunction.aiScript.handDeck, 2.0f, () => CheckAllRetrievals(onComplete))); // AI's cards
    }
    */

    /*
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
        Debug.Log($"Waiting {delay} seconds to retrieve cards...");
        yield return new WaitForSeconds(delay);

        Debug.Log($"Retrieving cards for deck. Total cards to check: {cards.Count}");
        foreach (Card card in cards)
        {
            if (deck.displayedCards.Contains(card))
            {
                Debug.Log($"Removing card {card.card_name} from display.");
                deck.displayedCards.Remove(card);
                Debug.Log($"Destroying GameObject for card {card.card_name}");
                if (card.cardGameObject != null && card.cardGameObject.activeInHierarchy) {
                    Destroy(card.cardGameObject);
                }
            }
            else
            {
                Debug.LogWarning($"[GameAI] Card {card.card_name} NOT found in display. Current displayed cards count: {deck.displayedCards.Count}");
                foreach (Card displayedCard in deck.displayedCards)
                {
                    Debug.Log($"Displayed Card: {displayedCard.card_name}");
                }
            }
        }

        if (deck.displayedCards.Count < 5)
        {
            Debug.Log("Refilling displayed cards due to insufficient count.");
            deck.RefillDisplayedCards();
        }

        cards.Clear();
        onComplete?.Invoke();
    }
    */


    public void Surrender() {

        Debug.Log("AI Wins!");
        gameOutcome = GameOutcome.Lose;
        SceneManager.LoadScene("GameOver");
    }

}