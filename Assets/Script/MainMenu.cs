using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioSource music;

    public bool muted = false;

    public void Play()
    {
        // Save the muted state in PlayerPrefs before loading the next scene
        PlayerPrefs.SetInt("Muted", muted ? 1 : 0);
        PlayerPrefs.Save();

        // Load the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Le joueur a quitté le jeu");
    }

    void Start()
    {
        BackgroundMusic();
    }

    void Update()
    {

    }

    public void PlayMusic()
    {
        music.Play();
        muted = false;
    }

    public void StopMusic()
    {
        music.Stop();
        muted = true;
    }

    public void BackgroundMusic()
    {
        if (muted == true)
        {
            StopMusic();
        }
        else
        {
            PlayMusic();
        }
    }

    // Method to toggle music state from the UI
    public void ToggleMute()
    {
        muted = !muted;
        BackgroundMusic();
    }
}