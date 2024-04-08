//script para creación de cartas
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

    public class Card
    {
        public int card_ID;
        public string card_name;
        public string card_description;
        public int attack;
        public int defense;
        public int healing;
        public string card_realm;
        public int power_cost;
        public int exp_cost;
        public string rarity;
        public int card_level;
        public string Effect_type;
        public bool desbloqueada;

        public Card(int id, string nombre, string desc, int ataque, int defensa, int heal, string realm, int power, int exp, string rareza, int nivel, string efecto, bool desb)
        {
            card_ID = id;
            card_name = nombre;
            card_description = desc;
            attack = ataque;
            defense = defensa;
            healing = heal;
            card_realm = realm;
            power_cost = power;
            exp_cost = exp;
            rarity = rareza;
            card_level = nivel;
            Effect_type = efecto;
            desbloqueada = desb;
        }


    }