using UnityEngine;
using TMPro;

public class DisplayScores : MonoBehaviour
{
    public static DisplayScores instance;

    public TMP_Text scoreText;
    public TMP_Text coinText;
    public TMP_Text timerText;
    public TMP_Text highScoreText;

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

    }

    void Update()
    {
        UpdateScoreText();
        UpdateCoinText();
        UpdateTimerText();
        UpdateHighScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + ScoreManager.score;
        else
            Debug.LogError("scoreText component not found!");
    }

    private void UpdateCoinText()
    {
        if (coinText != null)
            coinText.text = "Coins: " + ScoreManager.coins;
        else
            Debug.LogError("coinText component not found!");
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
            timerText.text = "Play Time:" + TimeFormatter.FormatTime(PlayTime.elapsedTime);
        else
            Debug.LogError("timerText component not found!");
    }

    private void UpdateHighScoreText()
    {
        if (highScoreText != null)
            highScoreText.text = "Highest Score: " + LeaderBoardLoader.highestScore;
        else
            Debug.LogError("highScoreText component not found!");
    }

}
