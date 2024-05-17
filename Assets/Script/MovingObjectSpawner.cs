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
                yield return new WaitForSeconds(warningSound.length); // Wait for the length of the warning sound
            }

            GameObject selectedPrefab = movingObjectPrefabs[Random.Range(0, movingObjectPrefabs.Count)];
            GameObject go = Instantiate(selectedPrefab, spawnPos.position, Quaternion.identity);

            if (!isRightSide)
            {
                go.transform.Rotate(new Vector3(0, 180, 0));
            }
        }
    }
}
