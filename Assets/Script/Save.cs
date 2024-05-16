using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class Save : MonoBehaviour
{
    static int score = ScoreManager.score;
    static int coins = ScoreManager.coins;
    static string elapsedTime = TimeFormatter.FormatTime(PlayTime.elapsedTime);

    public static void saveScores()
    {
        List<List<string>> scoreList = new List<List<string>>();
        List<List<string>> scoreListSorted = new List<List<string>>();
        List<string> playedRound = new List<string>();

        playedRound.Add(score.ToString());
        playedRound.Add(coins.ToString());
        playedRound.Add(elapsedTime);

        scoreList.Add(playedRound);
        scoreListSorted = scoreList.OrderByDescending(innerList => int.Parse(innerList[0])).ToList();
        //Debug.Log(scoreList[0][0]);

    }
}
