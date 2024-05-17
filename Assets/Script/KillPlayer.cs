using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class KillPlayer : MonoBehaviour
{
    //public ScoreManager scoreManager;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Player>() != null) 
        {
            Destroy(collision.gameObject);
            //float elapsedTime = PlayTime.elapsedTime; // Added 
            //int score = ScoreManager.score; // Added
            //int coins = ScoreManager.coins; // Added
            //Debug.LogError("PT: " + elapsedTime + " score: " + score + " coins: " + coins); So these work
            //SaveRound.SaveRoundData();
            //SaveRound.SortRoundData();
            //Save.saveScores();
            //Save.sortScores();
            //SaveRound.SaveRoundData();
            //LeaderBoardLoader.HighestScore();
           // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene("GameOver");
            //ScoreManager.SaveRoundData(elapsedTime, score, coins); // Added This on the other hand causes issues
            //Debug.LogError("Round Saved."); // Added
        }
    }

}
