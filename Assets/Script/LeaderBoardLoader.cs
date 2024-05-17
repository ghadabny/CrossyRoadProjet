using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

public class LeaderBoardLoader : MonoBehaviour
{
    public static string unsortedPath = "Assets/Script/ScoreData.txt";
    public static string sortedPath = "Assets/Script/SortedScoreData.txt";
    public static string highestScore;
    public static void HighestScore()
    {
        string[] lines = File.ReadAllLines(unsortedPath);
        List<string> scores = new List<string>();

        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            scores.Add(parts[0]);

        }
        List<int> intScores = new List<int>();
        foreach (string score in scores)
        {
            int intScore;
            if (int.TryParse(score, out intScore))
            {
                intScores.Add(intScore);
            }
            else
            {
                Debug.LogWarning("Failed to parse score: " + score);
            }
        }
        intScores.Sort((a, b) => b.CompareTo(a));

        highestScore = intScores[0].ToString();

        using (StreamWriter writer = new StreamWriter(sortedPath))
        {
            foreach (string score in scores)
            {
                writer.WriteLine(intScores.ToString());
            }
        }

        Debug.Log("Scores sorted and saved. The highest score is: " + highestScore);
    }

    public static List<string> MakeLeaderboard()
    {
        List<string> topFive = new List<string>();

        string[] sortedScores = File.ReadAllLines(sortedPath);

        using (StreamReader unsortedScores = new StreamReader(unsortedPath))
        {
            string line;
            while ((line = unsortedScores.ReadLine()) != null)
            {
                string[] parts = line.Split(',');

                for (int i = 0; i < 5; i++)
                //foreach (string sortedScore in sortedScores) // here add a range
                {
                    if (parts[0] == sortedScores[0])
                    {
                        topFive[0] = line;
                        break;
                    }

                    if (parts[0] == sortedScores[1])
                    {
                        topFive[1] = line;
                        break;
                    }

                    if (parts[0] == sortedScores[2])
                    {
                        topFive[2] = line;
                        break;
                    }

                    if (parts[0] == sortedScores[3])
                    {
                        topFive[3] = line;
                        break;
                    }

                    if (parts[0] == sortedScores[4])
                    {
                        topFive[4] = line;
                        break;
                    }
                }

            }
        }

        return topFive;
    }
}


