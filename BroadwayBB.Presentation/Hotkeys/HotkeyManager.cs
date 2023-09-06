using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Input;
using BroadwayBB.Simulation;
using BroadwayBB.Simulation.Commands;

namespace BroadwayBB.Presentation.Hotkeys;

public class HotkeyManager
{
    private List<Hotkey> _hotKeys = new();
    
    public HotkeyManager()
    {
        _hotKeys.Add(
            new Hotkey()
            {
                Key = Key.Space, 
                Command = new PauseArtists(),
                Description = "Pauzeer artiesten."
            }
        );
    }

    public void HandleCommand(Key pressedKey, IMuseumSimulation museumSimulation)
    {
        var hotkey = _hotKeys.FirstOrDefault(hotkey => hotkey.Key == pressedKey);
        hotkey?.Command.HandleCommand(museumSimulation);
    }
}