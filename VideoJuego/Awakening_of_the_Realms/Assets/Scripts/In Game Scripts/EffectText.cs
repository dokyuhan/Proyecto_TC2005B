using UnityEngine;
using TMPro;
using System.Collections;

public class EffectText : MonoBehaviour
{
    public TextMeshProUGUI effectText;
    public float displayTime;
    public float slideSpeed;    // Speed of the slide effect
    private Vector3 offScreenPositionStart;  // Position where the text starts offscreen
    private Vector3 onScreenPosition;        // Central visible position
    private Vector3 offScreenPositionEnd;    // Position where the text ends offscreen

    void Start()
    {
        // Calculate positions based on the Canvas size
        RectTransform canvasRect = effectText.canvas.GetComponent<RectTransform>();
        float canvasWidth = canvasRect.sizeDelta.x;
        float canvasHeight = canvasRect.sizeDelta.y;

        // Assuming text slides horizontally across the screen:
        offScreenPositionStart = new Vector3(canvasWidth, 0, 0); // Start from left off-screen
        onScreenPosition = new Vector3(0, 0, 0);                 // Center of the screen
        offScreenPositionEnd = new Vector3(-canvasWidth, 0, 0);  // End at right off-screen

        // Set initial position
        effectText.rectTransform.anchoredPosition = offScreenPositionStart;
        effectText.text = "";
    }

    public void ShowEffect(string message)
    {
        effectText.text = message;
        StartCoroutine(SlideTextOnScreen());
    }

    IEnumerator SlideTextOnScreen()
    {
        // Slide from left to center
        while (Vector3.Distance(effectText.rectTransform.anchoredPosition, onScreenPosition) > 0.01f)
        {
            effectText.rectTransform.anchoredPosition = Vector3.MoveTowards(effectText.rectTransform.anchoredPosition, onScreenPosition, slideSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(displayTime);

        // Slide from center to right (off-screen)
        StartCoroutine(SlideTextOffScreen());
    }

    IEnumerator SlideTextOffScreen()
    {
        while (Vector3.Distance(effectText.rectTransform.anchoredPosition, offScreenPositionEnd) > 0.01f)
        {
            effectText.rectTransform.anchoredPosition = Vector3.MoveTowards(effectText.rectTransform.anchoredPosition, offScreenPositionEnd, slideSpeed * Time.deltaTime);
            yield return null;
        }

        effectText.text = "";  // Clear text after animation
    }
}
