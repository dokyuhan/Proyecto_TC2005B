using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LogIN : MonoBehaviour
{
    public TMP_InputField player_name;
    public TMP_InputField password;

    public APIConnection conexion;

    public static User Usuario;

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
                /* Parse JSON response and create User object
                User loggedInUser = JsonUtility.FromJson<User>(response);
                // Assign loggedInUser to static variable
                Usuario = loggedInUser;
                // Load next scene or do other actions
                SceneManager.LoadScene("NextScene");*/
            }
            else
            {
                Debug.LogError("Login failed: " + response);
            }
        }));
    }
}
