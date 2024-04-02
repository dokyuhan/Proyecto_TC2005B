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
}