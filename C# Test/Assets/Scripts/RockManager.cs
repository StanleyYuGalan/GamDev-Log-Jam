using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManager : MonoBehaviour
{

    public GameObject[] tilePrefabs;
    
    private Transform playerTransform;
    private float spawnZ = -5.0f;
    private float tileLength = 50.0f;
    private int amnTilesOnScreen = 20;
    private List<GameObject> activeTiles;
    private int rand;
    private float safeZone = 50.0f;

    // Use this for initialization
    void Start ()
    {
        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < amnTilesOnScreen; i++)
        {
            SpawnTile();

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z - safeZone > (spawnZ - amnTilesOnScreen * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
    }

    void SpawnTile(int prefabIndex = -1)
    {
        GameObject go;
        go = Instantiate(tilePrefabs[0]) as GameObject;
        go.transform.SetParent(transform);

        rand = UnityEngine.Random.Range(1, 3);

        switch (rand)
        {
            case 1:
                go.transform.position = Vector3.right * spawnZ + Vector3.up * 180 + Vector3.forward * 130;
                break;
            case 2:
                go.transform.position = Vector3.right * spawnZ + Vector3.up * 180 + Vector3.forward * 125;
                break;
            case 3:
                go.transform.position = Vector3.right * spawnZ + Vector3.up * 180 + Vector3.forward * 140;
                break;
        }

        spawnZ += tileLength;
        activeTiles.Add(go);
    }

    void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

}
