using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using Avalonia.Styling;
using BroadwayBB.Common.Entities;
using BroadwayBB.Data;
using BroadwayBB.Presentation.Hotkeys;
using BroadwayBB.Presentation.ViewModels;
using BroadwayBB.Simulation;
using System;
using System.IO;

namespace BroadwayBB.Presentation;

public partial class MainWindow : Window
{
    private SimulationWindow? _simulationWindow;
    private ShortcutWindow _shortcutWindow;
    private HotkeyManager _hotkeyManager;
    private string _gridFileLocation;
    private string _artistsFileLocation;

    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
        _hotkeyManager = new HotkeyManager();
        _shortcutWindow = new ShortcutWindow(_hotkeyManager);
        _gridFileLocation = "";
        _artistsFileLocation = "";
    }

    private void ShowSimulation_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is MainWindowViewModel viewModel)
        {
            _simulationWindow = new SimulationWindow(this, _hotkeyManager);
            _simulationWindow.LoadSimulation(LoadMuseumSimulation(viewModel.GridPath, viewModel.ArtistsPath));
            _simulationWindow.Show();
            Hide();
        }
    }

    private async void SelectGrid_Click(object sender, RoutedEventArgs e)
    {
        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(this);

        // Start async operation to open the dialog.
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Grid File",
            FileTypeFilter = new FilePickerFileType[] { new("Grid") { Patterns = new[] { "*.xml" } } },
            AllowMultiple = false
        });

        if (DataContext is MainWindowViewModel viewModel)
        {
            viewModel.GridPath = files[0].Path.LocalPath;
        }
        else
        {
            throw new InvalidProgramException();
        }
    }

    private async void SelectArtists_Click(object sender, RoutedEventArgs e)
    {
        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(this);

        // Start async operation to open the dialog.
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Artists File",
            FileTypeFilter = new FilePickerFileType[] { new("Artists") { Patterns = new[] { "*.csv" } } },
            AllowMultiple = false
        });


        if (files.Count >= 1)
        {
            if (DataContext is MainWindowViewModel viewModel)
            {
                viewModel.ArtistsPath = files[0].Path.LocalPath;
            }
            else { 
                throw new InvalidProgramException();
            }
        }
    }

    private IMuseumSimulation LoadMuseumSimulation(string gridFile, string artistsFile)
    {
        var dataProcessor = new DataProcessor();
        Museum museum = dataProcessor.BuildMuseumFromFiles(gridFile, artistsFile);
        return new MuseumSimulation(museum);
    }

    public void ShowShortcutWindow()
    {
        _shortcutWindow.Show();
        _shortcutWindow.Focus();
    }
}