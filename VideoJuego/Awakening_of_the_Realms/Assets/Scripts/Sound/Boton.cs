using UnityEngine;
using UnityEngine.UI; // Necesario para interactuar con los botones de la UI.

public class Boton : MonoBehaviour
{
    public AudioSource audioSource; // Asignar en el Inspector, asegúrate de que cada botón tiene su propio AudioSource

    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(PlaySound);
    }

    public void PlaySound()
    {
        if (audioSource.isPlaying)
            audioSource.Stop(); // Detener el sonido actual si ya se está reproduciendo

        audioSource.Play(); // Reproducir el sonido
    }
}
