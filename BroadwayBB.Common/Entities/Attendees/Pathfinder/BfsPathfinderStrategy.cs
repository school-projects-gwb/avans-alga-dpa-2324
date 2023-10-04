using System.Drawing;
using BroadwayBB.Common.Entities.Tiles;

namespace BroadwayBB.Common.Entities.Attendees.PathFinder;

public class BfsPathfinderStrategy : IPathfinderStrategy
{
    public (List<ITile> shortestPath, List<ITile> visitedNodes) CalculatePath(List<TileNode> tileGraph, ITile start, ITile target)
    {
        Queue<ITile> queue = new Queue<ITile>();
        Dictionary<ITile, ITile> parentMap = new Dictionary<ITile, ITile>();
        List<ITile> visitedNodes = new List<ITile>();

        queue.Enqueue(start);
        visitedNodes.Add(start);

        while (queue.Count > 0)
        {
            ITile currentTile = queue.Dequeue();

            if (currentTile == target)
            {
                List<ITile> shortestPath = ReconstructPath(parentMap, target);
                return (shortestPath, visitedNodes);
            }

            foreach (ITile neighbor in GetNeighbors(tileGraph, currentTile))
            {
                if (visitedNodes.Contains(neighbor)) continue;
                queue.Enqueue(neighbor);
                visitedNodes.Add(neighbor);
                parentMap[neighbor] = currentTile;
            }
        }
        
        return (null, visitedNodes);
    }

    private List<ITile> ReconstructPath(Dictionary<ITile, ITile> parentMap, ITile target)
    {
        List<ITile> path = new List<ITile>();
        ITile currentTile = target;

        while (parentMap.ContainsKey(currentTile))
        {
            path.Insert(0, currentTile);
            currentTile = parentMap[currentTile];
        }

        path.Insert(0, currentTile);
        return path;
    }

    private IEnumerable<ITile> GetNeighbors(List<TileNode> tileGraph, ITile tile)
    {
        return tileGraph
            .First(node => node.Tile == tile)
            .Neighbors
            .Select(node => node.Tile);
    }

    public List<Rectangle> GetDebugInfo()
    {
        throw new NotImplementedException();
    }
}