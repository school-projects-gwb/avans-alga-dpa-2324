namespace BroadwayBB.Common.Entities.Attendees.PathFinder.Graph;

public class WeightedEdge
{
    public TileNodeWeightedDecorator Decorator { get; set; }
    public int Weight { get; set; }

    public WeightedEdge(TileNodeWeightedDecorator decorator, int weight)
    {
        Decorator = decorator;
        Weight = weight;
    }
}