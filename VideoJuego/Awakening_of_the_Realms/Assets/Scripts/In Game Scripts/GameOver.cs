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

        int level = SceneController.CurrentLevelIndex + 1;
        string idUsuario = Usuario.usuario.player_ID;
        int AI = 1;
        string scene = Game.gameScene;

        int gameDuration = 3600;
        int gameTurns = Game.turnCount;

        if(level == 1 || level == 2){scene = "Human";}
        else if(level == 3 || level == 4){scene = "Monster";}
        else if(level == 5 || level == 6){scene = "Celestial";}
        else if(level == 7 || level == 8){scene = "Magical";}



        if (Game.gameOutcome == Game.GameOutcome.Win){
            messageText.text = "Congratulations! You won!";
            coins.text = " + 450 coins";
            MusicManager.PlayWinMusic();
            StartCoroutine(conexion.AddCoins(Usuario.usuario.player_ID, HandleCoinsAdded));

            string jsonData = $"{{\"game\": {{\"player_ID_1\": \"{idUsuario}\", \"player_ID_2\": \"{AI}\", \"winner_ID\": \"{idUsuario}\", \"game_level\": \"{level}\", \"game_scene\": \"{scene}\", \"game_duration\": {gameDuration}, \"game_turns\": {gameTurns}}}}}";
            StartCoroutine(conexion.CreateGameMatch("/api/awakening/match/create", jsonData, HandleGameCreationResponse));

            StartCoroutine(conexion.UpdatePlayerRecord("/api/players/updateRecord/" + idUsuario + "/1", "{}", HandleUpdateResponse));


        } else {
            messageText.text = "Game Over. You lost.";
            coins.text = " + 0 coins";
            MusicManager.PlayLoseMusic();
            string jsonData = $"{{\"game\": {{\"player_ID_1\": \"{idUsuario}\", \"player_ID_2\": \"{AI}\", \"winner_ID\": \"{AI}\", \"game_level\": \"{level}\", \"game_scene\": \"{scene}\", \"game_duration\": {gameDuration}, \"game_turns\": {gameTurns}}}}}";
            StartCoroutine(conexion.CreateGameMatch("/api/awakening/match/create", jsonData, HandleGameCreationResponse));

            StartCoroutine(conexion.UpdatePlayerRecord("/api/players/updateRecord/" + idUsuario + "/2", "{}", HandleUpdateResponse));



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

    public void Continue()
    {
        // Call to destroy the MusicManager
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.DestroyMusicManager();
        }

        // Scene management to unload or load scenes
        if (SceneManager.sceneCount > 1)
        {
            SceneManager.UnloadSceneAsync("GameOver");
        }
        else
        {
            SceneManager.LoadScene("MainScreen", LoadSceneMode.Single);
        }
    }


    private void HandleGameCreationResponse(bool success, string response)
    {
        if (success)
        {
            Debug.Log("Game created successfully: " + response);
        }
        else
        {
            Debug.LogError("Failed to create game: " + response);
        }
    }


    private void HandleUpdateResponse(bool success, string response)
    {
        if (success)
        {
            Debug.Log("Record update successful: " + response);
        }
        else
        {
            Debug.LogError("Failed to update record: " + response);
        }
    }

    
}

