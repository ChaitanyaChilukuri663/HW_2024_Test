using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Reference to the UI Text component
    private int score = 0;

    void Start()
    {
        // Ensure scoreText is assigned
        if (scoreText == null)
        {
            Debug.LogError("ScoreText reference is missing in ScoreManager. Please assign it in the Inspector.");
            return;
        }

        UpdateScore();
    }

    public void IncrementScore()
    {
        score++;
        UpdateScore();
    }

    void UpdateScore()
    {
        // Check if scoreText is assigned before updating
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        else
        {
            Debug.LogError("ScoreText reference is missing in ScoreManager. Unable to update score.");
        }
    }

    public int GetScore()
    {
        return score;
    }
}
