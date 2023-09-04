using Avalonia.Controls;
using Avalonia.Interactivity;
using BroadwayBB.Common.Entities;
using BroadwayBB.Data;
using BroadwayBB.Simulation;

namespace BroadwayBB.Presentation;

public partial class MainWindow : Window
{
    private SimulationWindow _simulationWindow;

    public MainWindow()
    {
        InitializeComponent();
        _simulationWindow = new SimulationWindow(this);
    } 
    
    private void ShowSimulation_Click(object sender, RoutedEventArgs e)
    {
        _simulationWindow.LoadSimulation(LoadMuseumSimulation("/"));
        _simulationWindow.Show();
        Hide();
    }

    private MuseumSimulation LoadMuseumSimulation(string filePath)
    {
        var dataProcessor = new DataProcessor();
        Museum museum = dataProcessor.BuildMuseumFromFile("none");
        return new MuseumSimulation(museum);
    }
}