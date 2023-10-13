namespace BroadwayBB.Common.Entities.Attendees.PathFinder.Graph;

public class WeightedEdge
{
    public TileNodeWeightedDecorator Decorator { get; }
    public int Weight { get; }

    public WeightedEdge(TileNodeWeightedDecorator decorator, int weight)
    {
        Decorator = decorator;
        Weight = weight;
    }
}