using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text scoreText; 
    public Text coinText;  

    public static int score = 0; // was private
    public static int coins = 0; // was private

    //public LeaderBoardData LBData; // Added

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

    /*public static void SaveRoundData(float elapsedTime, int score, int coins) // Added
    {
        LeaderBoardData LBData = instance.LBData;
        RoundData round = new RoundData();
        round.elapsedTime = elapsedTime;
        round.score = score;
        round.coins = coins;
        LBData.rounds.Add(round);
        UnityEditor.EditorUtility.SetDirty(LBData);
        Debug.LogError("SaveRoundData done: " + round);
    }*/

    private void UpdateUI()
    {
        UpdateScoreText();
        UpdateCoinText();
    }
}
