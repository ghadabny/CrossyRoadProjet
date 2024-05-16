using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{

    public void LoadGame() 
    {
        SceneManager.LoadScene("SampleScene");
        Debug.Log("PlayAgain Pressed");

    }

    public void BackMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Debug.Log("Menu Pressed");

    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Le joueur a quitté le jeu");
    }
}
