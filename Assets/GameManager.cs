using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject gameOverScreen;
    public TextMeshProUGUI scoreText;
    private bool gameHasStarted = false;
    void Start()
    {
        Time.timeScale = 0; 
        startScreen.SetActive(true);
        gameOverScreen.SetActive(false);
    }
     public void StartGame()
    {
        if (!gameHasStarted)
        {
            gameHasStarted = true;
            Time.timeScale = 1; 
            startScreen.SetActive(false);
        }
    }

    public void GameOver(int finalScore)
    {
        Time.timeScale = 0; 
        gameOverScreen.SetActive(true);
        scoreText.text = "Game Over! Your Score: " + finalScore;
    }
}





   
