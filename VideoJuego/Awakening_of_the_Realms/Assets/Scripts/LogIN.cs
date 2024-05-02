using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
public class LogIN : MonoBehaviour
{
    public TMP_InputField player_name;
    public TMP_InputField password;

    public APIConnection conexion;

    public TextMeshProUGUI mensajes;
    public TextMeshProUGUI mensaje2;


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
                StartCoroutine(ShowMessageForSeconds("Incorrect username or password", 3, mensajes));
                StartCoroutine(ShowMessageForSeconds("Try Again", 3, mensaje2));

            }
        }));
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

    IEnumerator ShowMessageForSeconds(string message, float seconds, TextMeshProUGUI msg)
    {
        msg.text = message;
        yield return new WaitForSeconds(seconds);
        msg.text = "";
    }
}
