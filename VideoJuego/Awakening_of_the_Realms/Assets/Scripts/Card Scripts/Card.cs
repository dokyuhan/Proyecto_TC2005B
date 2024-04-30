//script para creación de cartas
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;


//clase de la carta
[System.Serializable]
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
        public string rarity;
        public string Effect_type;
        public string Effect_description;
        public bool desbloqueada;
        public int uniqueID;

        public GameObject cardGameObject; // Reference to the card's GameObject


    public Card(int id, string nombre, string desc, int ataque, int defensa, int heal, string realm, int power, string rareza, string efecto, string efecto_desc, bool desb)
    {
            card_ID = id;
            card_name = nombre;
            card_description = desc;
            attack = ataque;
            defense = defensa;
            healing = heal;
            card_realm = realm;
            power_cost = power;
            rarity = rareza;
            Effect_type = efecto;
            Effect_description = efecto_desc;
            desbloqueada = desb;
    }


}

// clase para post del mazo
[System.Serializable]
public class CardData
{
    public int card_ID;
    public string player_ID;
    public int deck_ID;

    public CardData(int cardID, string playerID, int deckID)
    {
        card_ID = cardID;
        player_ID = playerID;
        deck_ID = deckID;
    }
}

[System.Serializable]
public class CardsContainer
{
    public List<CardData> cards = new List<CardData>();
}


// Clase auxiliar para deserializar la respuesta
[System.Serializable]
public class CardIdListResponse
{
    public int[] cardIds;
}

// Clase auxiliar para deserializar la respuesta
[System.Serializable]
public class CoinResponse
{
    public int coins;
}

[System.Serializable]
public class CardResponse
{
    public string message;
    public CoinResponse coins; 
    public Card card;
}
