using UnityEngine;
using System.Collections;

public class EagleBehavior : MonoBehaviour
{
    public Player player; // Reference to the Player script
    private Vector3 lastPosition;
    private float timeStationary;
    public float timeToPickUp = 5.0f; // Time in seconds after which the eagle picks up the player
    public GameObject eagle; // Reference to the Eagle GameObject

    public Vector3 offscreenStartPosition = new Vector3(-10, 10, 0); // Modify this to ensure it's offscreen

    void Start()
    {
        if (player != null)
            lastPosition = player.transform.position;
        eagle.SetActive(false);
        eagle.transform.position = offscreenStartPosition; // Set eagle's starting position
    }

    void Update()
    {
        if (player != null)
        {
            if (player.transform.position == lastPosition)
            {
                // Player has not moved
                timeStationary += Time.deltaTime;
                if (timeStationary >= timeToPickUp && !eagle.activeInHierarchy)
                {
                    PickUpPlayer();
                }
            }
            else
            {
                // Player has moved, reset timer
                timeStationary = 0;
                lastPosition = player.transform.position;
            }
        }
    }

    void PickUpPlayer()
    {
        eagle.SetActive(true); // Enable eagle GameObject
        StartCoroutine(FlyToPlayer());
    }

    IEnumerator FlyToPlayer()
    {
        Vector3 startPosition = offscreenStartPosition; // Start from an off-screen position
        Vector3 endPosition = new Vector3(player.transform.position.x, offscreenStartPosition.y, player.transform.position.z); // End directly above the player

        // Fly from off-screen to directly above the player
        float journey = 0f;
        while (journey <= 1f)
        {
            journey += Time.deltaTime / 2; // Duration it takes to reach the player, adjust as necessary
            eagle.transform.position = Vector3.Lerp(startPosition, endPosition, journey);
            yield return null;
        }

        // Hover briefly before picking up
        yield return new WaitForSeconds(1.5f); // Adjust hover time as necessary

        // Move down to player's level
        while (eagle.transform.position != player.transform.position)
        {
            eagle.transform.position = Vector3.MoveTowards(eagle.transform.position, player.transform.position, Time.deltaTime * 5);
            yield return null;
        }

        // When reached, deactivate player
        player.gameObject.SetActive(false); // Or Destroy(player.gameObject);
        eagle.SetActive(false); // Optionally hide eagle again
    }
}
