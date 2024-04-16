using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour {

    public string[] levelScenes; // Array to store the names of the scenes for each level

    public void SelectGameLevel(int levelIndex) {
        // Check if the level index is within the range of defined scenes
        if (levelIndex >= 0 && levelIndex < levelScenes.Length) {
            // Load the scene corresponding to the selected level
            SceneManager.LoadScene(levelScenes[levelIndex]);
        } else {
            Debug.LogError("Invalid level index or scene not configured");
        }
    }

    public void Back() {
        SceneManager.LoadScene("MainScreen");
    }
}
