using UnityEngine;
using TMPro;
using System.Collections;

public class EffectText : MonoBehaviour
{
    public TextMeshProUGUI effectText;
    public float displayTime = 2.0f;

    void Start()
    {
        effectText.text = "";
    }

    public void ShowEffect(string message)
    {
        effectText.text = message;
        StartCoroutine(ClearTextAfterTime(displayTime));
    }

    IEnumerator ClearTextAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        effectText.text = "";
    }
}
