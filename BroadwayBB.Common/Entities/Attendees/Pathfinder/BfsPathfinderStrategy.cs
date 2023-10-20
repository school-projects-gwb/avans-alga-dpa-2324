using System.Drawing;
using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Entities.Tiles;

namespace BroadwayBB.Common.Entities.Attendees.PathFinder;

public class BfsPathfinderStrategy : PathfinderStrategyBase
{
    public override void CalculatePath(List<TileNode> tileGraph, ITile start, ITile target)
    {
        PathCount = 0;   
        PathsChanged = true;
        Queue<ITile> queue = new Queue<ITile>();
        Dictionary<ITile, ITile> parentMap = new Dictionary<ITile, ITile>();
        List<ITile> visitedNodes = new List<ITile>();

        queue.Enqueue(start);
        visitedNodes.Add(start);

        while (queue.Count > 0)
        {
            ITile currentTile = queue.Dequeue();

            foreach (ITile neighbor in GetFilteredNeighbors(tileGraph, currentTile))
            {
                if (visitedNodes.Contains(neighbor)) continue;

                queue.Enqueue(neighbor);
                visitedNodes.Add(neighbor);
                parentMap[neighbor] = currentTile;

                if (neighbor != target) continue;

                List<ITile> shortestPath = ReconstructPath(parentMap, target);
                CurrentPath = (new List<List<ITile>> {shortestPath}, visitedNodes);
                Console.WriteLine("---bfs---");
                ShowPathWeight();
                return;
            }
        }
        
        CurrentPath = (new(), visitedNodes);
    }
    
    private IEnumerable<ITile> GetFilteredNeighbors(List<TileNode> tileGraph, ITile tile)
    {
        return tileGraph
            .First(node => node.Tile == tile)
            .Neighbors
            .Select(node => node.Tile)
            .Where(neighbor => neighbor.ColorBehaviorStrategy.ColorName != ColorName.White);
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
}