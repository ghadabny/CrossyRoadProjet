using UnityEngine;
using UnityEngine.UI;

public class PlayTime : MonoBehaviour
{
    public static PlayTime instanceTimer;
    public Text PlayTimeText;
    public static float elapsedTime; // wasn't static
    public float startTime;

    void Awake()
    {
        if (instanceTimer == null)
        {
            instanceTimer = this;
        }
        else if (instanceTimer != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ResetTimer();
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        startTime = Time.time;
        UpdateUI();
        Debug.LogError("Timer started.");
    }

    void Update()
    {
        elapsedTime = Time.time - startTime;
        UpdateUI();
        //Debug.LogError("elapsedTime: " + elapsedTime);
    }

    private void UpdateUI()
    {
        
         PlayTimeText.text = TimeFormatter.FormatTime(elapsedTime);       
        //Debug.LogError("UI updated");
    }
}