using Avalonia;
using Avalonia.Animation.Easings;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Styling;
using BroadwayBB.Simulation;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Runtime.CompilerServices;
using Avalonia.Controls.Shapes;

namespace BroadwayBB.Presentation;

public partial class MainWindow : Window
{

    private Avalonia.Animation.Animation? _animation1;
    private SimulationWindow? simulation;

    public MainWindow()
    {
        InitializeComponent();
    }
    
    private void ShowSimulation_Click(object sender, RoutedEventArgs e)
    {
        //CreateAnimation1();
        //RunAnimation1((Button)sender);
        // AnimationTest((Button)sender);
        var museum = new Museum(); // Create your Museum instance here

        if (simulation == null)
        {
            var simulationWindow = new SimulationWindow(museum);
            simulationWindow.Show();
            simulation = simulationWindow;
        }
        else
        {

        }
        //this.Close();
    }

    public async void AnimationTest(Button sender)
    {
        double left = sender.Margin.Left;
        double top = sender.Margin.Top;

        for (int i = 0; i < 1000; i++)
        {
            sender.Margin = new Thickness(left + i, top);
            this.StartRendering();
            System.Threading.Thread.Sleep(1000);
        }
    }

    public void CreateAnimation1()
    {
        _animation1 = new Avalonia.Animation.Animation
        {
            Duration = TimeSpan.FromSeconds(10),
            IterationCount = new IterationCount(0, IterationType.Infinite),
            PlaybackDirection = PlaybackDirection.Alternate,
            FillMode = FillMode.None,
            Easing = new SplineEasing(new KeySpline(0.4, 0, 0.6, 1)),
            Delay = TimeSpan.FromSeconds(0),
            DelayBetweenIterations = TimeSpan.FromSeconds(0),
            SpeedRatio = 2d,
            Children =
            {
                new KeyFrame
                {
                    KeyTime = TimeSpan.FromSeconds(0),
                    Setters =
                    {
                        new Setter(MarginProperty, new Thickness(0,0,0,0)),
                        new Setter(Visual.OpacityProperty, 1.0),
                        new Setter(RotateTransform.AngleProperty, 0d),
                    }
                },
                new KeyFrame
                {
                    KeyTime = TimeSpan.FromSeconds(10),
                    Setters =
                    {
                        new Setter(MarginProperty, new Thickness(100,100,100,100)),
                        new Setter(Visual.OpacityProperty, 0.0),
                        new Setter(RotateTransform.AngleProperty, 360d),
                    }
                }
            }
        };
    }

    public void RunAnimation1(Animatable animatable)
    {
        _animation1?.RunAsync(animatable);
        // _disposable1 = _animation1.Apply(animatable, null, _animationTrigger, null);
        // _animation1.RunAsync(animatable, _timelineClock);
    }
}