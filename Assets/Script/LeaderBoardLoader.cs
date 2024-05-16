using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class LeaderBoardLoader : MonoBehaviour
{
    public static string inputPath = "Assets/ScoreData.txt";
    public static string outputPath = "Assets/SortedScoreData.txt";
    public static string highestScore;
    public static void HighestScore()
    {
        string[] lines = File.ReadAllLines(inputPath);
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
        scores.Sort((a, b) => b.CompareTo(a));

        highestScore = scores[0];

        using (StreamWriter writer = new StreamWriter(outputPath))
        {
            foreach (string score in scores)
            {
                writer.WriteLine(score.ToString());
            }
        }

        Debug.Log("Scores sorted and saved. The highest score is: "+ highestScore);
    }


}
