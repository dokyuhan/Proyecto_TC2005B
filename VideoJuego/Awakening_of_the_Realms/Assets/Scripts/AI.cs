using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AILevel { Aggressive, Defensive, Balanced }

public class AI : MonoBehaviour
{
    public AILevel personality;

    void PlayTurn()
    {
        switch (personality)
        {
            case AILevel.Aggressive:
                PlayAggressiveMove();
                break;
            case AILevel.Defensive:
                PlayDefensiveMove();
                break;
            case AILevel.Balanced:
                PlayBalancedMove();
                break;
        }
    }

    void PlayAggressiveMove()
    {
        
    }

    void PlayDefensiveMove()
    {

    }

    void PlayBalancedMove()
    {

    }
}