using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using BroadwayBB.Presentation.Hotkeys;

namespace BroadwayBB.Presentation;

public partial class ShortcutWindow : Window
{
    private readonly HotkeyManager _hotkeyManager = new();
    private ListBox _hotkeysListBox;
    private bool _isHotkeySelected;
    private Key _selectedHotkeyKey;
    
    public ShortcutWindow()
    {
        InitializeComponent();
        Closing += OnClosing;
        Draw();
    }
    
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        _hotkeysListBox = this.FindControl<ListBox>("HotkeysListBox");
        _hotkeysListBox.Tapped += OnHotkeyItemClicked;
        KeyDown += OnKeyDown;
    }
    
    private void OnKeyDown(object? sender, KeyEventArgs args)
    {
        if (!_isHotkeySelected) return;

        _hotkeyManager.UpdateHotkey(_selectedHotkeyKey, args.Key);
        _isHotkeySelected = false;
        Draw();
    }
    
    private void OnClosing(object? sender, WindowClosingEventArgs windowClosingEventArgs)
    {
        windowClosingEventArgs.Cancel = true;
        Hide();
    }
    
    private void OnHotkeyItemClicked(object? sender, RoutedEventArgs e)
    {
        if (_hotkeysListBox.SelectedItem is not ListBoxItem listBoxItem) return;
        
        _isHotkeySelected = true;
        _selectedHotkeyKey = ((Hotkey)listBoxItem.Tag).Key;
        _hotkeysListBox.IsEnabled = false;
    }

    private void Draw()
    {
        _hotkeysListBox.Items.Clear();
        _hotkeysListBox.IsEnabled = true;
        
        double windowHeight = _hotkeysListBox.Height;
        double hotkeyItemHeight = windowHeight / _hotkeyManager.Hotkeys.Count;
        
        foreach (var hotkey in _hotkeyManager.Hotkeys)
        {
            var item = new ListBoxItem
            {
                Content = $"{hotkey.Description}: {hotkey.Key}",
                Height = hotkeyItemHeight,
                Tag = hotkey
            };

            _hotkeysListBox.Items.Add(item);
        }
    }

    public HotkeyManager GetHotKeyManager()
    {
        return _hotkeyManager;
    }
}