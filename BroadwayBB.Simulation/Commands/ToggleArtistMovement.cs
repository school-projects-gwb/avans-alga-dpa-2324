using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Simulation.Commands;

public class ToggleArtistMovement : ICommand
{
    public void HandleCommand(IMuseumSimulation museumSimulation, MouseGridPosition mouseGridPosition)
    {
        museumSimulation.ToggleConfigValue(ConfigType.ShouldMoveAttendees);
    }
}