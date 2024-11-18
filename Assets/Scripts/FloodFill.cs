using System.Collections.Generic;
using UnityEngine;

public class FloodFill
{
    private int[,] map; // 맵 데이터 (0: 이동 가능, 1: 장애물)
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

        // Flood Fill 기반 탐색
        Vector2Int current = start;
        Stack<Vector2Int> stack = new Stack<Vector2Int>();
        stack.Push(current);

        while (stack.Count > 0)
        {
            current = stack.Pop();

            if (IsWalkable(current) && !visited[current.x, current.y])
            {
                // 현재 위치를 경로에 추가
                path.Add(new Vector3(current.x, yOffset, current.y));
                visited[current.x, current.y] = true;

                // 탐색 순서: 상, 우, 하, 좌 (오른손 법칙)
                foreach (var neighbor in GetNeighbors(current))
                {
                    if (IsWalkable(neighbor) && !visited[neighbor.x, neighbor.y])
                    {
                        stack.Push(neighbor);
                    }
                }
            }
        }

        // 순환 경로를 만들기 위해 시작점 추가
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
            new Vector2Int(point.x - 1, point.y), // 좌
            new Vector2Int(point.x, point.y + 1), // 하
            new Vector2Int(point.x + 1, point.y), // 우
            new Vector2Int(point.x, point.y - 1)  // 상
        };
    }

    private bool IsWalkable(Vector2Int point)
    {
        return point.x >= 0 && point.x < rows &&
               point.y >= 0 && point.y < cols &&
               map[point.x, point.y] == 0;
    }
}
