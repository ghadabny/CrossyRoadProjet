

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
            
            // Randomly select a prefab to instantiate
            GameObject selectedPrefab = movingObjectPrefabs[Random.Range(0, movingObjectPrefabs.Count)];
    bool isTrain = selectedPrefab.CompareTag("CrossyTrain");
            GameObject go = Instantiate(selectedPrefab, spawnPos.position, Quaternion.identity);
            Vector3 adjustedSpawnPos = spawnPos.position;
            if (!isRightSide)
            {
                go.transform.Rotate(new Vector3(0, 180, 0));
               
            }
            
            else if (isRightSide) 
            {
                
                adjustedSpawnPos.z = isRightSide ? spawnPos.position.z - 25 : spawnPos.position.z + 25;
            }
        }
    }
}