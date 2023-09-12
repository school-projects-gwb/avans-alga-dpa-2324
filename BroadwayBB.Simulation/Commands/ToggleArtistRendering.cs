using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Simulation.Commands;

public class ToggleArtistRendering : ICommand
{
    public void HandleCommand(IMuseumSimulation museumSimulation, MouseGridPosition mouseGridGridPosition)
    {
        museumSimulation.ToggleAttendeeRendering();
    }
}