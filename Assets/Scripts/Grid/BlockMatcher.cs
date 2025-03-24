using UnityEngine;
using System.Collections.Generic;

public class BlockMatcher : MonoBehaviour
{
    public List<Block> Match(Block startBlock)
    {
        List<Block> matched = new();
        Queue<Block> queue = new();
        HashSet<Vector2Int> visited = new();

        int target = startBlock.Value;
        var grid = GameManager.Instance.GridManager;

        queue.Enqueue(startBlock);
        visited.Add(startBlock.GridPosition);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            matched.Add(current);

            foreach (var dir in GetDirections())
            {
                Vector2Int nextPos = current.GridPosition + dir;
                if (!visited.Contains(nextPos))
                {
                    Block neighbor = grid.GetBlock(nextPos);
                    if (neighbor != null && neighbor.Value == target)
                    {
                        queue.Enqueue(neighbor);
                        visited.Add(nextPos);
                    }
                }
            }
        }
        return matched;
    }

    private List<Vector2Int> GetDirections() => new()
    {
        Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
    };
}
