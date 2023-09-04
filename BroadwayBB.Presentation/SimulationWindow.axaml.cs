using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;
using BroadwayBB.Common.Helpers;
using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Interfaces;
using BroadwayBB.Simulation;

namespace BroadwayBB.Presentation;

public partial class SimulationWindow : Window, ISimulationObserver
{
    private Museum _museum;
    private MuseumSimulation _simulation;
    private readonly Canvas _simulationCanvas;

    private int _numRows, _numCols;
    private double _tileWidth, _tileHeight;

    public SimulationWindow(MainWindow mainWindow)
    {
        InitializeComponent();
        _simulationCanvas = this.FindControl<Canvas>("simulationCanvas") ?? throw new InvalidOperationException();
    }

    public void LoadSimulation(MuseumSimulation simulation)
    {
        _simulation = simulation;
        _simulation.Subscribe(this);
        _museum = _simulation.Museum;
        
        _numRows = _museum.Tiles.Max(tile => tile.PosY) + 1;
        _numCols = _museum.Tiles.Max(tile => tile.PosX) + 1;
        _tileWidth = _simulationCanvas.Width / _numCols;
        _tileHeight = _simulationCanvas.Height / _numRows;
        
        DrawMuseum();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void DrawMuseum()
    {
        _simulationCanvas.Children.Clear();
        
        double artistSizeModifier = 0.5;

        foreach (ITile tile in _museum.Tiles)
        {
            RGBColor tileColor = ColorRegistry.Instance.GetColor(tile.TileColorBehavior.ColorName);
            
            Rectangle tileRectangle = new Rectangle
            {
                Width = _tileWidth,
                Height = _tileHeight,
                Fill = new SolidColorBrush(Color.FromRgb(tileColor.Red, tileColor.Green, tileColor.Blue)),
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };
            
            Canvas.SetLeft(tileRectangle, tile.PosX * _tileWidth);
            Canvas.SetTop(tileRectangle, tile.PosY * _tileHeight);

            _simulationCanvas.Children.Add(tileRectangle);
        }

        foreach (IAttendee artist in _museum.Artists)
        {
            Rectangle attendeeRectangle = new Rectangle
            {
                Width = _tileWidth * artistSizeModifier,
                Height = _tileHeight * artistSizeModifier,
                Fill = new SolidColorBrush(Colors.Black)
            };
            
            Canvas.SetLeft(attendeeRectangle, artist.Movement.GridPosX * _tileWidth);
            Canvas.SetTop(attendeeRectangle, artist.Movement.GridPosY * _tileHeight);
            
            _simulationCanvas.Children.Add(attendeeRectangle);
        }
    }

    public void UpdateSimulation() => Dispatcher.UIThread.Post(DrawMuseum);
}