using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Simulation.Commands;

public class UpdateTileCommand : ICommand
{
    public void HandleCommand(IMuseumSimulationFacade museumSimulationFacade, Coords mouseGridPosition)
    {
        museumSimulationFacade.UpdateTile(mouseGridPosition);
    }
}