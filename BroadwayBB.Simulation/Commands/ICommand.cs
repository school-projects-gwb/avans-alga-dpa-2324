using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Simulation.Commands;

public interface ICommand
{
    public void HandleCommand(IMuseumSimulationFacade museumSimulationFacade, Coords mouseGridPosition);
}