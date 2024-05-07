using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text scoreText; 
    public Text coinText;  

    private int score = 0;
    private int coins = 0;

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
       
        ResetScores();
    }

    public void ResetScores()
    {
        score = 0;
        coins = 0;
        UpdateUI(); 
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText(); 
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoinText(); 
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = " " + score;
        else
            Debug.LogError("ScoreText component not found!");
    }

    private void UpdateCoinText()
    {
        if (coinText != null)
            coinText.text = " " + coins;
        else
            Debug.LogError("CoinText component not found!");
    }

    private void UpdateUI()
    {
        UpdateScoreText();
        UpdateCoinText();
    }
}
