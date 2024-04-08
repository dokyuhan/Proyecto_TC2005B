using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class APIConnection : MonoBehaviour
{
    [SerializeField] private string apiURL = "http://localhost:3200";

    public IEnumerator AddUser(string endpoint, string jsonData, System.Action<bool, string> callback)
    {
        using (UnityWebRequest www = UnityWebRequest.Put(apiURL + endpoint, jsonData))
        {
            www.method = "POST"; 
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                callback(true, ""); 
            }
            else
            {
                callback(false, www.error); 
            }
        }
    }
}

