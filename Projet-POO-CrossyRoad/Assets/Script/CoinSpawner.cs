using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab; 
    public int numberOfCoins; 

    void Start()
    {
        SpawnCoins();
    }

    private void SpawnCoins()
    {
        for (int i = 0; i < numberOfCoins; i++)
        {
            Vector3 coinPosition = new Vector3(
                Random.Range(-25f, 25f),
                -0.05f,
                Random.Range(-25f, 25f)
            );

            GameObject coin = Instantiate(coinPrefab, transform.position + coinPosition + new Vector3(0, 0.5f, 0), Quaternion.identity, transform);
            coin.tag = "CrossyCoin"; 
        }
    }

}


