using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour {
    public int levelScenes;
    public void SelectGameLevel(int levelIndex) {
        PlayerPrefs.SetInt("SelectedLevel", levelIndex);
        PlayerPrefs.Save();
        // Check if the level index is within the range of defined scenes
        SceneManager.LoadScene("GameLevel");
    }

    public void Back() {
        SceneManager.LoadScene("MainScreen");
    }
}
