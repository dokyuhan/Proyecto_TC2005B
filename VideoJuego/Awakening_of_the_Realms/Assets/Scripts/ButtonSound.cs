using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSound : MonoBehaviour
{
    public AudioClip soundEffect;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => PlaySound());
    }

    private void PlaySound()
    {
        AudioManager.instance.PlaySound(soundEffect);
    }
}
