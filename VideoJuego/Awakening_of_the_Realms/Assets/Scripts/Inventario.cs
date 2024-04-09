using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Inventario : MonoBehaviour
{   
    /* Inventario es declarado como una Instancia que propiedades de un public getter y un private setter
    Esta instancia es utilizada para implementar un singleton design pattern, que se refiere a que el inventario es unico para todo el juego */
    public static Inventario Instance { get; private set; }

    public CardDisplayManager cardDisplayManager; 

    /* Funcion privada que utiliza Awake (inicializa cualquiera condicion antes de iniciar el juego)
    Dentro de la funcion se esta inicializando condiciones para verificar si el inventario ya existe o no, si existe lo elimina, si no lo mantiene */
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            CardFetch.CardsFetched += DisplayAllCards;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Funcion que corre cuando el obejto es destruido
    private void OnDestroy()
    {
        CardFetch.CardsFetched -= DisplayAllCards;
    }

    public void DisplayAllCards(List<Card> cards)
    {
        foreach (Card card in cards)
        {
            cardDisplayManager.DisplayCards(card);
        }
    }


    public void Back()
    {
        SceneManager.LoadScene("MainScreen");
    }

    public void Save()
    {
        //aqui se debe hacer el post de la lista mazo;
    }
}


