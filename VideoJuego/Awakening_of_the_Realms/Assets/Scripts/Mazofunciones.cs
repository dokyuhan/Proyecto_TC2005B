using UnityEngine;
using UnityEngine.SceneManagement;

public class Mazofunciones : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Back()
    {
        SceneManager.LoadScene("MainScreen");
    }
}
