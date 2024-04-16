using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreen : MonoBehaviour
{
    public void LevelSelect()
    {
        SceneManager.LoadScene("SelectLevel");
    }

    public void Deck()
    {
        SceneManager.LoadScene("DeckBuilding");
    }

    public void Shop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void LogOut()
    {
        SceneManager.LoadScene("Menu");
    }
}