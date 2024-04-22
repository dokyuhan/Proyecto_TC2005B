using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void LogIn()
    {
        SceneManager.LoadScene("LogIn");
    }

    public void NewUser()
    {
        SceneManager.LoadScene("NewUser");
    }

    public void Quit()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

}
