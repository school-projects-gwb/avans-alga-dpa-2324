using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Simulation.Commands;

public class FastForwardSimulationCommand : ICommand
{
    public void HandleCommand(IMuseumSimulationFacade museumSimulationFacade, Coords mouseGridPosition)
    {
        museumSimulationFacade.FastForward();
    }
}