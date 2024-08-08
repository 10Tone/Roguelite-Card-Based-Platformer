using System;
using System.Collections.Generic;
using System.Linq;
using AutoLoads;
using Game.WorldBuilding;
using Godot;

public partial class LevelAnalyzer : Node
{
    private GlobalVariables _globalVariables;
    private Vector2 _goalPosition;
    private Vector2 _startPosition;

    public LevelAnalyzer(GlobalVariables globalVariables, Vector2 startPosition, Vector2 goalPosition)
    {
        _globalVariables = globalVariables;
        _startPosition = startPosition;
        _goalPosition = goalPosition;
    }

    private List<Vector2> FindShortestPath()
    {
        var openSet = new PriorityQueue<Vector2, float>();
        var cameFrom = new Dictionary<Vector2, Vector2>();
        var gScore = new Dictionary<Vector2, float>();
        var fScore = new Dictionary<Vector2, float>();

        openSet.Enqueue(_startPosition, 0);
        gScore[_startPosition] = 0;
        fScore[_startPosition] = HeuristicCostEstimate(_startPosition, _goalPosition);

        while (openSet.Count > 0)
        {
            var current = openSet.Dequeue();

            if (current == _goalPosition) return ReconstructPath(cameFrom, current);

            foreach (var neighbor in GetNeighbors(current))
            {
                var tentativeGScore = gScore[current] + DistanceBetween(current, neighbor);

                if (!gScore.ContainsKey(neighbor) || tentativeGScore < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + HeuristicCostEstimate(neighbor, _goalPosition);

                    if (!openSet.UnorderedItems.Any(x => x.Element == neighbor))
                        openSet.Enqueue(neighbor, fScore[neighbor]);
                }
            }
        }

        return new List<Vector2>(); // No path found
    }

    private List<Vector2> ReconstructPath(Dictionary<Vector2, Vector2> cameFrom, Vector2 current)
    {
        var path = new List<Vector2> { current };
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Add(current);
        }

        path.Reverse();
        return path;
    }

    private float HeuristicCostEstimate(Vector2 start, Vector2 goal)
    {
        return start.DistanceTo(goal);
    }

    private float DistanceBetween(Vector2 a, Vector2 b)
    {
        return a.DistanceTo(b);
    }

    private IEnumerable<Vector2> GetNeighbors(Vector2 position)
    {
        var neighbors = new List<Vector2>();

        // Regular movement (left, right)
        neighbors.Add(position + new Vector2(-1, 0) * 32);
        neighbors.Add(position + new Vector2(1, 0) * 32);

        // Jumping
        for (var x = -4; x <= 4; x++)
        for (var y = -2; y <= 0; y++)
            if (x != 0 || y != 0)
            {
                var jump = new Vector2(x, y) * 32;
                if (Math.Abs(x) <= 2 || y == 0) // Allow longer horizontal jumps when not ascending
                    neighbors.Add(position + jump);
            }

        // Filter out invalid positions (walls, out of bounds, etc.)
        return neighbors.Where(IsValidPosition);
    }

    private bool IsValidPosition(Vector2 position)
    {
        // Check if the position is within the grid and is a valid platform
        return _globalVariables.BuildedItems.ContainsKey(position) &&
               _globalVariables.BuildedItems[position] is IBuildItem buildItem &&
               buildItem.BuildItemResource.BuildItemType == BuildItemTypes.Platforms;
    }


    private int CountObstaclesOnPath(List<Vector2> path)
    {
        // Count obstacles encountered on the given path
        return 0;
    }

    private int CountTotalObstacles()
    {
        // Count total obstacles placed in the level
        return 0;
    }

    private bool IsShortcutDetected(int obstaclesEncountered, int totalObstacles)
    {
        // Implement logic to determine if a shortcut exists
        return false;
    }
}