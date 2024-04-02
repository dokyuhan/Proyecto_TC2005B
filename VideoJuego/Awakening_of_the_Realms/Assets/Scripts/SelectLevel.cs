using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{
    public void HumanOne()
    {
        SceneManager.LoadScene("HumanLevel1");
    }

    public void HumanTwo()
    {
        SceneManager.LoadScene("HumanLevel2");
    }

    public void MonsterOne()
    {
        SceneManager.LoadScene("MonsterLevel1");
    }

    public void MonsterTwo()
    {
        SceneManager.LoadScene("MonsterLevel2");
    }

    public void CelestialOne()
    {
        SceneManager.LoadScene("CelestialLevel1");
    }

    public void CelestialTwo()
    {
        SceneManager.LoadScene("CelestialLevel2");
    }


    public void MagicaOne()
    {
        SceneManager.LoadScene("MagicLevel1");
    }

    public void MagicaTwo()
    {
        SceneManager.LoadScene("MagicLevel2");
    }

    public void Back()
    {
        SceneManager.LoadScene("MainScreen");
    }

}
