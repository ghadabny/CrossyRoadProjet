using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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
}
