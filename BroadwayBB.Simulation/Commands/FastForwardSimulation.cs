using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Simulation.Commands;

public class FastForwardSimulation : ICommand
{
    public void HandleCommand(IMuseumSimulation museumSimulation, MouseGridPosition mouseGridGridPosition)
    {
        museumSimulation.FastForward();
    }
}