using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Shop : MonoBehaviour
{
    public APIConnection apiConnection;
    public CardDisplayManager cardDisplayManager;
    public int currentUserId;
    public TMP_Text response;
    //public TMP_Text coinsText;

    public void Back()
    {
        SceneManager.LoadScene("MainScreen");
    }

    public void BuyCard()
    {
        if (apiConnection == null || cardDisplayManager == null || response == null)
        {
            Debug.LogError("APIConnection, CardDisplayManager, or response text is not set on Shop");
            return;
        }

        cardDisplayManager.ClearCardsUI();

        int userId = currentUserId; 
        StartCoroutine(apiConnection.BuyCard(userId, (card, message) => {
                cardDisplayManager.DisplayCards(card);
        }));
    }

}
