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
        Hotkeys.Add(new Hotkey {
                Key = Key.Space, 
                Command = new ToggleArtistMovement(), Description = "Toggle artist movement" });
        
        Hotkeys.Add(new Hotkey { 
            Key = Key.Enter, 
            Command = new UpdateTile(), Description = "Update tile at mouse pointer" });
        
        Hotkeys.Add(new Hotkey {
            Key = Key.O, 
            Command = new OpenFileMenu(), Description = "Open new sim file" });
        
        Hotkeys.Add(new Hotkey {
            Key = Key.A, 
            Command = new ToggleArtistRendering(), Description = "Toggle artist rendering" });
        
        Hotkeys.Add(new Hotkey {
            Key = Key.Left, 
            Command = new RewindSimulation(), Description = "Rewind simulation" });
        
        Hotkeys.Add(new Hotkey {
            Key = Key.Right, 
            Command = new FastForwardSimulation(), Description = "Fast-forward simulation" });
        
        Hotkeys.Add(new Hotkey {
            Key = Key.M, 
            Command = new OpenShortcutMenu(), Description = "Open shortcut menu" });
        
        Hotkeys.Add(new Hotkey {
            Key = Key.C, 
            Command = new TogglePathCollisionAlgorithm(), Description = "Change collision method (qtree/naive)" });
        
        Hotkeys.Add(new Hotkey {
            Key = Key.Q,
            Command = new ToggleQuadtreeRendering(), Description = "Toggle quadtree rendering" });
        
        Hotkeys.Add(new Hotkey {
            Key = Key.W,
            Command = new TogglePathCollision(), Description = "Toggle path collision" });
        
        Hotkeys.Add(new Hotkey { 
            Key = Key.D,
            Command = new ChangePathfindingAlgorithm(), Description = "Change pathfinding method (bfs/dijkstra)" });
        
        Hotkeys.Add(new Hotkey {
            Key = Key.P,
            Command = new TogglePathRendering(), Description = "Toggle path rendering" });
        
        Hotkeys.Add(new Hotkey {
            Key = Key.V,
            Command = new TogglePathVisitedRendering(), Description = "Toggle path visited rendering" });
        
        Hotkeys.Add(new Hotkey {
            Key = Key.X,
            Command = new ToggleTileBehavior(), Description = "Toggle tile behavior" });
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

    public void UpdateHotkey(Key currentKey, Key newKey)
    {
        var current = Hotkeys.First(key => key.Key == currentKey);
        var newCheck = Hotkeys.FirstOrDefault(key => key.Key == newKey);
        if (newCheck == null) current.Key = newKey;
    }
    
    public void HandleCommand(PointerPointProperties pointerProperties, IMuseumSimulation museumSimulation)
    {
        // Technical limitation: We need to handle mouse pointer events separately from the Commands since a Key 
        // cannot contain a mouse pointer button click
        museumSimulation.HandlePointerClick(pointerProperties.IsLeftButtonPressed, _mouseGridPosition);
    }
}