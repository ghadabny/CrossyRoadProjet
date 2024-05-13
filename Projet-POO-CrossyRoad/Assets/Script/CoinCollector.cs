using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
       // Debug.Log("Collision with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("CrossyCoin"))
        {
            CollectCoin(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger with: " + other.gameObject.name);
        if (other.CompareTag("CrossyCoin"))
        {
            CollectCoin(other.gameObject);
        }
    }

    private void CollectCoin(GameObject coin)
    {
        Debug.Log("Collecting and destroying coin: " + coin.name);
        Destroy(coin);
        ScoreManager.instance.AddCoins(1);
    }

    
}
