using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using UnityEngine.EventSystems;
using static SceneController;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }
    enum GameState { Start, PlayerTurn, AITurn, CheckWinConditions, EndTurn }
    GameState currentState;
    public PlayerDeck playerDeck;
    public AIFunction aiFunction;
    public HealthBar playerHealthBar, aiHealthBar;
    public EnergyBar playerEnergyBar, aiEnergyBar;
    private int retrievalCount = 0;
    private string aiCurrentRealm;

    public List<Card> cartasJugadorEnJuego = new List<Card>();
    public List<Card> cartasOponenteEnJuego = new List<Card>();
    //public Transform playerCardArea1, playerCardArea2, aiCardArea1, aiCardArea2;
    bool playerCardsRetrieved = false;
    bool aiCardsRetrieved = false;
    public static int turnCount;
    public TextMeshProUGUI turnCounterText;
    public TextMeshProUGUI battleLog;

    int attackTotalPlayer = 0, defenseTotalPlayer = 0, healingTotalPlayer = 0;
    int attackTotalAI = 0, defenseTotalAI = 0, healingTotalAI = 0;

    bool ignorePlayerDefense = false;
    bool ignoreAIDefense = false;

    public TextMeshProUGUI nombre;

    public enum GameOutcome { Win, Lose }
    public static GameOutcome gameOutcome;
    public CardRetrievalManager cardRetrievalManager;
     // Effects management for player and AI
    public Dictionary<string, int> playerEffects = new Dictionary<string, int>();
    public Dictionary<string, int> aiEffects = new Dictionary<string, int>();

    public static string gameScene;
    public EffectText playerEffectText;
    public EffectText aiEffectText;



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
            gameScene = SceneManager.GetActiveScene().name;
            Destroy(gameObject);
            return;
        }
        if (FindObjectOfType<EventSystem>() == null)
        {
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
            DontDestroyOnLoad(eventSystem);
        }
        else
        {
            // If an EventSystem already exists, log and consider whether you need to handle this case differently
            Debug.Log("An Event System already exists in the scene.");
        }
    }

    void Start()
    {

        nombre.text = Usuario.usuario.user_name;
        aiCurrentRealm = SceneController.Instance.GetCurrentRealm();
        Debug.Log("Current realm: " + aiCurrentRealm);

        if (Usuario.usuario.realm == "Monster")
        {
            playerHealthBar.SetMaxHealth(130);
        }
        else
        {
            playerHealthBar.SetMaxHealth(100);
        }

        if (aiCurrentRealm == "Monster")
        {
            aiHealthBar.SetMaxHealth(130);
        }
        else
        {
            aiHealthBar.SetMaxHealth(100);
        }

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
        // First, simulate adding the card to check if the turn can be ended validly
        cartasJugadorEnJuego.Add(card);
        Debug.Log("Attempting to place card for Player: " + card.card_name);

        if (CanEndTurn()) {
            Debug.Log("Card placed successfully.");
            if (cartasJugadorEnJuego.Count == 2) {
                PlayerEndTurn();
            }
        } else {
            // If the card placement results in an invalid state, remove it
            cartasJugadorEnJuego.Remove(card);
            Debug.Log("Card cannot be placed due to insufficient energy.");
        }
    }

    void HandleAICardPlacement(Card card)
    {
        if (!cartasOponenteEnJuego.Contains(card)) {
            cartasOponenteEnJuego.Add(card);
            Debug.Log("AI placed card: " + card.card_name);
        }
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
                break;
            case GameState.AITurn:
                aiFunction.InitializeAIActions();
                AIEndTurn();
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

    bool CanEndTurn()
    {
        int totalLegendaryCost = cartasJugadorEnJuego.Where(card => card.rarity == "Legendary").Sum(card => card.power_cost);
        if (cartasJugadorEnJuego.Any(card => card.rarity == "Legendary" && playerEnergyBar.currentEnergy < card.power_cost))
        {
            Debug.Log("Cannot end turn: Insufficient energy to play legendary cards. Adjust your cards.");
            return false;
        }

        if (totalLegendaryCost > playerEnergyBar.currentEnergy)
        {
            Debug.Log("Cannot end turn: Insufficient energy to play all legendary cards. Adjust your cards.");
            return false;
        }

        return true;
    }

    public void PlayerEndTurn()
    {
        if (CanEndTurn()) {
            Debug.Log("Player has ended their turn.");
            SetGameState(GameState.AITurn); // Transition to AI turn
        } else {
            Debug.Log("Turn cannot end due to unresolved issues.");
        }
    }


    void AIEndTurn()
    {
        Debug.Log("AI has ended their turn.");
        SetGameState(GameState.CheckWinConditions); // Proceed to check win conditions or change to the next appropriate state
    }

    private void AIEnergyIncrease()
    {
        Level currentLevel = SceneController.Instance.levelsConfig.levels[SceneController.CurrentLevelIndex];
        // Apply the energy increase factor from the LevelsConfig
        aiEnergyBar.IncrementEnergy(currentLevel.aiEnergyIncrease);

        Debug.Log($"Level loaded: {currentLevel.name}, Energy Increase: {currentLevel.aiEnergyIncrease}");
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
        turnCount++;
        RealizarCombate();
        ApplyTurnBasedEffects();
        AIEnergyIncrease();
        playerEnergyBar.IncrementEnergy(1);
        
        // Debug log to track the energy level after all operations
        Debug.Log($"Player's energy after all operations: {playerEnergyBar.currentEnergy}");
        
        ResetGameState();
    }

    private void ApplyTurnBasedEffects()
    {
        // Every 2 turns, check if any magical effects should be applied
        if (turnCount % 2 == 0)
        {
            if (aiCurrentRealm == "Magical")
            {
                Debug.Log("AI's Magical realm effect active: Steal Energy from Player");
                StealEnergy(playerEnergyBar, aiEnergyBar);  // AI steals energy from player
            }

            if (Usuario.usuario.realm == "Magical")
            {
                Debug.Log("Player's Magical realm effect active: Steal Energy from AI");
                StealEnergy(aiEnergyBar, playerEnergyBar);  // Player steals energy from AI
            }
        }
    }

    private void StealEnergy(EnergyBar fromEnergyBar, EnergyBar toEnergyBar)
    {
        Debug.Log("Attempting to steal energy...");
        // Ensure both players have at least 1 energy to perform a steal
        if (fromEnergyBar.currentEnergy > 0 && toEnergyBar.currentEnergy > 0)
        {
            fromEnergyBar.DecrementEnergy(1);
            toEnergyBar.IncrementEnergy(1);
            Debug.Log("Energy stolen successfully.");
        }
        else
        {
            Debug.Log("Energy cannot be stolen due to insufficient energy levels.");
        }
    }



    public void RealizarCombate()
    {

        // Process player cards
        foreach (var card in cartasJugadorEnJuego)
        {
            attackTotalPlayer += card.attack;
            defenseTotalPlayer += card.defense;
            healingTotalPlayer += card.healing;

            Debug.Log($"Player card: {card.card_name} Attack: {card.attack} Defense: {card.defense} Healing: {card.healing}");

            if (card.rarity == "Legendary")
            {
                Debug.Log($"Applying Legendary player card effect: {card.Effect_type}");
                playerEffectText.ShowEffect(card.Effect_type);
                ApplyCardEffect(card.Effect_type, aiEffects, playerEffects, true);
                Debug.Log("Decreasing Energy");
                playerEnergyBar.DecrementEnergy(card.power_cost);
                Debug.Log($"Energy cost: {card.power_cost}");
            }
        }
        string playerRealm = Usuario.usuario.realm;
        switch (playerRealm)
        {
            case "Human":
                attackTotalPlayer = (int)(attackTotalPlayer * 1.5);
                Debug.Log($"Human realm effect: {attackTotalPlayer}, card: {cartasJugadorEnJuego[0].card_name}");
                break;
            case "Celestial":
                healingTotalPlayer = (int)(healingTotalPlayer * 1.5);
                Debug.Log($"Celestial realm effect: {healingTotalPlayer}, card: {cartasJugadorEnJuego[0].card_name}");
                break;
        }

        // Process AI cards
        foreach (var card in cartasOponenteEnJuego)
        {
            attackTotalAI += card.attack;
            defenseTotalAI += card.defense;
            healingTotalAI += card.healing;

            Debug.Log($"AI card: {card.card_name} Attack: {card.attack} Defense: {card.defense} Healing: {card.healing}");

            if (card.rarity == "Legendary")
            {
                Debug.Log($"Applying Legendary AI card effect: {card.Effect_type}");
                aiEffectText.ShowEffect(card.Effect_type);
                ApplyCardEffect(card.Effect_type, playerEffects, aiEffects, false);
                Debug.Log("Decreasing Energy");
                Debug.Log($"Energy cost: {card.power_cost}");
            }
        }

        switch (aiCurrentRealm)
        {
            case "Human":
                attackTotalAI = (int)(attackTotalAI * 1.5);
                Debug.Log($"Human realm effect: {attackTotalAI}, card: {cartasOponenteEnJuego[0].card_name}");
                break;
            case "Celestial":
                healingTotalAI = (int)(healingTotalAI * 1.5);
                Debug.Log($"Celestial realm effect: {healingTotalAI}, card: {cartasOponenteEnJuego[0].card_name}");
                break;
        }

        // Initialize effect application (assuming some effects are pre-existing)
        Debug.Log("Effects being applied...");
        Debug.Log("Player effects: " + string.Join(", ", playerEffects.Keys));
        Debug.Log("AI effects: " + string.Join(", ", aiEffects.Keys));
        ApplyEffects(playerEffects, aiEffects);

        Debug.Log("Player attack: " + attackTotalPlayer + " Defense: " + defenseTotalPlayer + " Healing: " + healingTotalPlayer);
        Debug.Log("AI attack: " + attackTotalAI + " Defense: " + defenseTotalAI + " Healing: " + healingTotalAI);

        // Calculate and apply damages
        int damageToAI = ignoreAIDefense ? attackTotalPlayer : Math.Max(0, attackTotalPlayer - defenseTotalAI);
        int damageToPlayer = ignorePlayerDefense ? attackTotalAI : Math.Max(0, attackTotalAI - defenseTotalPlayer);

        // Update game statistics
        GameStats.TotalDamageDealt += damageToAI + damageToPlayer;
        GameStats.TotalHealthCured += healingTotalPlayer + healingTotalAI;
        GameStats.TotalDefenseMitigated += defenseTotalPlayer + defenseTotalAI;

        aiHealthBar.TakeDamage(damageToAI);
        aiHealthBar.Heal(healingTotalAI);
        
        playerHealthBar.TakeDamage(damageToPlayer);
        playerHealthBar.Heal(healingTotalPlayer);
        
        Debug.Log($"AI health bar: {aiHealthBar.currentHealth} Player health bar: {playerHealthBar.currentHealth}");
        battleLog.text = $"Player damage: {damageToAI} Healing: {healingTotalPlayer} Defense: {defenseTotalPlayer} \nEnemy damage: {damageToPlayer} Healing: {healingTotalAI} Defense: {defenseTotalAI}";

        attackTotalPlayer = 0;
        defenseTotalPlayer = 0;
        healingTotalPlayer = 0;
        attackTotalAI = 0;
        defenseTotalAI = 0;
        healingTotalAI = 0;
        ignorePlayerDefense = false;
        ignoreAIDefense = false;

    }

    private void ApplyCardEffect(string effectType, Dictionary<string, int> targetEffects, Dictionary<string, int> selfEffects, bool isPlayer)
    {
        Debug.Log($"Applying card effect: {effectType} (IsPlayer: {isPlayer})");
        switch (effectType)
        {
            case "Princess Blessing":
                selfEffects["HealingDoubling"] = 1; // Doubles healing for 1 round
                targetEffects["EnergyReduction"] = 1; // Reduces two energy levels immediately
                Debug.Log($"Healing doubling applied to self for 1 turn. Energy reduction by 2 applied to opponent.");
                break;
            case "King Arthur wrath":
                targetEffects["IgnoreDefense"] = 1; // Ignores defense for 1 round
                Debug.Log("Defense ignored for 1 round.");
                break;
            case "Skyfall":
                selfEffects["DodgeAttack"] = 1; // Can dodge one attack
                Debug.Log("Dodge attack effect applied for 1 round.");
                break;
            case "Hell Fire":
                targetEffects["DotDamage"] = 3; // Applies dot damage of 10
                targetEffects["HealingReduction"] = 3; // Reduces healing by 50%
                Debug.Log("Dot damage of 10 applied for 3 rounds. Healing reduction by 50% for 3 rounds.");
                break;
            case "Fortress":
                selfEffects["DefenseBarrier"] = 2; // Creates a barrier adding 50 defense for 2 rounds
                Debug.Log("Defense barrier added for 2 rounds.");
                break;
            case "Shadow Strike":
                targetEffects["AttackWeakening"] = 2; // Makes attacks 20% weaker for 2 rounds
                selfEffects["LifeSteal"] = 1; // Life steal effect of 30 points
                Debug.Log("Attack weakened by 20% for 2 rounds. Life steal effect of 30 points applied.");
                break;
            case "Mighty Soldier":
                selfEffects["ReflectDamage"] = 1; // Reflects all damage taken for 1 round
                selfEffects["HealOverTime"] = 3; // Heals 10 life points for 3 rounds
                Debug.Log("Reflect damage effect applied for 1 round. Heal over time effect of 10 points for 3 rounds.");
                break;
            case "Demon Curse":
                selfEffects["DoubleDamage"] = 1; // Doubles the damage for 1 round
                targetEffects["CurseDamage"] = 2; // 10 damage over time
                targetEffects["HealingReduction"] = 2; // 20% healing reduction
                Debug.Log("Damage doubled for 1 round. Curse damage of 10 for 2 rounds. Healing reduction by 20% for 2 rounds.");
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
                healingTotalPlayer = (int)(healingTotalPlayer * 1.25);
                Debug.Log("Player healing 50% more effective.");
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
                playerHealthBar.TakeDamage(15);
                Debug.Log("Player took 10 damage over time.");
                break;
            case "HealingReduction":
                healingTotalPlayer = healingTotalPlayer * (100 - 50) / 100;
                Debug.Log("Player healing reduced by 50%.");
                break;
            case "DefenseBarrier":
                defenseTotalPlayer += 100;
                Debug.Log("Player defense barrier added.");
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
                playerHealthBar.TakeDamage(20);
                Debug.Log("Player cursed and took 20 damage.");
                break;
        }
    }

    private void ApplyAIEffect(string effect, Dictionary<string, int> effects)
    {
        Debug.Log("Applying effect: " + effect);
        switch (effect)
        {
            case "HealingDoubling":
                healingTotalAI = (int)(healingTotalAI * 1.25);
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
                aiHealthBar.TakeDamage(15);
                Debug.Log("AI took 10 damage over time.");
                break;
            case "HealingReduction":
                healingTotalAI = healingTotalAI * (100 - 50) / 100;
                Debug.Log("AI healing reduced by 50%.");
                break;
            case "DefenseBarrier":
                defenseTotalAI += 100;
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
                aiHealthBar.TakeDamage(20);
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
            
            SetGameState(GameState.PlayerTurn); // Loop back to player turn
            Debug.Log("[GameAI] Game state reset completed.");
            retrievalCount = 0;
            EffectsDebug.TriggerTurnEnd();

            if (turnCounterText != null)
                turnCounterText.text = $"Turn: {turnCount}";
            
            if (playerHealthBar.currentHealth <= 0 || aiHealthBar.currentHealth <= 0)
            {
                if (playerHealthBar.currentHealth <= 0) {
                    gameOutcome = GameOutcome.Lose;
                } else {
                    gameOutcome = GameOutcome.Win;
                }

                GameOver(gameOutcome);
            }
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

    public void Surrender() {

        gameOutcome = GameOutcome.Lose;
        GameOver(gameOutcome);
    }
    void GameOver(GameOutcome outcome)
    {
        gameOutcome = outcome;
        Debug.Log(outcome == GameOutcome.Lose ? "AI Wins!" : "Player Wins!");
        CleanupGame(); // Handle cleanup
        LoadGameOverScene();
    }

    void LoadGameOverScene()
    {
        if (SceneManager.sceneCount > 1)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.UnloadSceneAsync(currentScene);
        }
        else
        {
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }
    }

    // Method to handle the cleanup process
    private void CleanupGame()
    {
        // Clear lists and destroy game objects if necessary
        //cartasJugadorEnJuego.ForEach(card => Destroy(card.gameObject));
        cartasJugadorEnJuego.Clear();

        //cartasOponenteEnJuego.ForEach(card => Destroy(card.gameObject));
        cartasOponenteEnJuego.Clear();

        attackTotalPlayer = defenseTotalPlayer = healingTotalPlayer = 0;
        attackTotalAI = defenseTotalAI = healingTotalAI = 0;
        ignorePlayerDefense = ignoreAIDefense = false;
        if (Instance != null)
        {

            Destroy(Instance.gameObject);  // Destroy the singleton game object
            Instance = null;  // Nullify the static reference to ensure clean re-instantiation later
        }
        
        if (SceneController.Instance != null)
        {
            SceneController.Instance.CleanupController();
        }

    }
}