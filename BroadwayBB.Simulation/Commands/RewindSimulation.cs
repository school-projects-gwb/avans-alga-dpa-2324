using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Simulation.Commands;

public class RewindSimulation : ICommand
{
    public void HandleCommand(IMuseumSimulation museumSimulation, Coords mouseGridPosition)
    {
        museumSimulation.Rewind();
    }
}