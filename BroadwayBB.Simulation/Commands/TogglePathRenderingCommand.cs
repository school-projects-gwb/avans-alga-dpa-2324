using BroadwayBB.Common.Entities.Museum;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Simulation.Commands;

public class TogglePathRenderingCommand : ICommand
{
    public void HandleCommand(IMuseumSimulationFacade museumSimulationFacade, Coords mouseGridPosition)
    {
        museumSimulationFacade.ToggleConfigValue(ConfigType.ShouldRenderPath);
    }
}