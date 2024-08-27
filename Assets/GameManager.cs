using UnityEngine;
using TMPro;
using Newtonsoft.Json.Linq;
public class GameManager : MonoBehaviour
{
   
    public static GameManager Instance { get; private set; }

    public GameObject   
 startScreen;
    public GameObject gameOverScreen;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverScoreText; 

    private bool gameHasStarted = false;
    private int score = 0; 

    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this; 
        }
        else if (Instance != this)
        {
            Destroy(gameObject);   
 
        }

        DontDestroyOnLoad(gameObject); 
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

   
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    public int GetScore()
    {
        return score;
    }
}