using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI countdownText; // For TextMeshPro. If using legacy UI, change the type to "public Text text;"
    public int countdownTime = 30; // Starting time in seconds

    private float currentTime;

    void Start()
    {
        currentTime = countdownTime;
    }

    void Update()
    {
        // Decrease currentTime by the elapsed time between frames
        currentTime -= Time.deltaTime;
        // Update the UI text
        countdownText.text = Mathf.CeilToInt(currentTime).ToString();

        // Optional: Do something when the countdown reaches 0
        if (currentTime <= 0)
        {
            // Stop the countdown
            currentTime = 0;
            // Perform an action, like ending the game or starting a new level
            // Debug.Log("Countdown Finished!");
        }
    }
}
