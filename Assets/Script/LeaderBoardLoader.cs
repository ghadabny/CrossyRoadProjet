/*using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LeaderBoardLoader : MonoBehaviour
{
    public LeaderBoardData LBData;
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
    }
}*/
