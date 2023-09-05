using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;
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
    private IObjectPool<Rectangle> _tileObjectPool;
    private IObjectPool<Rectangle> _attendeeObjectPool;

    private int _numRows, _numCols;
    private double _tileWidth, _tileHeight;
    private readonly double _artistSizeModifier = 0.5;

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
        
        HandleTileConfiguration();
        CreateTileObjectPool();
        CreateAttendeeObjectPool();
    }

    private void HandleTileConfiguration()
    {
        _numRows = _museum.Tiles.Max(tile => tile.PosY) + 1;
        _numCols = _museum.Tiles.Max(tile => tile.PosX) + 1;
        _tileWidth = _simulationCanvas.Width / _numCols;
        _tileHeight = _simulationCanvas.Height / _numRows;
    }

    private void CreateTileObjectPool()
    {
        var config = new ObjectPoolConfiguration()
        {
            MaxPoolAmount = _numCols * _numRows,
            SupportedColors = ColorRegistry.Instance.GetAllColors(),
            ObjectWidth = _tileWidth,
            ObjectHeight = _tileHeight
        };
        
        _tileObjectPool = new CanvasItemPool(config);
    }

    private void CreateAttendeeObjectPool()
    {
        var config = new ObjectPoolConfiguration()
        {
            MaxPoolAmount = 175,
            SupportedColors = new Dictionary<ColorName, RGBColor> { { ColorName.Black, ColorRegistry.Instance.GetColor(ColorName.Black) } },
            ObjectWidth = _tileWidth * _artistSizeModifier,
            ObjectHeight = _tileHeight * _artistSizeModifier
        };
        
        _attendeeObjectPool = new CanvasItemPool(config);
    }
    
    private void DrawMuseum()
    {
        _simulationCanvas.Children.Clear();
        DrawTiles();
        DrawAttendees();
    }

    private void DrawTiles()
    {
        foreach (ITile tile in _museum.Tiles)
        {
            double posX = tile.PosX * _tileWidth, posY = tile.PosY * _tileHeight;
            DrawCanvasItem(_tileObjectPool, tile.TileColorBehavior.ColorName, posX, posY);
        }

        _tileObjectPool.ReleaseMarked();
    }

    private void DrawAttendees()
    {
        foreach (IAttendee artist in _museum.Attendees)
        {
            double posX = artist.Movement.GridPosX * _tileWidth, posY = artist.Movement.GridPosY * _tileHeight;
            DrawCanvasItem(_attendeeObjectPool, ColorName.Black, posX, posY);
        }
        
        _attendeeObjectPool.ReleaseMarked();
    }

    private void DrawCanvasItem(IObjectPool<Rectangle> objectPool, ColorName itemColor, double posX, double posY)
    {
        var item = objectPool.GetObject(itemColor);
        if (item == null) return;
        
        Canvas.SetLeft(item, posX);
        Canvas.SetTop(item, posY);
        
        _simulationCanvas.Children.Add(item);
        objectPool.MarkForRelease(itemColor, item);
    }

    public void UpdateSimulation() => Dispatcher.UIThread.Post(DrawMuseum);
}