using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BroadwayBB.Presentation.Hotkeys;

namespace BroadwayBB.Presentation;

public partial class ShortcutWindow : Window
{
    private HotkeyManager _hotkeyManager;
    
    public ShortcutWindow(HotkeyManager hotkeyManager)
    {
        InitializeComponent();
        _hotkeyManager = hotkeyManager;
        Closing += OnClosing;
        Draw();
    }
    
    private void OnClosing(object? sender, WindowClosingEventArgs windowClosingEventArgs)
    {
        windowClosingEventArgs.Cancel = true;
        Hide();
    }

    private void Draw()
    {
        var hotkeysListBox = this.FindControl<ListBox>("HotkeysListBox");
        double windowHeight = hotkeysListBox.Height;
        double hotkeyItemHeight = windowHeight / _hotkeyManager.Hotkeys.Count;
        
        foreach (var hotkey in _hotkeyManager.Hotkeys)
        {
            var item = new ListBoxItem
            {
                Content = $"{hotkey.Description}: {hotkey.Key}",
                Height = hotkeyItemHeight
            };

            hotkeysListBox.Items.Add(item);
        }
    }
}