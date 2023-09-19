using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Minigame");  
    }
    public void StartMediumGame()
    {
        SceneManager.LoadScene("MinigameMedium");  
    }

    public void StartHardGame()
    {
        SceneManager.LoadScene("MinigameHard");  
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");  
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
