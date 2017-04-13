using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManager : MonoBehaviour
{

    public GameObject[] tilePrefabs;
    
    private Transform playerTransform;
    private float spawnZ = -5.0f;
    private float tileLength = 50.0f;
    private int amnTilesOnScreen = 5;
    private List<GameObject> activeTiles;
    private int rand;
    private float safeZone = 50.0f;


    private float spawnX = 1.0f;
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
    // public void Update1()
    //  {
    //      {
    //spawnX = GameObject.Find("Tiles").GetComponent<WaterManager>().getX();
    //spawnZ = GameObject.Find("Tiles").GetComponent<WaterManager>().getZ();
    //       SpawnTile();

    ////     DeleteTile();
    //  }
    //  }

    int interval = 1;
    float nextTime = 0;


    public void Update()
    {
        //  if (playerTransform.position.z - safeZone > (spawnZ - amnTilesOnScreen * tileLength))
        //  {
        //spawnX = GameObject.Find("Tiles").GetComponent<WaterManager>().getX();
        //spawnZ = GameObject.Find("Tiles").GetComponent<WaterManager>().getZ();
        //     SpawnTile();

        //       DeleteTile();
        //   }

        if (Time.time >= nextTime)
        {

            SpawnTile();
            nextTime += interval;

        }

    }

    void SpawnTile(int prefabIndex = -1)
    {
        GameObject go;
        go = Instantiate(tilePrefabs[UnityEngine.Random.Range(0, 3)]) as GameObject;
        go.transform.SetParent(transform);

        rand = UnityEngine.Random.Range(1, 3);

        switch (rand)
        {
            case 1:
                //go.transform.position = Vector3.right * (spawnX + 130) + Vector3.forward * spawnZ + Vector3.up *30;
                Vector3 playerPos = playerTransform.transform.position;
                Vector3 playerDirection = playerTransform.transform.up;
                Quaternion playerRotation = playerTransform.transform.rotation;
                float spawnDistance = -1 * UnityEngine.Random.Range(100, 200); 

                Vector3 spawnPos = playerPos + playerDirection * spawnDistance + Vector3.up * -5; 
                print(go.transform.position);
                
                Instantiate(go, spawnPos, playerRotation);
                break;

         /*   case 2:
                //  go.transform.position = Vector3.right * (spawnX + 130) + Vector3.forward * spawnZ + Vector3.up * 30;
                go.transform.position = playerTransform.transform.position + (playerTransform.transform.forward * 10);

                print("Position");
                print(go.transform.position);
                break;
            case 3:
                //go.transform.position = Vector3.right * (spawnX + 130) + Vector3.forward * spawnZ + Vector3.up * 30;
                go.transform.position = playerTransform.transform.position + (playerTransform.transform. * 10);

                print("Position");
                print(go.transform.position);
                break;
                */
        }

        spawnZ += tileLength;
        activeTiles.Add(go);
    }

    void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    public void InitializePlayerCoordinate(float spawnXNew, float spawnZNew)
    {
        spawnX = spawnXNew;
        spawnZ = spawnZNew;
    }

}
