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
        // Assuming the GameObject this script is attached to has a Renderer or Collider from which to get bounds
        Bounds bounds = GetComponent<Renderer>().bounds;  // Use Collider if Renderer is not applicable

        for (int i = 0; i < numberOfCoins; i++)
        {
            // Generate random x and z positions within the bounds of the terrain
            float xPosition = Mathf.Round(Random.Range(bounds.min.x, bounds.max.x));
            float zPosition = Mathf.Round(Random.Range(bounds.min.z, bounds.max.z));

            // Construct the position vector with the rounded values, and correct Y position based on terrain or desired offset
            Vector3 coinPosition = new Vector3(
                xPosition,
                transform.position.y +0.45f,  // You may want to adjust this based on terrain's actual surface level
                zPosition
            );

            // Instantiate the coin at the calculated position, at ground level plus an offset
            GameObject coin = Instantiate(coinPrefab, coinPosition, Quaternion.identity, this.transform);  // Set terrain as parent
            coin.tag = "CrossyCoin";
        }
    }
}
