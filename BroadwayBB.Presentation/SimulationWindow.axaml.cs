using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;
using BroadwayBB.Common.Behaviors;
using BroadwayBB.Common.Entities.Attendees;
using BroadwayBB.Common.Helpers;
using BroadwayBB.Presentation.Hotkeys;
using BroadwayBB.Presentation.ObjectPools;
using BroadwayBB.Simulation;

namespace BroadwayBB.Presentation;

public partial class SimulationWindow : Window, ISimulationObserver
{
    private IMuseumSimulation _simulation;
    private readonly MainWindow _mainWindow;
    private readonly HotkeyManager _hotkeyManager;
    
    private Canvas _backgroundCanvas;
    private Canvas _debugCanvas;
    private Canvas _simulationCanvas;
    
    private IObjectPool<Rectangle> _tileObjectPool;
    private IObjectPool<Rectangle> _attendeeObjectPool;

    private int _numRows, _numCols;
    private double _tileWidth, _tileHeight;
    private readonly double _artistSizeModifier = 0.5;
    
    private readonly Dictionary<(double X, double Y), Rectangle> _tileRectangles = new();
    private readonly Dictionary<ColorName, SolidColorBrush> _colorMap = new();

    public SimulationWindow(MainWindow mainWindow, HotkeyManager hotkeyManager)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
        _hotkeyManager = hotkeyManager;

        var x = this.FindControl<Grid>("simulationGrid") ?? throw new InvalidOperationException();
        x.Focusable = true;
        x.Focus();
        x.KeyDown += (sender, e) => _hotkeyManager.HandleCommand(e.Key, _simulation);
        x.PointerMoved += HandlePointerMoved;
        
