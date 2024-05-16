using UnityEngine;

public static class TimeFormatter
{
    public static string FormatTime(float timeInSeconds)
    {
        string minutes = Mathf.Floor(timeInSeconds / 60).ToString("00");
        string seconds = Mathf.Floor(timeInSeconds % 60).ToString("00");
        return string.Format("{0}:{1}", minutes, seconds);
    }
}
