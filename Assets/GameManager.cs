using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject startScreen;
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
        InitializeGame();
    }
    private void InitializeGame()
    {
        Time.timeScale = 0;
        gameHasStarted = false;
        score = 0;
        UpdateScoreUI();
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
            gameOverScreen.SetActive(false);
        }
    }
    public void GameOver(int finalScore)
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
        gameOverScoreText.text = "Game Over! Your Score: " + finalScore;
    }
    public void RestartGame()
    {
        Debug.Log("Restarting the game...");
        score = 0;
        gameHasStarted = true;
        UpdateScoreUI();
        Time.timeScale = 1;
        startScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        Debug.Log("Game restarted. Game has started: " + gameHasStarted + ", Score: " + score);
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