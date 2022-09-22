using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> Children;
    private Tilemap tilemap1, tilemap2, tilemap3, tilemap4;

    // I am not proud of this file. It is like a child I would keep in the basement.

    [SerializeField]
    private Tile[] tiles;

    int offset = -20;

    int[,] levelMap =
        {
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
        };

    void Start()
    {
        tilemap1 = gameObject.transform.Find("ManualLevel").transform.Find("Quadrant1").GetComponent<Tilemap>();
        tilemap2 = gameObject.transform.Find("ManualLevel2").transform.Find("Quadrant2").GetComponent<Tilemap>();
        tilemap3 = gameObject.transform.Find("ManualLevel3").transform.Find("Quadrant3").GetComponent<Tilemap>();
        tilemap4 = gameObject.transform.Find("ManualLevel4").transform.Find("Quadrant4").GetComponent<Tilemap>();

        tilemap1.ClearAllTiles();
        tilemap2.ClearAllTiles();
        tilemap3.ClearAllTiles();
        tilemap4.ClearAllTiles();

        int n1, n2, n3, n4;

        // Set Quadrant 1 Tiles.
        for (int i = 0; i < 15; i++)
        {
            for (int j = 0; j < 14; j++)
            {
                if (levelMap[i, j] != 0)
                {
                    n1 = (i > 0) ? levelMap[i-1, j]: -1;
                    n2 = (j < 13) ? levelMap[i, j+1]: -1;
                    n3 = (i < 14) ? levelMap[i+1, j]: -1;
                    n4 = (j > 0) ? levelMap[i, j-1]: -1;
                    int[] neighbours = { n1, n2, n3, n4 };
                    int n = calculateRotation(levelMap[i, j], neighbours);
                    Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, n), Vector3.one);

                    if (levelMap[i, j] == 5) // Scale sprite for pellet.
                        matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, n), new Vector3(0.5f, 0.5f, 1));

                    if (levelMap[i, j] == 6) // Scale sprite for Power pellet.
                        matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, n), new Vector3(0.8f, 0.8f, 1));

                    if (levelMap[i, j] == 7) // If T-Junction, we may have to mirror it depending on the position configuration.
                    {
                        if (n < 0)
                        {
                            matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, -n), new Vector3(1.0f, -1.0f, 1.0f));
                        }
                        else
                        {
                            matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, n), new Vector3(1.0f, 1.0f, 1));
                        }
                    }
                    tilemap1.SetTile(new Vector3Int(j - 1, offset + 15 - i + 1, 0), tiles[levelMap[i, j]]);
                    tilemap1.SetTransformMatrix(new Vector3Int(j - 1, offset + 15 - i + 1, 0), matrix);
                    Debug.Log(levelMap[i, j] + " " + n);
                }
            }
        }

        // Set Quadrant 2 Tiles.
        for (int i = 0; i < 15; i++)
        {
            for (int j = 13; j >= 0; j--)
            {
                if (levelMap[i, j] != 0)
                {
                    n1 = (i > 0) ? levelMap[i - 1, j] : -1;
                    n2 = (j < 13) ? levelMap[i, j + 1] : -1;
                    n3 = (i < 14) ? levelMap[i + 1, j] : -1;
                    n4 = (j > 0) ? levelMap[i, j - 1] : -1;
                    int[] neighbours = { n1, n2, n3, n4 };
                    int n = calculateRotation(levelMap[i, j], neighbours);
                    Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, n), Vector3.one);

                    if (levelMap[i, j] == 5)
                        matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, n), new Vector3(0.5f, 0.5f, 1));

                    if (levelMap[i, j] == 6)
                        matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, n), new Vector3(0.8f, 0.8f, 1));

                    if (levelMap[i, j] == 7)
                    {
                        if (n < 0)
                        {
                            matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, -n), new Vector3(1.0f, -1.0f, 1));
                        }
                        else
                        {
                            matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, n), new Vector3(1.0f, 1.0f, 1));
                        }
                    }
                    tilemap2.SetTile(new Vector3Int(j - 1, offset + 15 - i + 1, 0), tiles[levelMap[i, j]]);
                    tilemap2.SetTransformMatrix(new Vector3Int(j - 1, offset + 15 - i + 1, 0), matrix);
                }

            }
        }

        // Set Quadrant 3 Tiles.
        for (int i = 14; i >= 0; i--)
        {
            for (int j = 0; j < 14; j++)
            {
                if (levelMap[i, j] != 0)
                {
                    n1 = (i > 0) ? levelMap[i - 1, j] : -1;
                    n2 = (j < 13) ? levelMap[i, j + 1] : -1;
                    n3 = (i < 14) ? levelMap[i + 1, j] : -1;
                    n4 = (j > 0) ? levelMap[i, j - 1] : -1;
                    int[] neighbours = { n1, n2, n3, n4 };
                    int n = calculateRotation(levelMap[i, j], neighbours);
                    Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, n), Vector3.one);

                    if (levelMap[i, j] == 5)
                        matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, n), new Vector3(0.5f, 0.5f, 1));

                    if (levelMap[i, j] == 6)
                        matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, n), new Vector3(0.8f, 0.8f, 1));

                    if (levelMap[i, j] == 7)
                    {
                        if (n < 0)
                        {
                            matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, -n), new Vector3(1.0f, -1.0f, 1));
                        }
                        else
                        {
                            matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, n), new Vector3(1.0f, 1.0f, 1));
                        }
                    }

                    tilemap3.SetTile(new Vector3Int(j - 1, offset + 15 - i + 1, 0), tiles[levelMap[i, j]]);
                    tilemap3.SetTransformMatrix(new Vector3Int(j - 1, offset + 15 - i + 1, 0), matrix);
                }
            }
        }

        // Set Quadrant 4 Tiles.
        for (int i = 14; i >= 0; i--)
        {
            for (int j = 13; j >= 0; j--)
            {
                if (levelMap[i, j] != 0)
                {
                    n1 = (i > 0) ? levelMap[i - 1, j] : -1;
                    n2 = (j < 13) ? levelMap[i, j + 1] : -1;
                    n3 = (i < 14) ? levelMap[i + 1, j] : -1;
                    n4 = (j > 0) ? levelMap[i, j - 1] : -1;
                    int[] neighbours = { n1, n2, n3, n4 };
                    int n = calculateRotation(levelMap[i, j], neighbours);
                    Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, n), Vector3.one);

                    if (levelMap[i, j] == 5)
                        matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, n), new Vector3(0.5f, 0.5f, 1));

                    if (levelMap[i, j] == 6)
                        matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, n), new Vector3(0.8f, 0.8f, 1));

                    if (levelMap[i, j] == 7)
                    {
                        if (n < 0)
                        {
                            matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, -n), new Vector3(1.0f, -1.0f, 1));
                        }
                        else
                        {
                            matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, n), new Vector3(1.0f, 1.0f, 1));
                        }
                    }

                    tilemap4.SetTile(new Vector3Int(j - 1, offset + 15 - i + 1, 0), tiles[levelMap[i, j]]);
                    tilemap4.SetTransformMatrix(new Vector3Int(j - 1, offset + 15 - i + 1, 0), matrix);
                }
            }
        }
    }

    int calculateRotation(int tile, int[] neighbours)
    {
        switch (tile)
        {
            case 1: //Outside Corner.
                if (neighbours[0] == 2 || neighbours[0] == 1)
                {
                    if (neighbours[1] == 2 || neighbours[1] == 1)
                        return 90;
                    return 180;
                }
                if (neighbours[2] == 2 || neighbours[2] == 1)
                {
                    if (neighbours[1] == 2 || neighbours[1] == 1)
                        return 0;
                    return 270;
                }
                break;

            case 2: //Outside Wall, either 0 or 90. Check if there is another 1/2/7 as one of the neighbours.
                if (neighbours[0] == 1 || neighbours[0] == 2 || neighbours[0] == 7 || (neighbours[1] <= 0 && neighbours[3] <= 0))
                {
                    if (neighbours[2] == 1 || neighbours[2] == 2 || neighbours[2] == 7 || (neighbours[1] <= 0 && neighbours[3] <= 0))
                    {
                        return 0;
                    }

                }
                return 90;
   
            case 3: //Inside Corner, either 0, 90, 180, 270. Dependant on the position of the 2 adjacent walls.

                // Handle the corners that are next to more than 2 walls.
                if (neighbours[0] == 4 && neighbours[1] == 4) return 90;
                if (neighbours[1] == 4 && neighbours[2] == 4) return 0;
                if (neighbours[2] == 4 && neighbours[3] == 4) return 270;
                if (neighbours[3] == 4 && neighbours[0] == 4) return 180;

                if (neighbours[0] == 3 || neighbours[0] == 4 || neighbours[0] == 7 || neighbours[0] == -1)
                {
                    if (neighbours[1] == 3 || neighbours[1] == 4 || neighbours[1] == 7 || neighbours[1] == -1)
                    {
                        return 90;
                    }
                    return 180;
                }

                if (neighbours[2] == 3 || neighbours[2] == 4 || neighbours[0] == 7 || neighbours[2] == -1)
                {
                    if (neighbours[1] == 3 || neighbours[1] == 4 || neighbours[1] == 7 || neighbours[1] == -1)
                    {
                        return 0;
                    }
                    return 270;
                }
                break;

            case 4: //Inside Wall, either 0 or 90. Check if there is another 3/4/7 as one of the neighbours.
                if (neighbours[0] == 3 || neighbours[0] == 4 || neighbours[0] == 7 || (neighbours[1] <= 0 && neighbours[3] <= 0))
                {
                    if (neighbours[2] == 3 || neighbours[2] == 4 || neighbours[2] == 7 || (neighbours[1] <= 0 && neighbours[3] <= 0))
                    {
                        return 0;
                    }
                }
                return 90;

            case 7: //T-Junction. Only occurs on the outside wall. Will connect with an outside wall (2) and an inside wall or corner (3/4)
                if (neighbours[0] == -1)
                {
                    if (neighbours[3] == 7)  // Check if the left neighbour is a T-Junction, as we will have to mirror it. Return an encoding.
                    {
                        return -270;
                    }
                    return 270;
                }
                if (neighbours[3] == -1) //Shouldnt really have to worry about this since the top quadrant is always mirrored, but anyway
                {
                    if (neighbours[0] == 7)  // Check if the left neighbour is a T-Junction, as we will have to mirror it. Return an encoding.
                    {
                        return -180;
                    }
                    return 180;
                }

                break;
        }
        return 0;
    }
}
