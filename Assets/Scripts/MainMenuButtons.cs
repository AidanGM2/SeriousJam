using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
   
    [SerializeField] private string startLevel = "Level";
    [SerializeField] private string credits = "CreditsScreen";
    public void StartButton()
    {
        SceneManager.LoadScene(startLevel);
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene(credits);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
