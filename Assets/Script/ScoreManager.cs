using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text scoreText;
    private int score = 0;

    void Awake()
    {
        // Singleton setup
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        // Attempt to find the scoreText again in case it's a new instance
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        if (scoreText == null)
            Debug.LogError("Failed to find scoreText component on GameObject.");
        else
            ResetScore();  // Optionally reset the score here
    }
    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = " " + score;
        else
            Debug.LogError("scoreText is null!");
    }

}
