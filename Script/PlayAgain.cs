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
}
