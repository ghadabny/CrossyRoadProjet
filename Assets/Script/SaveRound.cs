using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using TMPro;

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


    public static void SortRoundData()
    {
        // Sort
        //RoundData newRound = new RoundData(score, coins, elapsedTime);
        //List<RoundData> roundsUnsorted = new List<RoundData>();
        string path = Application.persistentDataPath + "/roundData.json";
        string pathSortedRoundsData = Application.persistentDataPath + "/sortedRoundData.json";

        string jsonUnsorted = File.ReadAllText(path);
        // Here put a regex here to extract the first one
        Debug.Log("jsonUnsorted 1 : " + jsonUnsorted);
        string jsonSorted = File.ReadAllText(pathSortedRoundsData);
        List<RoundData> roundsUnsorted1 = JsonConvert.DeserializeObject<List<RoundData>>(jsonSorted);
        //Debug.Log("roundsUnsorted1 : " + roundsUnsorted1);

        List<RoundData> rounds = new List<RoundData>();
        rounds = JsonUtility.FromJson<RoundDataList>(jsonUnsorted).rounds;
        foreach ( RoundData ent in rounds ) 
         {
            List<RoundData> roundsUnsorted = JsonConvert.DeserializeObject<List<RoundData>>(jsonUnsorted);
            //List<RoundData> roundsUnsorted = JsonConvert.DeserializeObject<List<RoundData>>(ent);
            roundsUnsorted.Sort((x, y) => y.score.CompareTo(x.score));
            string sortedJson = JsonConvert.SerializeObject(roundsUnsorted, Formatting.Indented);
            File.WriteAllText(pathSortedRoundsData, sortedJson);
            Debug.Log("Sorted file done");
         }



        Debug.Log("SortRoundData Done");
    }
}


/* How can I achieve what I want? 
 * First I coudl try and from the begining sort them.*/

