using BroadwayBB.Common.Behaviors;

namespace BroadwayBB.Common.Helpers;


public sealed class WeightRegistryHelper
{
    private static readonly Lazy<WeightRegistryHelper> Instance = new(() => new WeightRegistryHelper());

    private readonly Dictionary<ColorName, int> _weightMap = new();

    private WeightRegistryHelper()
    {

    }

    public static WeightRegistryHelper GetInstance => Instance.Value;

    public int GetWeight(ColorName colorName)
    {
        lock (_weightMap)
            return _weightMap.TryGetValue(colorName, out var weight) ? weight : int.MaxValue;
    }

    public Dictionary<ColorName, int> GetAllWeights()
    {
        lock (_weightMap)
            return _weightMap;
    }

    public void RegisterWeight(ColorName colorName, int weight)
    {
        lock (_weightMap)
            _weightMap[colorName] = weight;
    }
}
