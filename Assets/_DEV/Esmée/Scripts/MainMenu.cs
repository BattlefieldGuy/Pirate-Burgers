using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject sure;
    public void StartGame()
    {
        SceneManager.LoadScene("S_Main");
    }

    public void AreUSure()
    {
        sure.SetActive(true);
    }

    public void Yes()
    {
        Application.Quit();
    }

    public void No()
    {
        sure.SetActive(false);
    }
}
