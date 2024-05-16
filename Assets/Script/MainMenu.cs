using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Button btn;
    public GameObject SonOn;
    public GameObject SonOff;
    public bool muted = false;


    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.LoadScene("GameScene");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Le joueur a quitté le jeu");
    }
/*
    public AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayMusic();
    }

    public void PlayMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }


/*
    public void OnButton()
    {
        muted = false;
        SonOn.SetActive(true);
        SonOff.SetActive(false);

        Save();
    }

    public void OffButton()
    {
        muted = true;
        SonOn.SetActive(false);
        SonOff.SetActive(true);

        Save();
    }

    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }
*/
/*
    public void Song()
    {
        if(muted= PlayerPrefs.GetInt("muted") == 1)
        {
            music.GetComponent<AudioSource>().enabled = false;
        }
        else
        {
            music.GetComponent<AudioSource>().enabled = true;
        }
    }
    void Start()
    {
        btn = GameObject.Find("SonOn")?.GetComponent<Button>();
        btn.onClick.AddListener(Song);
            
        //Load();
        if(muted = PlayerPrefs.GetInt("muted") == 1)
        {
            SonOff.SetActive(true);
            SonOn.SetActive(false);
        }else{
            SonOn.SetActive(true);
            SonOff.SetActive(false);
        }

        if(muted= PlayerPrefs.GetInt("muted") == 1)
        {
            music.GetComponent<AudioSource>().enabled = false;
        }
        else
        {
            music.GetComponent<AudioSource>().enabled = true;
        }

    }
*/
}
