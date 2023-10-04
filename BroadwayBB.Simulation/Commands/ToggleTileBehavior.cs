using BroadwayBB.Common.Entities.Museum;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Simulation.Commands;

public class ToggleTileBehavior : ICommand
{
    public void HandleCommand(IMuseumSimulation museumSimulation, MouseGridPosition mouseGridPosition)
    {
        museumSimulation.ToggleConfigValue(ConfigType.ShouldHaveTileBehavior);
    }
}