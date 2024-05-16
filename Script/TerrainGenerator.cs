using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{

    [SerializeField] private int maxTerrainCount;
    [SerializeField] private List<TerrainData> terrainDatas = new List<TerrainData>();
    [SerializeField] private List<TerrainData> terrainDatasInit = new List<TerrainData>();
    [SerializeField] private Transform terrainHolder;
    [SerializeField] private int minDistanceFromPlayer;


    private List<GameObject> currentTerrains = new List<GameObject>();
    [HideInInspector] public Vector3 currentPosition = new Vector3(0,0,0);

    private void Start()
    {
        
        foreach (var terrainData in terrainDatasInit)
        {
            SpawnTerrainInit(terrainData);
        }

       
        while (currentTerrains.Count < maxTerrainCount)
        {
            SpawnTerrain(true,new Vector3(0, 0, 0));
        }
    }

    private void SpawnTerrainInit(TerrainData terrainData)
    {
        int terrainInSuccession = Random.Range(1, terrainData.maxInSuccession);
        for (int i = 0; i < terrainInSuccession; i++)
        {
            GameObject terrain = Instantiate(terrainData.possibleTerrain[Random.Range(0, terrainData.possibleTerrain.Count)], currentPosition, Quaternion.identity, terrainHolder);
            currentTerrains.Add(terrain);
            currentPosition.x++; 
        }
    }

    public void SpawnTerrain(bool isStart, Vector3 playerPos)
    {

        if ((currentPosition.x - playerPos.x < minDistanceFromPlayer )|| (isStart)) 
        {
            int whichTerrain = Random.Range(0, terrainDatas.Count);
            int terrainInSuccession = Random.Range(1, terrainDatas[whichTerrain].maxInSuccession);
            for (int i = 0; i < terrainInSuccession; i++)
            {
                GameObject terrain = Instantiate(terrainDatas[whichTerrain].possibleTerrain[Random.Range(0, terrainDatas[whichTerrain].possibleTerrain.Count)], currentPosition, Quaternion.identity, terrainHolder);
                currentTerrains.Add(terrain);
                if (!isStart)
                {
                    if (currentTerrains.Count > maxTerrainCount)
                    {

                        Destroy(currentTerrains[0]);
                        currentTerrains.RemoveAt(0);
                    }
                }
                currentPosition.x++;
            }

        }

    }
}