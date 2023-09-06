namespace BroadwayBB.Simulation.Commands;

public class PauseArtists : ICommand
{
    public void HandleCommand(IMuseumSimulation museumSimulation)
    {
        Console.WriteLine("Testing");
    }
}