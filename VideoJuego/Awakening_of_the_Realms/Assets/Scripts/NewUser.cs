using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NewUser : MonoBehaviour
{
    public TMP_InputField player_name;
    public TMP_InputField player_last_name;
    public TMP_InputField player_age;
    public TMP_InputField user_name;
    public TMP_InputField password;


    public void botonPresionado()
    {
        if (Usuario.usuario == null)
        {
            Usuario.usuario = new User();
        }
        
        Usuario.usuario.player_name = player_name.text;
        Usuario.usuario.player_last_name = player_last_name.text;
        Usuario.usuario.player_age = int.Parse(player_age.text); // Asegúrate de manejar posibles excepciones aquí
        Usuario.usuario.user_name = user_name.text;
        Usuario.usuario.password = password.text;

        // Cambia a la escena 'RealmInfo' para continuar el proceso
        SceneManager.LoadScene("RealmInfo");
    }

    public void GoBack()
    {
        SceneManager.LoadScene("Menu");
    }
}
