using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance;
    public AudioClip currentClip;  // Public to assign default clip or modify in Inspector
    private AudioSource audioSource;

    public static BackgroundMusic Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        ManageSingleton();
        audioSource = GetComponent<AudioSource>();
    }

    private void ManageSingleton()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            if (instance.currentClip != this.currentClip)
            {
                instance.PlayMusic(this.currentClip);  // Play new clip
            }
            Destroy(this.gameObject);  // Destroy the new instance
        }
    }

    public void PlayMusic(AudioClip newClip)
    {
        if (audioSource.clip == newClip && audioSource.isPlaying)
            return; // The correct music is already playing

        audioSource.clip = newClip;
        audioSource.Play();
        currentClip = newClip; // Update the current clip reference
    }
}
