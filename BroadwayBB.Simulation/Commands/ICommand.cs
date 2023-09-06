namespace BroadwayBB.Simulation.Commands;

public interface ICommand
{
    public void HandleCommand(IMuseumSimulation museumSimulation);
}