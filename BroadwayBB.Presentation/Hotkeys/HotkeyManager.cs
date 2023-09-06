using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Input;
using BroadwayBB.Common.Entities.Structures;
using BroadwayBB.Simulation;
using BroadwayBB.Simulation.Commands;

namespace BroadwayBB.Presentation.Hotkeys;

public class HotkeyManager
{
    public readonly List<Hotkey> Hotkeys = new();
    private readonly MouseGridPosition _mouseGridPosition = new () { PosX = 0, PosY = 0 };
    
    public HotkeyManager()
    {
        Hotkeys.Add(
            new Hotkey()
            {
                Key = Key.Space, 
                Command = new ToggleArtistMovement(),
                Description = "Bewegen van artiesten aan- en uitzetten."
            }
        );
        
        Hotkeys.Add(
            new Hotkey()
            {
                Key = Key.Enter, 
                Command = new UpdateTile(),
                Description = "Herschik vakje waar muispointer zich bevindt"
            }
        );
        
        Hotkeys.Add(
            new Hotkey()
            {
                Key = Key.O, 
                Command = new OpenFileMenu(),
                Description = "Open nieuw simulatiebestand"
            }
        );
        
        Hotkeys.Add(
            new Hotkey()
            {
                Key = Key.A, 
                Command = new ToggleArtistRendering(),
                Description = "Renderen van artiesten aan- en uitzetten"
            }
        );
        
        Hotkeys.Add(
            new Hotkey()
            {
                Key = Key.Left, 
                Command = new RewindSimulation(),
                Description = "Simulatie terugspoelen"
            }
        );
        
        Hotkeys.Add(
            new Hotkey()
            {
                Key = Key.Right,
                Command = new RewindSimulation(),
                Description = "Simulatie vooruitspoelen"
            }
        );
        
        Hotkeys.Add(
            new Hotkey()
            {
                Key = Key.M,
                Command = new OpenShortcutMenu(),
                Description = "Sneltoetsmenu openen"
            }
        );
    }

    public void UpdateMousePosition(int mousePositionX, int mousePositionY)
    {
        _mouseGridPosition.PosX = mousePositionX;
        _mouseGridPosition.PosY = mousePositionY;
    }

    public void HandleCommand(Key pressedKey, IMuseumSimulation museumSimulation)
    {
        var hotkey = Hotkeys.FirstOrDefault(hotkey => hotkey.Key == pressedKey);
        hotkey?.Command.HandleCommand(museumSimulation, _mouseGridPosition);
    }
}