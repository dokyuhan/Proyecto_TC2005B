//script para creación de cartas en el inventario
//script para creación de cartas
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Inventario : MonoBehaviour
{

    public CardDisplayManager cardDisplayManager;
    public GameObject apiConnectionGameObject;
    private APIConnection apiConnection;
    public List<Card> mazo = new List<Card>();


    IEnumerator Start()
    {
       // Obtén una referencia al script APIConnection adjunto al GameObject
        apiConnection = apiConnectionGameObject.GetComponent<APIConnection>();

        // Llama al método GetCards y espera a que termine
        yield return StartCoroutine(apiConnection.GetCards());

        // Muestra todas las cartas obtenidas
        foreach (Card card in apiConnection.cards)
        {
            cardDisplayManager.DisplayCards(card);
        }

        foreach (Card card in apiConnection.cards)
        {
            mazo.Add(card);
        }



    }



    public void Back()
    {
        SceneManager.LoadScene("MainScreen");
    }

    public void Save()
    {
    
    }




}


