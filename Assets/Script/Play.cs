using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{

    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene");

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
