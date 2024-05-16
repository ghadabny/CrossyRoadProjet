using UnityEngine;
using System.IO;
using System.Collections.Generic;


public class SaveRound
{
    public static void SaveRoundData()
    {
        // This isn't working. 
        string filePath = "Assets/ScoreData.txt";
        int score = ScoreManager.score;
        int coins = ScoreManager.coins;
        string elapsedTime = TimeFormatter.FormatTime(PlayTime.elapsedTime);

        string roundScores = score+","+coins+","+elapsedTime;

        //File.WriteAllText(filePath, roundScores);
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            // Write the new line to the file
            writer.WriteLine(roundScores);
        }

        Debug.Log("Round's scores saved.");
        }
    }


