using System;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinder
{
    private int[,] map; // 맵 데이터 (0: 이동 가능, 1: 장애물)
    private int rows, cols;
    public float yOffset { get; private set; } = 0f;

    public AStarPathfinder(int[,] mapData)
    {
        map = mapData;
        rows = map.GetLength(0);
        cols = map.GetLength(1);
    }

    //순환형 Path
    public Vector3[] FindCircularPath(Vector2Int start, Vector2Int goal)
    {
        List<Vector2> pathList = FindPath(start, goal); // 기존 A* 경로 탐색

        if(pathList.Equals(null) || pathList.Count.Equals(0))
        {
            Debug.LogAssertion("Empty map");
            return new Vector3[0];
        }

        // 순환 경로 만들기
        if (pathList.Count > 1)
        {
            pathList.Add(pathList[0]); // 마지막에서 첫 번째로 돌아가는 연결
        }

        Vector3[] path = new Vector3[pathList.Count];
        for(int i = 0; i < pathList.Count; i++)
        {
            Vector2 node = pathList[i];
            path[i] = new Vector3(node.x, yOffset, node.y);
        }

        return path;
    }

    public List<Vector2> FindPath(Vector2Int start, Vector2Int goal)
    {
        PriorityQueue<Node> openSet = new PriorityQueue<Node>();
        HashSet<Vector2Int> closedSet = new HashSet<Vector2Int>();
        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();

        openSet.Enqueue(new Node(start, 0, Heuristic(start, goal)));

        Dictionary<Vector2Int, float> gScore = new Dictionary<Vector2Int, float> { [start] = 0 };

        while (openSet.Count > 0)
        {
            Node current = openSet.Dequeue();

            if (current.Position == goal)
            {
                return ReconstructPath(cameFrom, current.Position);
            }

            closedSet.Add(current.Position);

            foreach (Vector2Int neighbor in GetNeighbors(current.Position))
            {
                if (closedSet.Contains(neighbor) || !IsWalkable(neighbor))
                    continue;

                float tentativeGScore = gScore[current.Position] + 1;

                if (!gScore.ContainsKey(neighbor) || tentativeGScore < gScore[neighbor])
                {
                    cameFrom[neighbor] = current.Position;
                    gScore[neighbor] = tentativeGScore;
                    openSet.Enqueue(new Node(neighbor, tentativeGScore, tentativeGScore + Heuristic(neighbor, goal)));
                }
            }
        }

        return new List<Vector2>(); // 경로를 찾지 못한 경우
    }

    private List<Vector2> ReconstructPath(Dictionary<Vector2Int, Vector2Int> cameFrom, Vector2Int current)
    {
        List<Vector2> path = new List<Vector2> { current };

        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Add(current);
        }

        path.Reverse();
        return path;
    }

    private IEnumerable<Vector2Int> GetNeighbors(Vector2Int point)
    {
        return new List<Vector2Int>
        {
            new Vector2Int(point.x + 1, point.y),
            new Vector2Int(point.x - 1, point.y),
            new Vector2Int(point.x, point.y + 1),
            new Vector2Int(point.x, point.y - 1)
        };
    }

    private float Heuristic(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y); // Manhattan Distance
    }

    private bool IsWalkable(Vector2Int point)
    {
        return point.x >= 0 && point.x < rows &&
               point.y >= 0 && point.y < cols &&
               map[point.x, point.y] == 0;
    }

    private class Node : IComparable<Node>
    {
        public Vector2Int Position;
        public float GScore;
        public float FScore;

        public Node(Vector2Int position, float gScore, float fScore)
        {
            Position = position;
            GScore = gScore;
            FScore = fScore;
        }

        public int CompareTo(Node other)
        {
            return FScore.CompareTo(other.FScore);
        }
    }
}
