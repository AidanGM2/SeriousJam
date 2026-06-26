using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
   
    [SerializeField] private string startLevel = "Level";

    public void StartButton()
    {
        SceneManager.LoadScene(startLevel);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
