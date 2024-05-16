using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] AudioSource music;

    void Start()
    {
        // Retrieve the muted state from PlayerPrefs
        bool muted = PlayerPrefs.GetInt("Muted", 0) == 1;

        // Control the background music based on the muted state
        if (muted)
        {
            music.Stop();
        }
        else
        {
            music.Play();
        }
    }
}