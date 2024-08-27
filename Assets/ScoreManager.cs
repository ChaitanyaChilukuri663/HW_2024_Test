using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;  
    private int score = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreText(); 
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        Debug.Log("Score increased by " + amount + ". New score: " + score);  
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
            Debug.Log("Score UI updated to: " + score);  
        }
    }
}
