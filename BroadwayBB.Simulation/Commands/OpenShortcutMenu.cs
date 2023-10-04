using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Simulation.Commands;

public class OpenShortcutMenu : ICommand
{
    public void HandleCommand(IMuseumSimulation museumSimulation, Coords mouseGridPosition)
    {
        museumSimulation.OpenShortcutMenu();
    }
}