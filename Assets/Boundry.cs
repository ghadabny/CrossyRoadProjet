using UnityEngine;
using UnityEngine.SceneManagement;

public class Boundary : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player reached the boundary and died.");
            Destroy(other.gameObject);
            SceneManager.LoadScene("GameOver");
        }
    }
}
