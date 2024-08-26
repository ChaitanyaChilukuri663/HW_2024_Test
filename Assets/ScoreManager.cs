using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    private int score = 0;
    void Start()
    {
        UpdateScore();
    }
    public void IncrementScore()
    {
        score++;
        UpdateScore();
    }
    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
    public int GetScore()
{
    return score;
}

}
