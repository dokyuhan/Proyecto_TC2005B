using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI statsText;
    public TextMeshProUGUI coins;
    public APIConnection conexion;

    void Start()
    {

        if (Game.gameOutcome == Game.GameOutcome.Win){
            messageText.text = "Congratulations! You won!";
            coins.text = " + 150 coins";
            StartCoroutine(conexion.AddCoins("21", HandleCoinsAdded));

        }else{
            messageText.text = "Game Over. You lost.";
            coins.text = " + 0 coins";

        }
        // Display game stats
        statsText.text = $"Damage Dealt: {GameStats.TotalDamageDealt}\n" +
                         $"Health Cured: {GameStats.TotalHealthCured}\n" +
                         $"Defense Mitigated: {GameStats.TotalDefenseMitigated}";
    }

    private void HandleCoinsAdded(bool success, string message)
    {
        if (success)
        {
            Debug.Log("Coins successfully added!");
        }
        else
        {
            Debug.LogError("Failed to add coins: " + message);
        }
    }

    public void Continue() {

        SceneManager.LoadScene("MainScreen");
    }

    
}

