using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject eaglePrefab;  // Assign this in the inspector with your Eagle prefab
    public Player player;           // Reference to the player script
    public float inactivityThreshold ;  // Time in seconds before spawning the eagle
    private float inactivityTimer = 0f;
    private GameObject currentEagle;  // To keep track of the spawned eagle

    void Update()
    {
        // Check if the player has moved
        if (player != null && player.hasNotMovedMuch())
        {
            inactivityTimer += Time.deltaTime;
        }
        else
        {
            inactivityTimer = 0; // Reset timer if the player moves significantly
        }

        // Spawn the eagle if the player has been inactive too long and no eagle currently exists
        if (inactivityTimer >= inactivityThreshold && currentEagle == null)
        {
            SpawnEagle();
        }
    }

    void SpawnEagle()
    {
        if (eaglePrefab)
        {
            Vector3 spawnPosition = new Vector3(player.transform.position.x, 20f, player.transform.position.z);
            currentEagle = Instantiate(eaglePrefab, spawnPosition, Quaternion.identity);
            Eagle eagleScript = currentEagle.GetComponent<Eagle>();
            if (eagleScript != null)
            {
                eagleScript.swoopTarget = player.transform;  // Set the target for the eagle
            }
            else
            {
                Debug.LogError("Eagle script not found on the eagle prefab!");
            }
        }
        else
        {
            Debug.LogError("Eagle prefab not assigned!");
        }
    }
}
