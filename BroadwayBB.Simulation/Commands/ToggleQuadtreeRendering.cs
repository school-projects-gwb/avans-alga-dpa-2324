using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Simulation.Commands;

public class ToggleQuadtreeRendering : ICommand
{
    public void HandleCommand(IMuseumSimulation museumSimulation, MouseGridPosition mouseGridGridPosition)
    {
        museumSimulation.ToggleConfigValue(ConfigType.ShouldRenderQuadtree);
    }
}