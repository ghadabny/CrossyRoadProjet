using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text scoreText;
    public Text coinText;

    public static int score = 0;
    public static int coins = 0;

    public AudioClip coinSound; // Assign the coin sound in the inspector
    public AudioClip milestoneSound; // Assign the milestone sound in the inspector
    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            audioSource = GetComponent<AudioSource>(); // Get the AudioSource component

            if (audioSource == null)
            {
                Debug.LogError("AudioSource component not found on the GameObject.");
            }

            if (coinSound == null)
            {
                Debug.LogError("coinSound AudioClip not assigned in the inspector.");
            }

            if (milestoneSound == null)
            {
                Debug.LogError("milestoneSound AudioClip not assigned in the inspector.");
            }
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

        // Play milestone sound every 20 points
        if (score % 15 == 0 && score != 0)
        {
            if (audioSource != null && milestoneSound != null)
            {
                audioSource.PlayOneShot(milestoneSound);
            }
            else
            {
                Debug.LogWarning("AudioSource or milestoneSound not set!");
            }
        }

        UpdateScoreText();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        if (audioSource != null && coinSound != null)
        {
            audioSource.PlayOneShot(coinSound); // Play the coin sound
        }
        else
        {
            Debug.LogWarning("AudioSource or coinSound not set!");
        }
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
