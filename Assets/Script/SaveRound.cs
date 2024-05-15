using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class SaveRound
{
    public static void SaveRoundData()
    {
        int score = ScoreManager.score;
        int coins = ScoreManager.coins;
        string elapsedTime = TimeFormatter.FormatTime(PlayTime.elapsedTime);

        RoundData newRound = new RoundData(score, coins, elapsedTime);

        string path = Application.persistentDataPath + "/roundData.json";

        List<RoundData> rounds = new List<RoundData>();

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            rounds = JsonUtility.FromJson<RoundDataList>(json).rounds;
        }

        if (rounds.Count >= 5)
        {
            int minScoreIndex = 0;
            int minScore = rounds[0].score;

            for (int i = 1; i < rounds.Count; i++)
            {
                if (rounds[i].score < minScore)
                {
                    minScore = rounds[i].score;
                    minScoreIndex = i;
                }
            }

            if (score > minScore)
            {
                rounds[minScoreIndex] = newRound;
            }
            else
            {
                Debug.Log("New score is not higher than the lowest score.");
                return;
            }
        }
        else
        {
            rounds.Add(newRound);
        }

        RoundDataList roundDataList = new RoundDataList();
        roundDataList.rounds = rounds;

        string jsonToSave = JsonUtility.ToJson(roundDataList);

        File.WriteAllText(path, jsonToSave);

        //Debug.Log("Round data saved: " + newRound);
        //Debug.Log("Persistent Data Path: " + Application.persistentDataPath);
    }
}