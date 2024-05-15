[System.Serializable]
public class RoundData
{
    public int score;
    public int coins;
    public string elapsedTime;

    public RoundData(int score, int coins, string elapsedTime)
    {
        this.score = score;
        this.coins = coins;
        this.elapsedTime = elapsedTime;
    }
}
