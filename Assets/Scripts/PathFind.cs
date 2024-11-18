using System.Numerics;
using UnityEngine;

public class PathFind
{
    private int[,] map; // 맵 데이터 (0: 이동 가능, 1: 장애물)
    private int rows, cols;
    public float yOffset { get; private set; } = 0f;

    public PathFind(int[,] mapData)
    {
        map = mapData;
        rows = map.GetLength(0);
        cols = map.GetLength(1);
    }

    public void PathFinder(Vector2Int start)
    {

    }
}