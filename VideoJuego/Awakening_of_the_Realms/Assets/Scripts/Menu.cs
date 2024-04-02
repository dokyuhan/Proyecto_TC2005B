using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("MainScreen");
    }

    public void NewUser()
    {
        SceneManager.LoadScene("RealmInfo");
    }
}
