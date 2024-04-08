using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class User
{
    public string player_name;
    public string player_last_name;
    public int player_age;
    public string user_name;
    public string password;
    public string realm;
    public bool is_npc = false;
    public int level = 1; 
    public int player_exp = 0;
    public int win_record = 0;
    public int lose_record = 0;
    public int coins = 0; 
    public int token = 0;
}