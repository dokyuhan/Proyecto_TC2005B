using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("LogIn");
    }

    public void NewUser()
    {
        SceneManager.LoadScene("NewUser");
    }
}
