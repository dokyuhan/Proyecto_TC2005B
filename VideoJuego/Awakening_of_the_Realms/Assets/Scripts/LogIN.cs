using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LogIN : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField password;


    public void botonPresionado()
    {
        SceneManager.LoadScene("MainScreen");

    }
}


