using System.Collections.Generic;
using UnityEngine;

public class FloodFill
{
    private int[,] map; // �� ������ (0: �̵� ����, 1: ��ֹ�)
    private int rows, cols;
    public float yOffset { get; private set; } = 0f;

    public FloodFill(int[,] mapData)
    {
        map = mapData;
        rows = map.GetLength(0);
        cols = map.GetLength(1);
    }

    public Vector3[] GeneratePath(Vector2Int start)
    {
        List<Vector3> path = new List<Vector3>();
        bool[,] visited = new bool[rows, cols];

        // Flood Fill ��� Ž��
        Vector2Int current = start;
        Stack<Vector2Int> stack = new Stack<Vector2Int>();
        stack.Push(current);

        while (stack.Count > 0)
        {
            current = stack.Pop();

            if (IsWalkable(current) && !visited[current.x, current.y])
            {
                // ���� ��ġ�� ��ο� �߰�
                path.Add(new Vector3(current.x, yOffset, current.y));
                visited[current.x, current.y] = true;

                // Ž�� ����: ��, ��, ��, �� (������ ��Ģ)
                foreach (var neighbor in GetNeighbors(current))
                {
                    if (IsWalkable(neighbor) && !visited[neighbor.x, neighbor.y])
                    {
                        stack.Push(neighbor);
                    }
                }
            }
        }

        // ��ȯ ��θ� ����� ���� ������ �߰�
        if (path.Count > 0)
        {
            path.Add(path[0]);
        }

        return path.ToArray();
    }

    private IEnumerable<Vector2Int> GetNeighbors(Vector2Int point)
    {
        return new List<Vector2Int>
        {
            new Vector2Int(point.x - 1, point.y), // ��
            new Vector2Int(point.x, point.y + 1), // ��
            new Vector2Int(point.x + 1, point.y), // ��
            new Vector2Int(point.x, point.y - 1)  // ��
        };
    }

    private bool IsWalkable(Vector2Int point)
    {
        return point.x >= 0 && point.x < rows &&
               point.y >= 0 && point.y < cols &&
               map[point.x, point.y] == 0;
    }
}
