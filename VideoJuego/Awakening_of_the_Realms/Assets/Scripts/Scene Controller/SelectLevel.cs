using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class SelectLevel : MonoBehaviour {
    public int levelScenes;
    public TextMeshProUGUI locked;
    public void SelectGameLevel(int levelIndex) {
        if (Usuario.usuario.level - 1 < levelIndex) {
            StartCoroutine(ShowMessageForSeconds("Level Locked! You must complete all previous levels before!", 5));
            Debug.Log("Level locked");
            return;
        }
        PlayerPrefs.SetInt("SelectedLevel", levelIndex);
        PlayerPrefs.Save();
        // Check if the level index is within the range of defined scenes
        SceneManager.LoadScene("GameLevel");
    }

    IEnumerator ShowMessageForSeconds(string message, float seconds)
    {
        locked.text = message;
        yield return new WaitForSeconds(seconds);
        locked.text = "";
    }

    public void Back() {
        SceneManager.LoadScene("MainScreen");
    }
}
