using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Behaviors.Interfaces;
using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Structures;
using BroadwayBB.Common.Entities.Tiles;
using BroadwayBB.Data.Factories.Interfaces;
using BroadwayBB.Data.Strategies.Interfaces;

namespace BroadwayBB.Data.Factories;

public class TileFactory : ITileFactory
{
    private const string ColorBehaviorStrategyClassName = "ColorBehaviorStrategy";
    private readonly Dictionary<string, Type> ColorBehaviorStrategies = new Dictionary<string, Type>();

    public TileFactory()
    {
        var interfaceType = typeof(IColorBehaviorStrategy);
        var implementingTypes = AppDomain.CurrentDomain.GetAssemblies()
                                .SelectMany(a => a.GetTypes())
                                .Where(type => interfaceType.IsAssignableFrom(type) &&
                                                !type.IsAbstract &&
                                                !type.IsInterface);

        foreach (var type in implementingTypes)
        {
            string name = type.Name.Replace(ColorBehaviorStrategyClassName, "").ToLower();
            ColorBehaviorStrategies[name] = type;
        }
    }

    public ITile Create(ColorName name, Coords coords)
    {
        if (!ColorBehaviorStrategies.TryGetValue(name.ToString().ToLower(), out Type? colorBehaviour))
        {
            colorBehaviour = typeof(NullColorBehaviorStrategy);
        }

        var strategy = (IColorBehaviorStrategy?)Activator.CreateInstance(colorBehaviour);
        strategy ??= new NullColorBehaviorStrategy();

        return new Tile(coords, strategy);
    }
}