        InitiateColorMap();
        InitiateSimulationCanvas();
        InitiateTileCanvas();
        Closed += (sender, e) => _mainWindow.Show();
    }

    private void InitiateSimulationCanvas()
    {
        _simulationCanvas = this.FindControl<Canvas>("simulationCanvas") ?? throw new InvalidOperationException();
        _debugCanvas = this.FindControl<Canvas>("debugInfoCanvas") ?? throw new InvalidOperationException(); 
    }

    private void InitiateColorMap()
    {
        foreach (var colorRecord in ColorRegistryHelper.GetInstance.GetAllColors())
        {
            Color rgbColor = Color.FromRgb(colorRecord.Value.Red, colorRecord.Value.Green, colorRecord.Value.Blue);
            _colorMap[colorRecord.Key] = new SolidColorBrush(rgbColor);
        }
    }

    private void InitiateTileCanvas()
    {
        _backgroundCanvas = this.FindControl<Canvas>("backgroundCanvas") ?? throw new InvalidOperationException();
    }

    private void HandlePointerMoved(object? sender, PointerEventArgs e)
    {
        var pointerPosition = e.GetPosition(_simulationCanvas);
        
        int mouseGridPosX = (int)(pointerPosition.X / _tileWidth);
        int mouseGridPosY = (int)(pointerPosition.Y / _tileHeight);
        
        mouseGridPosX = Math.Max(0, Math.Min(mouseGridPosX, _numCols - 1));
        mouseGridPosY = Math.Max(0, Math.Min(mouseGridPosY, _numRows - 1));
        
        _hotkeyManager.UpdateMousePosition(mouseGridPosX, mouseGridPosY);
    }
    
    private void InitializeComponent() => AvaloniaXamlLoader.Load(this);

    public void LoadSimulation(IMuseumSimulation simulation)
    {
        _simulation = simulation;
        _simulation.Subscribe(this);
        HandleTileConfiguration();
        CreateTileObjectPool();
        CreateAttendeeObjectPool();
        InitiateTileObjects();
    }

    private void HandleTileConfiguration()
    {
        _numRows = _simulation.GetMuseumTiles().Max(tile => tile.PosY) + 1;
        _numCols = _simulation.GetMuseumTiles().Max(tile => tile.PosX) + 1;
        _tileWidth = _simulationCanvas.Width / _numCols;
        _tileHeight = _simulationCanvas.Height / _numRows;
    }

    private void CreateTileObjectPool()
    {
        int maxPercentageOfTilesPerColor = _numCols * _numRows;
        
        var config = new ObjectPoolConfiguration
        {
            MaxPoolAmount = maxPercentageOfTilesPerColor, 
            SupportedColors = ColorRegistryHelper.GetInstance.GetAllColors(),
            ObjectWidth = _tileWidth,
            ObjectHeight = _tileHeight
        };
        
        _tileObjectPool = new CanvasItemPool(config);
    }

    private void CreateAttendeeObjectPool()
    {
        var config = new ObjectPoolConfiguration
        {
            MaxPoolAmount = _simulation.GetMaxMuseumAttendees(),
            SupportedColors = new Dictionary<ColorName, RgbColor>
            {
                { ColorName.Black, ColorRegistryHelper.GetInstance.GetColor(ColorName.Black) },
                { ColorName.Red, ColorRegistryHelper.GetInstance.GetColor(ColorName.Red) }
            },
            ObjectWidth = _tileWidth * _artistSizeModifier,
            ObjectHeight = _tileHeight * _artistSizeModifier
        };
        
        _attendeeObjectPool = new CanvasItemPool(config);
    }
    
    private void DrawMuseumAttendees()
    {
        _simulationCanvas.Children.Clear();
        DrawAttendees();
    }
    
    private void DrawMuseumBackground() => UpdateTileColors();

    private void UpdateTileColors()
    {
        foreach (var tile in _simulation.GetMuseumTiles())
        {
            double posX = tile.PosX * _tileWidth, posY = tile.PosY * _tileHeight;
            if (!_tileRectangles.TryGetValue((posX, posY), out Rectangle rectangle)) continue;
            
            rectangle.Fill = _colorMap[tile.ColorBehaviorStrategy.ColorName];
        }
    }

    private void InitiateTileObjects()
    {
        foreach (var tile in _simulation.GetMuseumTiles())
        {
            double posX = tile.PosX * _tileWidth, posY = tile.PosY * _tileHeight;
            var item = _tileObjectPool.GetObject(tile.ColorBehaviorStrategy.ColorName);
            if (item == null) return;
        
            DrawCanvasItem(item, posX, posY, _backgroundCanvas);
            _tileObjectPool.MarkForRelease(tile.ColorBehaviorStrategy.ColorName, item);
            _tileRectangles[(posX, posY)] = item;
        }
            
        _tileObjectPool.ReleaseMarked();
    }

    private void DrawAttendees()
    {
        foreach (IAttendee artist in _simulation.GetMuseumAttendees())
        {
            double posX = artist.Movement.GridPosX * _tileWidth, posY = artist.Movement.GridPosY * _tileHeight;
            var color = artist.Movement.IsColliding ? ColorName.Red : ColorName.Black;
            var item = _attendeeObjectPool.GetObject(color);
            if (item == null) return;
            
            DrawCanvasItem(item, posX, posY, _simulationCanvas);
            _attendeeObjectPool.MarkForRelease(color, item);
        }
        
        _attendeeObjectPool.ReleaseMarked();
    }

    private void DrawCanvasItem(Rectangle item, double posX, double posY, Canvas canvas)
    {
        Canvas.SetLeft(item, posX);
        Canvas.SetTop(item, posY);
        
        canvas.Children.Add(item);
    }

    private void DrawDebugInfo()
    {
        _debugCanvas.Children.Clear();
        
        foreach (var rect in _simulation.GetDebugInfo())
        {
            var item = new Rectangle
            {
                Width = rect.Width * _tileWidth,
                Height = rect.Height * _tileHeight,
                Stroke = Brushes.Red,
                StrokeThickness = 1.5
            };
            
            DrawCanvasItem(item, rect.X * _tileWidth, rect.Y * _tileHeight, _debugCanvas);
        }
    }
    
    public void UpdateSimulation() => Dispatcher.UIThread.Post(DrawMuseumAttendees);

    public void UpdateBackground() => Dispatcher.UIThread.Post(DrawMuseumBackground);

    public void UpdateDebugInfo() => Dispatcher.UIThread.Post(DrawDebugInfo);

    public void OpenShortcutMenu() => _mainWindow.ShowShortcutWindow();
    
    public void StopSimulation()
    {
        _mainWindow.Show();
        _mainWindow.Focus();
        Close();
    }
}