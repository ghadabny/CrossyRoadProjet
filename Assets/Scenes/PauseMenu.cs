using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    private Button btn;
    private Button PlayBtn = null;
    private Button restartBtn = null;
    private Button QuitBtn = null;
    public MainMenu mainMenu;
    public GameObject pauseMenuUI;

    public bool muted;

    void Start()
    {
      
        btn = GameObject.Find("PauseBtn")?.GetComponent<Button>();
        if (btn != null && PlayBtn == null && restartBtn == null)
        {
            btn.onClick.AddListener(Pause);
            PlayBtn = GameObject.Find("PlayBtn")?.GetComponent<Button>();
            restartBtn = GameObject.Find("StartBtn")?.GetComponent<Button>();
        }
        PlayBtn = GameObject.Find("PlayBtn")?.GetComponent<Button>();
        
        if(PlayBtn != null)
        {
            PlayBtn.onClick.AddListener(Resume);
            PlayBtn = null;
            restartBtn = null;
        }
        
        restartBtn = GameObject.Find("StartBtn")?.GetComponent<Button>();
        if(restartBtn != null)
        {
            restartBtn.onClick.AddListener(RestartGame);
            restartBtn = null;
            PlayBtn = null;   
        }

        QuitBtn = GameObject.Find("QuitBtn")?.GetComponent<Button>();
        if(QuitBtn != null)
        {
            QuitBtn.onClick.AddListener(Reload);
            restartBtn = null;
            PlayBtn = null;
            QuitBtn = null;
        }
    }
    void Update()
    {
        
        btn = GameObject.Find("PauseBtn")?.GetComponent<Button>();
        if (btn != null && PlayBtn == null && restartBtn == null && QuitBtn == null)
        {
            btn.onClick.AddListener(Pause);
            PlayBtn = GameObject.Find("PlayBtn")?.GetComponent<Button>();
            restartBtn = GameObject.Find("StartBtn")?.GetComponent<Button>();
            QuitBtn = GameObject.Find("QuitBtn")?.GetComponent<Button>();

        }
        PlayBtn = GameObject.Find("PlayBtn")?.GetComponent<Button>();
        
        if(PlayBtn != null)
        {
            PlayBtn.onClick.AddListener(Resume);
            PlayBtn = null;
            restartBtn = null;
            QuitBtn = null;
        }
        
        restartBtn = GameObject.Find("StartBtn")?.GetComponent<Button>();
        if(restartBtn != null)
        {
            restartBtn.onClick.AddListener(Resume);
            restartBtn = null;
            PlayBtn = null;
            QuitBtn = null;     
        }

        QuitBtn = GameObject.Find("QuitBtn")?.GetComponent<Button>();
        if(QuitBtn != null)
        {
            QuitBtn.onClick.AddListener(Resume);
            restartBtn = null;
            PlayBtn = null;
            QuitBtn = null;
        }
    }

    void Resume()
    {

        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {

        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    void RestartGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    void Reload()
    {
        SceneManager.LoadScene("Main Menu");
    }

    
}
