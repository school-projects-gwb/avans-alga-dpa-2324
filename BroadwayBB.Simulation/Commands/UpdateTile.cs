using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Simulation.Commands;

public class UpdateTile : ICommand
{
    public void HandleCommand(IMuseumSimulation museumSimulation, MouseGridPosition mouseGridGridPosition)
    {
        museumSimulation.UpdateTile(mouseGridGridPosition);
    }
}