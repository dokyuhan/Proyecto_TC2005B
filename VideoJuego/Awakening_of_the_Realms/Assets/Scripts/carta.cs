//script para creación de cartas

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carta
{

    /*public int Card_Id;
    public string Name;
    public string Description;
    public int Attack;
    public int Defense;
    public int Healing;
    public int Power_Cost;
    public int XP_Cost;
    public string Realm;
    public string Rarity;
    public string Level;
    public string Effect;
    
    // Añadido para manejar el estado de desbloqueo
    public bool IsUnlocked;
    */
    public string nombre;
    public string imagen; 
    public bool desbloqueada;

    public Carta(string name, string nombreImagen, bool bloqueada)
    {
        nombre = name;
        imagen = nombreImagen;
        desbloqueada = bloqueada;
    }

}
