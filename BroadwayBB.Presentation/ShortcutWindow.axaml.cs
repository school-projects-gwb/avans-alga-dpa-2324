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
    }
}