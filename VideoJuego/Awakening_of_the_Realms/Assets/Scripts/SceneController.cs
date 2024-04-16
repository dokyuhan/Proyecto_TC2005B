using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; } // Singleton instance
    public LevelsConfig levelsConfig; // Assign this in the inspector
    public int currentLevelIndex = 0;
    public AI aiComponent;  // Reference to the AI component on the appropriate GameObject
    public MeshRenderer backgroundRenderer; // Reference to the MeshRenderer that displays the background

    void Awake()
    {
        if (Instance != null && Instance != this) 
        {
            Destroy(gameObject); // Ensures no duplicate singletons
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        if (aiComponent == null)
            Debug.LogError("AI component is not set in SceneController.");

        if (backgroundRenderer == null)
            Debug.LogError("Background renderer is not set in SceneController.");

        LoadLevel(currentLevelIndex);
    }

    public void LoadLevel(int index)
    {
        if (index < 0 || index >= levelsConfig.levels.Length)
        {
            Debug.LogError("Invalid level index");
            return;
        }

        Level level = levelsConfig.levels[index];
        aiComponent.personality = level.aiLevel;

        // Apply the background material
        if (backgroundRenderer != null && level.background != null)
        {
            backgroundRenderer.material = level.background;
        }
        else
        {
            Debug.LogError("Background renderer or material is not properly set up.");
        }

        Debug.Log("Level " + level.name + " loaded with AI setting " + level.aiLevel);
    }
}
