using Avalonia.Controls;
using Avalonia.Interactivity;
using BroadwayBB.Common.Entities;
using BroadwayBB.Data;
using BroadwayBB.Presentation.Hotkeys;
using BroadwayBB.Simulation;

namespace BroadwayBB.Presentation;

public partial class MainWindow : Window
{
    private SimulationWindow _simulationWindow;
    private HotkeyManager _hotkeyManager;

    public MainWindow()
    {
        InitializeComponent();
        _hotkeyManager = new HotkeyManager();
    } 
    
    private void ShowSimulation_Click(object sender, RoutedEventArgs e)
    {
        _simulationWindow = new SimulationWindow(this, _hotkeyManager);
        _simulationWindow.LoadSimulation(LoadMuseumSimulation("/"));
        _simulationWindow.Show();
        Hide();
    }

    private IMuseumSimulation LoadMuseumSimulation(string filePath)
    {
        var dataProcessor = new DataProcessor();
        Museum museum = dataProcessor.BuildMuseumFromFile("none");
        return new MuseumSimulation(museum);
    }
}