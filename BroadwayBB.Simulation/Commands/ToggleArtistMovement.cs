using BroadwayBB.Common.Entities.Museum;
using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Simulation.Commands;

public class ToggleArtistMovement : ICommand
{
    public void HandleCommand(IMuseumSimulation museumSimulation, Coords mouseGridPosition)
    {
        museumSimulation.ToggleConfigValue(ConfigType.ShouldMoveAttendees);
    }
}