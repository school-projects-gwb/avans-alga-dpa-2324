using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;
using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Helpers;
using BroadwayBB.Common.Entities;
using BroadwayBB.Common.Entities.Interfaces;
using BroadwayBB.Presentation.ObjectPools;
using BroadwayBB.Simulation;

namespace BroadwayBB.Presentation;

public partial class SimulationWindow : Window, ISimulationObserver
{
    private Museum _museum;
    private MuseumSimulation _simulation;
    private readonly Canvas _simulationCanvas;
    private TileObjectPool _tileObjectPool;
    private TileObjectPool _attendeeObjectPool;

    private int _numRows, _numCols;
    private double _tileWidth, _tileHeight, artistSizeModifier = 0.5;

    public SimulationWindow(MainWindow mainWindow)
    {
        InitializeComponent();
        _simulationCanvas = this.FindControl<Canvas>("simulationCanvas") ?? throw new InvalidOperationException();
    }
    
    private void InitializeComponent() => AvaloniaXamlLoader.Load(this);

    public void LoadSimulation(MuseumSimulation simulation)
    {
        _simulation = simulation;
        _simulation.Subscribe(this);
        _museum = _simulation.Museum;
        
        _numRows = _museum.Tiles.Max(tile => tile.PosY) + 1;
        _numCols = _museum.Tiles.Max(tile => tile.PosX) + 1;
        _tileWidth = _simulationCanvas.Width / _numCols;
        _tileHeight = _simulationCanvas.Height / _numRows;

        int maxTilePoolAmount = _numCols * _numRows;
        var tileColors = ColorRegistry.Instance.GetAllColors();
        _tileObjectPool = new TileObjectPool(_tileWidth, _tileHeight, maxTilePoolAmount, tileColors);
        _tileObjectPool.Create();

        int maxAttendeePoolAmount = 175;
        var attendeeColors = new Dictionary<ColorName, RGBColor>();
        attendeeColors.Add(ColorName.Black, ColorRegistry.Instance.GetColor(ColorName.Black));
        _attendeeObjectPool =
            new TileObjectPool(_tileWidth * artistSizeModifier, _tileHeight * artistSizeModifier, maxAttendeePoolAmount, attendeeColors);
        
        DrawMuseum();
    }
    
    private void DrawMuseum()
    {
        _simulationCanvas.Children.Clear();

        foreach (ITile tile in _museum.Tiles)
        {
            Rectangle tileRectangle = _tileObjectPool.GetObject(tile.TileColorBehavior.ColorName);
            
            Canvas.SetLeft(tileRectangle, tile.PosX * _tileWidth);
            Canvas.SetTop(tileRectangle, tile.PosY * _tileHeight);

            _simulationCanvas.Children.Add(tileRectangle);
            _tileObjectPool.MarkForRelease(tile.TileColorBehavior.ColorName, tileRectangle);
        }

        _tileObjectPool.ReleaseMarked();

        foreach (IAttendee artist in _museum.Attendees)
        {
            Rectangle attendeeRectangle = _attendeeObjectPool.GetObject(ColorName.Black);
            
            Canvas.SetLeft(attendeeRectangle, artist.Movement.GridPosX * _tileWidth);
            Canvas.SetTop(attendeeRectangle, artist.Movement.GridPosY * _tileHeight);
            
            _simulationCanvas.Children.Add(attendeeRectangle);
            _attendeeObjectPool.MarkForRelease(ColorName.Black, attendeeRectangle);
        }
        
        _attendeeObjectPool.ReleaseMarked();
    }

    public void UpdateSimulation() => Dispatcher.UIThread.Post(DrawMuseum);
}