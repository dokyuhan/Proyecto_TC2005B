using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelsConfig", menuName = "Configuration/LevelsConfig")]
public class LevelsConfig : ScriptableObject {
    public Level[] levels;
}

[System.Serializable]
public class Level {
    public string name;
    public AILevel aiLevel;
    public Material background; // Reference to the background Material
    public int maxPlayerEnergy;
    public int maxAIEnergy;
}
