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

    public static User newUser; // Objeto User estático para ser accesible globalmente

    public void botonPresionado()
    {
        // Asigna los valores recogidos del formulario al objeto User
        newUser = new User()
        {
            player_name = player_name.text,
            player_last_name = player_last_name.text,
            player_age = int.Parse(player_age.text),
            user_name = user_name.text,
            password = password.text,
            // Nota: 'realm' se asignará en RealmInfo
        };

        // Cambia a la escena 'RealmInfo' para continuar el proceso
        SceneManager.LoadScene("RealmInfo");
    }

    public void GoBack()
    {
        SceneManager.LoadScene("Menu");
    }
}
