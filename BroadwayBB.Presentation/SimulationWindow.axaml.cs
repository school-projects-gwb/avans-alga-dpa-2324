using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using BroadwayBB.Common.Models;

namespace BroadwayBB.Presentation;

public partial class SimulationWindow : Window
{
    private Museum _museum;
    private Canvas _simulationCanvas;

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
        Random random = new Random();
        int cellSize = 30;

        for (int row = 0; row < _museum.Rows; row++)
        {
            for (int col = 0; col < _museum.Columns; col++)
            {
                Rectangle rect = new Rectangle
                {
                    Width = cellSize,
                    Height = cellSize,
                    Fill = new SolidColorBrush(RandomColor(random)),
                    Stroke = Brushes.Black,
                    StrokeThickness = 1
                };

                Canvas.SetLeft(rect, col * cellSize);
                Canvas.SetTop(rect, row * cellSize);

                _simulationCanvas.Children.Add(rect);
            }
        }
    }

    private Color RandomColor(Random random)
    {
        byte r = (byte)random.Next(256);
        byte g = (byte)random.Next(256);
        byte b = (byte)random.Next(256);
        return Color.FromRgb(r, g, b);
    }
}