using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    private AudioSource audioSource;

    // Assign clips via the Inspector
    public AudioClip winClip;
    public AudioClip loseClip;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            // Ensure an AudioSource component is attached
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null) // Check if AudioSource exists
            {
                audioSource = gameObject.AddComponent<AudioSource>(); // Add AudioSource if it does not exist
            }
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void StopAllMusic()
    {
        // Find all AudioSource components in the scene and stop them
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in allAudioSources)
        {
            source.Stop();
        }
    }



    public void PlayMusic(AudioClip clip)
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component missing on MusicManager GameObject!");
            return;
        }

        if (clip == null)
        {
            Debug.LogError("No clip provided to play.");
            return;
        }

        // Stop all music playing in the scene
        StopAllMusic();

        audioSource.clip = clip;
        audioSource.Play();
    }


    // Static methods for easy access
    public static void PlayWinMusic()
    {
        if (Instance != null)
            Instance.PlayMusic(Instance.winClip);
    }

    public static void PlayLoseMusic()
    {
        if (Instance != null)
            Instance.PlayMusic(Instance.loseClip);
    }

    public void DestroyMusicManager()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            Instance = null;
            Debug.Log("MusicManager has been destroyed.");
        }
    }

}
