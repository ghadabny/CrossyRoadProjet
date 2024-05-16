using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
/*using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;*/

public class LeaderBoardLoader : MonoBehaviour
{
    Leaderboard leaderboard = new Leaderboard();
    public Text roundScore;
    public Text roundCoins;
    public Text roundElapsedTime;
    List<RoundData> roundStats = new List<RoundData>();

    void Start()
    {
        roundStats = leaderboard.LoadLeaderboardData();
        roundScore.text = " " + roundStats[0];
        roundCoins.text = " " + roundStats[1];
        roundElapsedTime.text = " " + roundStats[2];
        Debug.LogWarning("LeaderboardLoader done!");
    }
}




    /*public LeaderBoardData LBData;
    public Text leaderboardText;

    void Start()
    {
        DisplayLeaderboard();
    }

    private void DisplayLeaderboard()
    {
        var sortedRounds = LBData.rounds.OrderByDescending(round => round.score);

        string leaderboard = "Leaderboard:\n";

        foreach (var round in sortedRounds)
        {
            string formattedTime = TimeFormatter.FormatTime(round.elapsedTime);
            leaderboard += $"Time: {formattedTime} - Score: {round.score} - Coins: {round.coins}\n";

        }

        leaderboardText.text = leaderboard;
    }*/

