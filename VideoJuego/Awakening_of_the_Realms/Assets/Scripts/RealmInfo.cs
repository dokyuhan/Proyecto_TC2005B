using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class RealmInfo : MonoBehaviour
{
    public TMP_Dropdown realmDropdown;
    public APIConnection apiConnection;

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
            Debug.Log("Usuario añadido con éxito");
            SceneManager.LoadScene("LogIn");
        }
        else
        {
            Debug.LogError($"Error al añadir usuario: {error}");
        }
    }
}
