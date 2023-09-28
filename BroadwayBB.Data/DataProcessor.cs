using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Interfaces;
using BroadwayBB.Data.Factories;
using BroadwayBB.Data.Factories.Interfaces;

namespace BroadwayBB.Data;

public class DataProcessor
{
    public Museum BuildMuseumFromFile(string filePath)
    {
        Museum museum = new Museum();
        List<IAttendee> artists = new List<IAttendee>();

        ITileFactory tileFactory = new TileFactory();
        IAttendeeFactory attendeeFactory = new AttendeeFactory();

        char[] colors = { 'R', 'B', '_', 'Y', 'G' };
        var random = new Random();
        int colRowAmount = 60;

        List<TileNode> nodes = new List<TileNode>();

        for (int y = 0; y < colRowAmount; y++)
        {
            for (int x = 0; x < colRowAmount; x++)
            {
                var tile = tileFactory.Create(y, x, colors[random.Next(colors.Length)]);
                var node = new TileNode(tile);
                
                nodes.Add(node);
                ConnectOrthogonalNeighbors(node, nodes);
            }
        }
        
        artists.Add(attendeeFactory.Create(2.5, 3, 0, 2.5));
        artists.Add(attendeeFactory.Create(2, 2, 2, 0));
        artists.Add(attendeeFactory.Create(1, 3, 0, 2));
        artists.Add(attendeeFactory.Create(2.5, 3, 0, 1));
        
        museum.Attendees = artists;
        museum.Tiles = nodes;
        
        return museum;
    }
    
    List<(int posX, int posY)> neighborOffsets = new List<(int posX, int posY)>
    {
        (-1, 0), (1, 0), (0, -1), (0, 1)
    };

    // Function to connect orthogonal neighbors
    void ConnectOrthogonalNeighbors(TileNode currentNode, List<TileNode> allNodes)
    {
        int posX = currentNode.Tile.PosX;
        int posY = currentNode.Tile.PosY;

        foreach (var offset in neighborOffsets)
        {
            int neighborX = posX + offset.posX;
            int neighborY = posY + offset.posY;
            
            var neighborNode = allNodes.FirstOrDefault(node =>
                node.Tile.PosX == neighborX && node.Tile.PosY == neighborY);

            if (neighborNode == null) continue;
            
            currentNode.Neighbors.Add(neighborNode);
            neighborNode.Neighbors.Add(currentNode);
        }
    }

}