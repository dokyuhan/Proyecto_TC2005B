using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ButtonSound : MonoBehaviour
{
    private AudioSource audioSource;

    // Use SerializeField to expose the field in the inspector while keeping it private.
    [SerializeField] private AudioClip sound;

    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
        //audioSource.playOnAwake = false;  // Make sure the sound does not play on awake
        audioSource.clip = sound; // Assign the clip once here if it doesn't change

        // Find the Button component on the same GameObject and add the listener
        var button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(PlaySound);
        }
        else
        {
            Debug.LogError("ButtonSound script is not on a button GameObject");
        }
    }

    void PlaySound()
    {
        Debug.Log("Attempting to play sound");
        if (audioSource != null)
        {
            Debug.Log("AudioSource exists");
            if (audioSource.clip != null)
            {
                Debug.Log("Clip is assigned: " + audioSource.clip.name + ", volume: " + audioSource.volume);
                audioSource.Play();
            }
            else
            {
                Debug.LogError("No AudioClip is assigned to AudioSource");
            }
        }
        else
        {
            Debug.LogError("AudioSource component missing");
        }
    }



}
