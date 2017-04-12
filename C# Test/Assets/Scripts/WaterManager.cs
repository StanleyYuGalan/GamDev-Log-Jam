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
    
    private Transform playerTransform;
    private float spawnX = -200;
    private float spawnZ = -100;
    private float tileLength = 200.0f;
    private int amnTilesOnScreen = 4;
    private int lastPrefabIndex = -1;
    private List<GameObject> activeTiles;
    private List<float> tileLocation;
    private List<Direction> tileExit;
    private float safeZone = 30.0f;

    private float animationDuration = 2f;
    private Direction direction;
    private Direction exit;

    // Use this for initialization
    void Start ()
    {
        activeTiles = new List<GameObject>();
        tileLocation = new List<float>();
        tileExit = new List<Direction>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < amnTilesOnScreen; i++)
        {
            if (i < 2)
            {
                SpawnTile(0);
            }
            else
            {
                SpawnTile();
            }
            // Debug.Log(i + " (" + spawnX + ", " + spawnZ + ") - " + exit + " " + direction + "");
            // SpawnTile(i + 3);
            // Debug.Log(tileLocation[i]);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (Time.time > animationDuration)
        {
            // print(playerTransform.position.z - safeZone);
            // print(spawnX - amnTilesOnScreen * tileLength);

            if (tileExit[1] == Direction.NORTH && playerTransform.position.x > tileLocation[1] + safeZone ||
                tileExit[1] == Direction.SOUTH && playerTransform.position.x < tileLocation[1] + tileLength - safeZone ||
                tileExit[1] == Direction.EAST && playerTransform.position.z < tileLocation[1] + tileLength - safeZone ||
                tileExit[1] == Direction.WEST && playerTransform.position.z > tileLocation[1] + safeZone)
            {
                Debug.Log("IN IF: " + playerTransform.position + " " + tileLocation[1] + " " + tileExit[1] + "");

                SpawnTile();
                DeleteTile();
            }
        }
    }

    void SpawnTile ( int prefabIndex = -1 )
    {
        GameObject go;

        exit = direction;

        if (prefabIndex == -1)
        {
            go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        }
        else
        {
            if (prefabIndex == 0)
            {
                direction = Direction.NORTH;
            }

            lastPrefabIndex = prefabIndex;
            go = Instantiate(tilePrefabs[prefabIndex]) as GameObject;
        }

        go.transform.SetParent(transform);

        if (exit == Direction.NORTH)
        {
            spawnX += tileLength;
            tileLocation.Add(spawnX);
        }
        else if (exit == Direction.SOUTH)
        {
            spawnX -= tileLength;
            tileLocation.Add(spawnX);
        }
        else if (exit == Direction.EAST)
        {
            spawnZ -= tileLength;
            tileLocation.Add(spawnZ);
        }
        else if (exit == Direction.WEST)
        {
            spawnZ += tileLength;
            tileLocation.Add(spawnZ);
        }

        tileExit.Add(exit);

        go.transform.position = (Vector3.right * spawnX) + (Vector3.up * -16) + (Vector3.forward * spawnZ);

        Debug.Log("( " + go.transform.position.x + ", " + go.transform.position.z + ") - E:" + exit + " D:" + direction);
        // Debug.Log(go.transform.position + "");

        /*go.transform.SetParent(transform);
        go.transform.position = Vector3.right * spawnX + Vector3.up * -16 + Vector3.forward * -100;

        spawnX += tileLength;*/
        activeTiles.Add(go);
    }

    void DeleteTile ()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
        tileLocation.RemoveAt(0);
        tileExit.RemoveAt(0);
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
