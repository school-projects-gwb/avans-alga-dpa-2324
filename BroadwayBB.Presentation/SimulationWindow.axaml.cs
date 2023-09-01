using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;
using BroadwayBB.Simulation;

namespace BroadwayBB.Presentation;

public partial class SimulationWindow : Window
{
    private Museum _museum;
    public Canvas _simulationCanvas;
    private int rectPos =0;
    private DispatcherTimer _timer;

    public SimulationWindow(Museum museum)
    {
        InitializeComponent();
        _museum = museum;
        _simulationCanvas = this.FindControl<Canvas>("simulationCanvas");

        DrawMuseum();

        // Create a timer
        _timer = new DispatcherTimer();
        _timer.Interval = TimeSpan.FromMilliseconds(1000 / 60); // 60 frames per second
        _timer.Tick += Timer_Tick;
        _timer.Start();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        _simulationCanvas.Children.Clear();
        DrawMuseum();

        Rectangle rect = new Rectangle
        {
            Width = 5,
            Height = 5,
            Fill = new SolidColorBrush(Color.Parse("Yellow")),
            Stroke = Brushes.Black,
            StrokeThickness = 1
        };

        Canvas.SetLeft(rect, rectPos);
        Canvas.SetTop(rect, rectPos);
        if (rectPos > 100)
        {
            rectPos = 0;
        }
        rectPos++;
        _simulationCanvas.Children.Add(rect);
    }

    public void DrawMuseum()
    {
        int cellSize = 30;

        for (int row = 0; row < _museum.Rows; row++)
        {
            for (int col = 0; col < _museum.Columns; col++)
            {
                Rectangle rect = new Rectangle
                {
                    Width = cellSize,
                    Height = cellSize,
                    Fill = new SolidColorBrush(RandomColor(row, col)),
                    Stroke = Brushes.Black,
                    StrokeThickness = 1
                };

                Canvas.SetLeft(rect, col * cellSize);
                Canvas.SetTop(rect, row * cellSize);

                _simulationCanvas.Children.Add(rect);
            }
        }
    }

    private Color RandomColor(int row, int col)
    {
        byte r = (byte)(row * 7);
        byte g = (byte)(col * 7);
        byte b = (byte)((row + col) * 4);
        return Color.FromRgb(r, g, b);
    }
}