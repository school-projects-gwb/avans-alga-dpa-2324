using Avalonia.Controls;
using Avalonia.Interactivity;
using BroadwayBB.Common.Models;
using BroadwayBB.Data;

namespace BroadwayBB.Presentation;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    private void ShowSimulation_Click(object sender, RoutedEventArgs e)
    {
        var dataProcessor = new DataProcessor();
        Museum museum = dataProcessor.BuildMuseumFromFile("none"); // TEST ; returns hardcoded museum

        var simulationWindow = new SimulationWindow(museum);
        simulationWindow.Show();
    }
}