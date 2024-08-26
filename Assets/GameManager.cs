using UnityEngine;
using TMPro;
using Newtonsoft.Json.Linq;
public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance { get; private set; }

    public GameObject   
 startScreen;
    public GameObject gameOverScreen;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverScoreText; // Separate text for game over screen

    private bool gameHasStarted = false;
    private int score = 0; // Variable to track the player's score

    void Awake()
    {
        // Check if Instance already exists
        if (Instance == null)
        {
            Instance = this; // If not, set Instance to this
        }
        else if (Instance != this)
        {
            Destroy(gameObject);   
 // Enforce singleton pattern, destroy duplicate
        }

        DontDestroyOnLoad(gameObject); // Optional: Keeps the game manager active across scenes
    }

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
        gameOverScoreText.text = "Game Over! Your Score: " + finalScore;
    }

    // Method to increase the score
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    // Method to update the score display on the UI
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    // Method to get the current score
    public int GetScore()
    {
        return score;
    }
}