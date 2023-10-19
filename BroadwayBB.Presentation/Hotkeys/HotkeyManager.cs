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
    private Coords _mouseGridPosition = new (0, 0);
    
    public HotkeyManager()
    {
        Hotkeys.Add(new Hotkey {
                Key = Key.Space, 
                Command = new ToggleArtistMovementCommand(), Description = "Toggle artist movement" });
        
        Hotkeys.Add(new Hotkey { 
            Key = Key.Enter, 
            Command = new UpdateTileCommand(), Description = "Update tile at mouse pointer" });
        
        Hotkeys.Add(new Hotkey {
            Key = Key.O, 
            Command = new OpenFileMenuCommand(), Description = "Open new sim file" });
        
        Hotkeys.Add(new Hotkey {
            Key = Key.A, 
            Command = new ToggleArtistRenderingCommand(), Description = "Toggle artist rendering" });
        
        Hotkeys.Add(new Hotkey {
            Key = Key.Left, 
            Command = new RewindSimulationCommand(), Description = "Rewind simulation" });
        
        Hotkeys.Add(new Hotkey {
            Key = Key.Right, 
            Command = new FastForwardSimulationCommand(), Description = "Fast-forward simulation" });
        
        Hotkeys.Add(new Hotkey {
            Key = Key.M, 
            Command = new OpenShortcutMenuCommand(), Description = "Open shortcut menu" });
        
        Hotkeys.Add(new Hotkey {
            Key = Key.C, 
            Command = new TogglePathCollisionAlgorithmCommand(), Description = "Change collision method (qtree/naive)" });
        
        Hotkeys.Add(new Hotkey {
            Key = Key.Q,
            Command = new ToggleQuadtreeRenderingCommand(), Description = "Toggle quadtree rendering" });
        
        Hotkeys.Add(new Hotkey {
            Key = Key.W,
            Command = new TogglePathCollisionCommand(), Description = "Toggle path collision" });
        
        Hotkeys.Add(new Hotkey { 
            Key = Key.D,
            Command = new ChangePathfindingAlgorithmCommand(), Description = "Change pathfinding method (bfs/dijkstra)" });
        
        Hotkeys.Add(new Hotkey {
            Key = Key.P,
            Command = new TogglePathRenderingCommand(), Description = "Toggle path rendering" });
        
        Hotkeys.Add(new Hotkey {
            Key = Key.V,
            Command = new TogglePathVisitedRenderingCommand(), Description = "Toggle path visited rendering" });
        
        Hotkeys.Add(new Hotkey {
            Key = Key.X,
            Command = new ToggleTileBehaviorCommand(), Description = "Toggle tile behavior" });
    }

    public void UpdateMousePosition(Coords newMousePosition)
    {
        _mouseGridPosition = newMousePosition;
    }
    
    public void HandleCommand(Key pressedKey, IMuseumSimulationFacade museumSimulationFacade)
    {
        var hotkey = Hotkeys.FirstOrDefault(hotkey => hotkey.Key == pressedKey);
        hotkey?.Command.HandleCommand(museumSimulationFacade, _mouseGridPosition);
    }

    public void UpdateHotkey(Key currentKey, Key newKey)
    {
        var current = Hotkeys.First(key => key.Key == currentKey);
        var newCheck = Hotkeys.FirstOrDefault(key => key.Key == newKey);
        if (newCheck == null) current.Key = newKey;
    }
    
    public void HandleCommand(PointerPointProperties pointerProperties, IMuseumSimulationFacade museumSimulationFacade)
    {
        // Technical limitation: We need to handle mouse pointer events separately from the Commands since a Key 
        // cannot contain a mouse pointer button click
        museumSimulationFacade.HandlePointerClick(pointerProperties.IsLeftButtonPressed, _mouseGridPosition);
    }
}