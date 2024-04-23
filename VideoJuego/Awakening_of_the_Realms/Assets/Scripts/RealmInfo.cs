using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class RealmInfo : MonoBehaviour
{
    public TMP_Dropdown realmDropdown;
    public APIConnection apiConnection;

    public TextMeshProUGUI mensajes;

    private void Start()
    {
        if (apiConnection == null)
        {
            apiConnection = FindObjectOfType<APIConnection>();
        }
    }

    public void GoBack()
    {
        SceneManager.LoadScene("NewUser");
    }

    IEnumerator ShowMessageForSeconds(string message, float seconds)
    {
        mensajes.text = message;
        yield return new WaitForSeconds(seconds);
        mensajes.text = "";
        SceneManager.LoadScene("LogIn");

    }

    public void Continue()
    {
        if (Usuario.usuario != null)
        {
            Usuario.usuario.realm = realmDropdown.options[realmDropdown.value].text;

            StartCoroutine(apiConnection.AddUser("/api/awakening/players", JsonUtility.ToJson(Usuario.usuario), HandleAPIResponse));
        }
        else
        {
            Debug.LogError("El usuario no está creado.");
        }
    }

    private void HandleAPIResponse(bool success, string error)
    {
        if (success)
        {
            StartCoroutine(ShowMessageForSeconds("Tu cuenta ha sido creada con exito.", 2));
        }
        else
        {
            Debug.LogError($"Error al añadir usuario: {error}");
        }
    }
}
