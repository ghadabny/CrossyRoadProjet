

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovingObjectSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> movingObjectPrefabs; // List of different vehicle prefabs
    [SerializeField] private Transform spawnPos;
    [SerializeField] private float minSeperationTime;
    [SerializeField] private float maxSeperationTime;
    [SerializeField] private bool isRightSide;

    private void Start()
    {
        StartCoroutine(SpawnVehicle());
    }

    private IEnumerator SpawnVehicle()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSeperationTime, maxSeperationTime));
            // Randomly select a prefab to instantiate
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
            }
        }
    }
}

