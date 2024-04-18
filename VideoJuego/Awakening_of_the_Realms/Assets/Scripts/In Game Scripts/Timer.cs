using System.Collections;
using UnityEngine;
using TMPro;
using System; // Incluir para usar el evento EventHandler

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public int countdownTime = 30;

    public event Action OnCountdownFinished; // Evento para notificar que el tiempo se acabó

    private float currentTime;

    void Start()
    {
        currentTime = countdownTime;
        StartCountdown(); // Inicia el conteo inicial
    }

    public void StartCountdown()
    {
        currentTime = countdownTime;
        StartCoroutine(CountdownRoutine());
    }

    private IEnumerator CountdownRoutine()
    {
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            countdownText.text = Mathf.CeilToInt(currentTime).ToString();
            yield return null;
        }
        countdownText.text = "0";
        OnCountdownFinished?.Invoke(); // Llama al evento cuando el contador llegue a 0
    }
}
