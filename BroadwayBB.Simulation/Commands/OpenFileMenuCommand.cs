using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Simulation.Commands;

public class OpenFileMenuCommand : ICommand
{
    public void HandleCommand(IMuseumSimulationFacade museumSimulationFacade, Coords mouseGridPosition)
    {
        museumSimulationFacade.OpenFileMenu();
    }
}