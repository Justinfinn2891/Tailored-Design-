using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public float cellSize = 1f;
    public GameObject gridCellPrefab;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 spawnPos = new Vector3(x * cellSize, 0, z * cellSize);
                Instantiate(gridCellPrefab, spawnPos, Quaternion.identity);
            }
        }
    }

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x / cellSize);
        int z = Mathf.RoundToInt(position.z / cellSize);
        return new Vector3(x * cellSize, .5f, z * cellSize);
    }
}