using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    private Button btn;
    private Button PlayBtn = null;

    public GameObject pauseMenuUI;


    void Start()
    {
        btn = GameObject.Find("PauseBtn")?.GetComponent<Button>();
        if (btn != null && PlayBtn == null)
        {
            btn.onClick.AddListener(Pause);
            PlayBtn = GameObject.Find("PlayBtn")?.GetComponent<Button>();
        }
        PlayBtn = GameObject.Find("PlayBtn")?.GetComponent<Button>();
        if(PlayBtn != null)
        {
            PlayBtn.onClick.AddListener(Resume);
            PlayBtn = null;
        }
    }
    void Update()
    {
        btn = GameObject.Find("PauseBtn")?.GetComponent<Button>();
        if (btn != null && PlayBtn == null)
        {
            btn.onClick.AddListener(Pause);
            PlayBtn = GameObject.Find("PlayBtn")?.GetComponent<Button>();
        }
        PlayBtn = GameObject.Find("PlayBtn")?.GetComponent<Button>();
        if(PlayBtn != null)
        {
            PlayBtn.onClick.AddListener(Resume);
            PlayBtn = null;
        }
    }

    void Resume()
    {

        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
