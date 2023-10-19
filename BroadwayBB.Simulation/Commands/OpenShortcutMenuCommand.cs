using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Simulation.Commands;

public class OpenShortcutMenuCommand : ICommand
{
    public void HandleCommand(IMuseumSimulationFacade museumSimulationFacade, Coords mouseGridPosition)
    {
        museumSimulationFacade.OpenShortcutMenu();
    }
}