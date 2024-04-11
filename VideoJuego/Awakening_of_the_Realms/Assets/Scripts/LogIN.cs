using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LogIN : MonoBehaviour
{
    public TMP_InputField player_name;
    public TMP_InputField password;

    public APIConnection conexion;

    public void OnLoginButtonClick()
    {
        string userName = player_name.text;
        string userPassword = password.text;

        string jsonData = "{\"user_name\": \"" + userName + "\", \"password\": \"" + userPassword + "\"}";

        StartCoroutine(conexion.LogIn("/api/awakening/players/login", jsonData, (success, response) =>
        {
            if (success)
            {
                Debug.Log("Login successful!");
                User loggedInUser = JsonUtility.FromJson<User>(response);
                Usuario.usuario = loggedInUser;
                Debug.LogError(Usuario.usuario.player_ID);

                SceneManager.LoadScene("MainScreen");


            }
            else
            {
                Debug.LogError("Login failed: " + response);
            }
        }));
    }
}
