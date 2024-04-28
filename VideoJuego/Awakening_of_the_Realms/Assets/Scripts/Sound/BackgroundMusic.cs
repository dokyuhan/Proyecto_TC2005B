using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        // Get the AudioSource component from the current GameObject
        audioSource = GetComponent<AudioSource>();
        
        // Find all game objects with the "GameMusic" tag
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("GameMusic");
        
        // Check if there is more than one music object
        if (musicObj.Length > 1)
        {
            foreach (GameObject obj in musicObj)
            {
                // Check if the current object is not this object
                if (obj != this.gameObject)
                {
                    // If it's another music object, stop its AudioSource and destroy it
                    AudioSource otherAudioSource = obj.GetComponent<AudioSource>();
                    if (otherAudioSource != null)
                    {
                        otherAudioSource.Stop();
                    }
                    Destroy(obj);
                }
            }
        }
        
        // Set this music object to not be destroyed on load
        DontDestroyOnLoad(this.gameObject);
        
        // Start playing music if not already playing
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
