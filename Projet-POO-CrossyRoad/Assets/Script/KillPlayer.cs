using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Player>() != null) 
        { 
            Destroy(collision.gameObject);
            SceneManager.LoadScene("GameOver");
            
        }
    }

}
