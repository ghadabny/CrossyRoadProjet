using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;

// I can remove the limit of 5 from the file but instead make a sorted list and take the top 5!

// I can't return 2 lists so maybe the sorting should happen elsewhere. or I just generate the two lists elsewhere.

public class Leaderboard
{
    public int lastScore;
    public int lastCoins;
    public string lastElapsedTime;

    public List<RoundData> LoadLeaderboardData()
    {
        string pathRoundData = Application.persistentDataPath + "/roundData.json";
        //string pathSortedRoundData = Application.persistentDataPath + "/sortedRoundData.json";
        List<RoundData> roundStats = new List<RoundData>();
        //List<RoundData> SortedRoundStats = new List<RoundData>();
        string json = File.ReadAllText(pathRoundData);
        RoundDataList roundDataList = JsonUtility.FromJson<RoundDataList>(json);

        if (File.Exists(pathRoundData))
        {
            if (roundDataList.rounds.Count > 0)
            {
                RoundData lastRound = roundDataList.rounds[roundDataList.rounds.Count - 1];
                lastScore = lastRound.score;
                lastCoins = lastRound.coins;
                lastElapsedTime = lastRound.elapsedTime;

                roundStats.Add(new RoundData(lastScore, lastCoins, lastElapsedTime));
                //SortedRoundStats = roundStats.OrderBy(x => x.score).ToList();
                Debug.LogWarning("score: " + lastScore + " coins: " + lastCoins + " ET: " + lastElapsedTime);
            }
            else
            {
                Debug.LogWarning("No rounds found in the leaderboard data.");
            }
        }
        else
        {
            Debug.LogWarning("Leaderboard data file not found.");
        }

        return roundStats;
    }
}

/* public Text lastScore;
   public Text lastCoins;
   public Text lastElapsedTime;*/

/*lastScore.text = " "+lastRound.score;
lastCoins.text = " "+lastRound.coins;
lastElapsedTime.text = " "+lastRound.elapsedTime;*/

//string pathSortedRoundData = Application.persistentDataPath + "/sortedRoundData.json";

/*
             if (roundDataList.rounds.Count > 0)
            {
                RoundData lastRound = roundDataList.rounds[roundDataList.rounds.Count - 1];
                lastScore = lastRound.score;
                lastCoins = lastRound.coins;
                lastElapsedTime = lastRound.elapsedTime;
            }
            else
            {
                Debug.LogWarning("No rounds found in the leaderboard data.");
            }*/