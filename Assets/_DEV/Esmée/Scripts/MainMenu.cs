using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("S_Main");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void AreUSure()
    {
        //als dat qua ui gemaakt word kan je deze op de button zetten en dan daarna pas de application.quit;
    }
}
