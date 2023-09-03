using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using BroadwayBB.Common.Helpers;
using BroadwayBB.Common.Models;
using BroadwayBB.Common.Models.Interfaces;

namespace BroadwayBB.Presentation;

public partial class SimulationWindow : Window
{
    private Museum _museum;
    private Canvas _simulationCanvas;

    private int _cellSize = 10;

    public SimulationWindow(Museum museum)
    {
        InitializeComponent();
        _museum = museum;
        _simulationCanvas = this.FindControl<Canvas>("simulationCanvas");

        DrawMuseum();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void DrawMuseum()
    {
        if (_museum == null || _museum.Tiles == null) return;
        
        int numRows = _museum.Tiles.Max(tile => tile.PosY) + 1;
        int numCols = _museum.Tiles.Max(tile => tile.PosX) + 1;
        double tileWidth = _simulationCanvas.Width / numCols;
        double tileHeight = _simulationCanvas.Height / numRows;
        double artistSizeModifier = 0.5;

        foreach (ITile tile in _museum.Tiles)
        {
            RGBColor tileColor = ColorRegistry.Instance.GetColor(tile.TileColorBehavior.ColorName);
            
            Rectangle rect = new Rectangle
            {
                Width = tileWidth,
                Height = tileHeight,
                Fill = new SolidColorBrush(Color.FromRgb(tileColor.Red, tileColor.Green, tileColor.Blue)),
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };
            
            Canvas.SetLeft(rect, tile.PosX * tileWidth);
            Canvas.SetTop(rect, tile.PosY * tileHeight);

            _simulationCanvas.Children.Add(rect);
        }

        foreach (IAttendee artist in _museum.Artists)
        {
            Rectangle rect = new Rectangle
            {
                Width = tileWidth * artistSizeModifier,
                Height = tileHeight * artistSizeModifier,
                Fill = new SolidColorBrush(Colors.Black),
            };
            
            Canvas.SetLeft(rect, artist.Movement.GridPosX * tileWidth);
            Canvas.SetTop(rect, artist.Movement.GridPosY * tileHeight);
            
            _simulationCanvas.Children.Add(rect);
        }
    }
}