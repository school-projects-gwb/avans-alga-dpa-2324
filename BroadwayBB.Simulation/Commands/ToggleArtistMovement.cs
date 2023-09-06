namespace BroadwayBB.Simulation.Commands;

public class ToggleArtistMovement : ICommand
{
    public void HandleCommand(IMuseumSimulation museumSimulation, MouseGridPosition mouseGridPosition)
    {
        Console.WriteLine(mouseGridPosition.PosX);
    }
}