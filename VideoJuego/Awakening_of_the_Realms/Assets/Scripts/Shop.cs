using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public APIConnection apiConnection;
    public CardDisplayManager cardDisplayManager;
    public Button buyButton;
    public TMP_Text response;
    public TMP_Text coinsText;
    public int currentUserId;

    void Start()
    {
        if (apiConnection != null && coinsText != null)
        {
            StartCoroutine(apiConnection.GetCoins(currentUserId, coinsAmount =>
            {
                coinsText.text = coinsAmount.ToString();
            }));
        }
        else
        {
            Debug.LogError("APIConnection or coinsText is not set on Shop");
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("MainScreen");
    }

   public void BuyCard()
    {
            if (apiConnection == null || cardDisplayManager == null || buyButton == null)
            {
                Debug.LogError("APIConnection, CardDisplayManager, or buyButton is not set on Shop");
                return;
            }

            buyButton.interactable = false;  
            cardDisplayManager.ClearCardsUI();

            int userId = currentUserId; 
            StartCoroutine(apiConnection.BuyCard(userId, (card, message, coinsString) =>
        {
            if (card != null)
            {
                cardDisplayManager.DisplayCards(card);
            }
            
            response.text = message;

            if (coinsString != 0)
            {
                coinsText.text = coinsString.ToString();  
            }
            buyButton.interactable = true;
            }));
    }

}
