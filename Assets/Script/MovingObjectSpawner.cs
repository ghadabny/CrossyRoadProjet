using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> movingObjectPrefabs;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private float minSeperationTime;
    [SerializeField] private float maxSeperationTime;
    [SerializeField] private bool isRightSide;
    [SerializeField] private AudioClip warningSound; // Assign the warning sound in the inspector (optional)

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        StartCoroutine(SpawnVehicle());
    }

    private IEnumerator SpawnVehicle()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSeperationTime, maxSeperationTime));

            if (audioSource != null && warningSound != null)
            {
                audioSource.PlayOneShot(warningSound);
                yield return new WaitForSeconds(2f); // Wait for 2 seconds before spawning the object
            }
            else
            {
                // If no warning sound, just wait for 2 seconds
                yield return new WaitForSeconds(2f);
            }

            GameObject selectedPrefab = movingObjectPrefabs[Random.Range(0, movingObjectPrefabs.Count)];
            GameObject go = Instantiate(selectedPrefab, spawnPos.position, Quaternion.identity);

            if (!isRightSide)
            {
                go.transform.Rotate(new Vector3(0, 180, 0));
            }
            else if (isRightSide)
            {
                Vector3 adjustedSpawnPos = spawnPos.position;
                adjustedSpawnPos.z = isRightSide ? spawnPos.position.z - 25 : spawnPos.position.z + 25;
                go.transform.position = adjustedSpawnPos; // Adjust the spawn position
            }
        }
    }
}
