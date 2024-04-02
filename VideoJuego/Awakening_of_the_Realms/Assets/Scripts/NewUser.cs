using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class NewUser : MonoBehaviour
{
    public TMP_InputField nombreInput;
    public TMP_Dropdown reinoDropdown;

    public void botonPresionado()
    {
        string nombreUsuario = nombreInput.text;
        string reino = reinoDropdown.options[reinoDropdown.value].text;
        SceneManager.LoadScene("MainScreen");

    }
}


