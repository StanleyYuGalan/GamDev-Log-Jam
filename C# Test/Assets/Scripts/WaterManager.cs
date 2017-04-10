using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterManager : MonoBehaviour
{
    public enum Direction
    {
        NORTH, SOUTH, EAST, WEST
    }

    public GameObject[] tilePrefabs;
    // Use this for initialization

    private Transform playerTransform;
    private float spawnX = -200;
    private float spawnZ = -100;
    private float tileLength = 200.0f;
    private int amnTilesOnScreen = 4;
    private int lastPrefabIndex = -1;
    private List<GameObject> activeTiles;
    private float safeZone = 200.0f;

    private float animationDuration = 2f;
    private Direction direction;

    void Start ()
    {
        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < amnTilesOnScreen; i++)
        {
            if (i < 2)
            {
                direction = Direction.NORTH;
                SpawnTile(0);
            }
            else
            {
                SpawnTile();
            }

            // SpawnTile(i + 3);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (Time.time > animationDuration)
        {
            print(playerTransform.position.z - safeZone);
            print(spawnX - amnTilesOnScreen * tileLength);
            if (playerTransform.position.x - safeZone > (spawnX - amnTilesOnScreen * tileLength))
            {
                SpawnTile();
                DeleteTile();
            }
        }
    }

    void SpawnTile ( int prefabIndex = -1 )
    {
        GameObject go;

        if (prefabIndex == -1)
        {
            go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        }
        else
        {
            go = Instantiate(tilePrefabs[prefabIndex]) as GameObject;
        }

        /*go.transform.SetParent(transform);

        if (direction == Direction.NORTH)
        {
            spawnX += tileLength;
        }
        else if (direction == Direction.SOUTH)
        {
            spawnX -= tileLength;
        }
        else if (direction == Direction.EAST)
        {
            spawnZ -= tileLength;
        }
        else if (direction == Direction.WEST)
        {
            spawnZ += tileLength;
        }

        go.transform.position = (Vector3.right * spawnX) + (Vector3.up * -16) + (Vector3.forward * spawnZ);

        Debug.Log(go.transform.position + "");*/
        
        go.transform.SetParent(transform);
        go.transform.position = Vector3.right * spawnX + Vector3.up * -16 + Vector3.forward * -100;

        spawnX += tileLength;
        activeTiles.Add(go);
    }

    void DeleteTile ()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private int RandomPrefabIndex ()
    {
        if (tilePrefabs.Length <= 1)
        {
            return 0;
        }

        int randomIndex = lastPrefabIndex;

        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);

            // 0 - Straight Horizontal
            // 1 - Straight Horizontal 2
            // 2 - Straight Horizontal 3
            // 3 - Upper Left
            // 4 - Lower Left
            // 5 - Lower Right
            // 6 - Upper Right
            // 7 - Straight Vertical

            switch (lastPrefabIndex)
            {
                case 0:
                case 1:
                case 2:

                    if (direction == Direction.NORTH)
                    {
                        if (randomIndex == 0 || randomIndex == 1 || randomIndex == 2)
                        {
                            direction = Direction.NORTH;
                        }
                        else if (randomIndex == 4)
                        {
                            direction = Direction.WEST;
                        }
                        else if (randomIndex == 5)
                        {
                            direction = Direction.EAST;
                        }
                        else
                        {
                            randomIndex = lastPrefabIndex;
                        }
                    }
                    else if (direction == Direction.SOUTH)
                    {
                        if (randomIndex == 0 || randomIndex == 1 || randomIndex == 2)
                        {
                            direction = Direction.SOUTH;
                        }
                        else if (randomIndex == 3)
                        {
                            direction = Direction.WEST;
                        }
                        else if (randomIndex == 6)
                        {
                            direction = Direction.EAST;
                        }
                        else
                        {
                            randomIndex = lastPrefabIndex;
                        }
                    }
                    break;

                case 3:

                    if (direction == Direction.NORTH)
                    {
                        if (randomIndex == 0 || randomIndex == 1 || randomIndex == 2)
                        {
                            direction = Direction.NORTH;
                        }
                        else if (randomIndex == 4)
                        {
                            direction = Direction.WEST;
                        }
                        else if (randomIndex == 5)
                        {
                            direction = Direction.EAST;
                        }
                        else
                        {
                            randomIndex = lastPrefabIndex;
                        }
                    }
                    else if (direction == Direction.WEST)
                    {
                        if (randomIndex == 7)
                        {
                            direction = Direction.WEST;
                        }
                        else if (randomIndex == 5)
                        {
                            direction = Direction.SOUTH;
                        }
                        else if (randomIndex == 6)
                        {
                            direction = Direction.NORTH;
                        }
                        else
                        {
                            randomIndex = lastPrefabIndex;
                        }
                    }
                    break;

                case 4:

                    if (direction == Direction.SOUTH)
                    {
                        if (randomIndex == 0 || randomIndex == 1 || randomIndex == 2)
                        {
                            direction = Direction.SOUTH;
                        }
                        else if (randomIndex == 3)
                        {
                            direction = Direction.WEST;
                        }
                        else if (randomIndex == 6)
                        {
                            direction = Direction.EAST;
                        }
                        else
                        {
                            randomIndex = lastPrefabIndex;
                        }
                    }
                    else if (direction == Direction.WEST)
                    {
                        if (randomIndex == 7)
                        {
                            direction = Direction.WEST;
                        }
                        else if (randomIndex == 5)
                        {
                            direction = Direction.SOUTH;
                        }
                        else if (randomIndex == 6)
                        {
                            direction = Direction.NORTH;
                        }
                        else
                        {
                            randomIndex = lastPrefabIndex;
                        }
                    }
                    break;

                case 5:

                    if (direction == Direction.SOUTH)
                    {
                        if (randomIndex == 0 || randomIndex == 1 || randomIndex == 2)
                        {
                            direction = Direction.SOUTH;
                        }
                        else if (randomIndex == 3)
                        {
                            direction = Direction.WEST;
                        }
                        else if (randomIndex == 6)
                        {
                            direction = Direction.EAST;
                        }
                        else
                        {
                            randomIndex = lastPrefabIndex;
                        }
                    }
                    else if (direction == Direction.EAST)
                    {
                        if (randomIndex == 7)
                        {
                            direction = Direction.EAST;
                        }
                        else if (randomIndex == 3)
                        {
                            direction = Direction.NORTH;
                        }
                        else if (randomIndex == 4)
                        {
                            direction = Direction.SOUTH;
                        }
                        else
                        {
                            randomIndex = lastPrefabIndex;
                        }
                    }
                    break;

                case 6:

                    if (direction == Direction.NORTH)
                    {
                        if (randomIndex == 0 || randomIndex == 1 || randomIndex == 2)
                        {
                            direction = Direction.NORTH;
                        }
                        else if (randomIndex == 4)
                        {
                            direction = Direction.WEST;
                        }
                        else if (randomIndex == 5)
                        {
                            direction = Direction.EAST;
                        }
                        else
                        {
                            randomIndex = lastPrefabIndex;
                        }
                    }
                    else if (direction == Direction.EAST)
                    {
                        if (randomIndex == 7)
                        {
                            direction = Direction.EAST;
                        }
                        else if (randomIndex == 3)
                        {
                            direction = Direction.NORTH;
                        }
                        else if (randomIndex == 4)
                        {
                            direction = Direction.SOUTH;
                        }
                        else
                        {
                            randomIndex = lastPrefabIndex;
                        }
                    }
                    break;

                case 7:

                    if (direction == Direction.EAST)
                    {
                        if (randomIndex == 7)
                        {
                            direction = Direction.EAST;
                        }
                        else if (randomIndex == 3)
                        {
                            direction = Direction.NORTH;
                        }
                        else if (randomIndex == 4)
                        {
                            direction = Direction.SOUTH;
                        }
                        else
                        {
                            randomIndex = lastPrefabIndex;
                        }
                    }
                    else if (direction == Direction.WEST)
                    {
                        if (randomIndex == 7)
                        {
                            direction = Direction.WEST;
                        }
                        else if (randomIndex == 6)
                        {
                            direction = Direction.NORTH;
                        }
                        else if (randomIndex == 5)
                        {
                            direction = Direction.SOUTH;
                        }
                        else
                        {
                            randomIndex = lastPrefabIndex;
                        }
                    }
                    break;
            }
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;
    }
}
