using System.Numerics;
using UnityEngine;

public class PathFind
{
    private int[,] map; // �� ������ (0: �̵� ����, 1: ��ֹ�)
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