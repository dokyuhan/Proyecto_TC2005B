//script para creación de cartas
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

   [System.Serializable]
    public class Card
    {
        public int card_ID;
        public string card_name;
        public string card_description;
        public int attack; // Ensure this matches with your data. You might need a custom parsing method if your data structure is complex.
        public int defense; // Used for fetching the sprite
        public int healing; // Used for fetching the sprite
        public string card_realm; // Used for fetching the sprite
        public int power_cost; // Used for fetching the sprite
        public int exp_cost; // Used for fetching the sprite
        public string rarity; // Used for fetching the sprite
        public int card_level; // Used for fetching the sprite
        public string Effect_type; // Used for fetching the sprite
        public bool desbloqueada;

        public Card(int card_ID, string card_name, string card_description, int attack, int defense, int healing, string card_realm, int power_cost, int exp_cost, string rarity, int card_level, string Effect_type, bool desbloqueada)
    {
        this.card_ID = card_ID;
        this.card_name = card_name;
        this.card_description = card_description;
        this.attack = attack;
        this.defense = defense;
        this.healing = healing;
        this.card_realm = card_realm;
        this.power_cost = power_cost;
        this.exp_cost = exp_cost;
        this.rarity = rarity;
        this.card_level = card_level;
        this.Effect_type = Effect_type;
        this.desbloqueada = desbloqueada;
    }



        /*public Card(int ID,string name, string desc, int att, int def, int heal, string rein, int power, int exp, string rarity, int lvl, string effect)
        {
            id = ID;
            nombre = name;
            descripcion = desc;
            ataque = att;
            defensa = def;
            curacion = heal;
            reino = rein;
            poderCosto = power;
            expCosto = exp;
            rareza = rarity;
            nivel = lvl;
            efecto = effect;
        }*/


    }