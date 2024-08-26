using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject gameOverScreen;

    void Start()
    {
        Time.timeScale = 0; // Pause the game
        startScreen.SetActive(true);
        gameOverScreen.SetActive(false);
    }

    public void StartGame()
    {
        Time.timeScale = 1; // Resume the game
        startScreen.SetActive(false);
    }

    public void GameOver()
    {
        Time.timeScale = 0; // Pause the game
        gameOverScreen.SetActive(true);
    }
}
