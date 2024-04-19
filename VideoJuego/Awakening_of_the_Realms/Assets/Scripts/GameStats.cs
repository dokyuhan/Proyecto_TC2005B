using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStats
{
    public static int TotalDamageDealt;
    public static int TotalHealthCured;
    public static int TotalDefenseMitigated;

    public static void Reset()
    {
        TotalDamageDealt = 0;
        TotalHealthCured = 0;
        TotalDefenseMitigated = 0;
    }
}

