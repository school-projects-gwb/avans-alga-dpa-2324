using Avalonia.Input;
using BroadwayBB.Simulation.Commands;

namespace BroadwayBB.Presentation.Hotkeys;

public class Hotkey
{
    public Key Key;
    public ICommand Command;
    public string Description;
}