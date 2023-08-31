using Avalonia.Controls;
using Avalonia.Interactivity;
using BroadwayBB.Simulation;

namespace BroadwayBB.Presentation;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    private void ShowSimulation_Click(object sender, RoutedEventArgs e)
    {
        var museum = new Museum(); // Create your Museum instance here

        var simulationWindow = new SimulationWindow(museum);
        simulationWindow.Show();
    }
}