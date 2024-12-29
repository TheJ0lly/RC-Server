using Avalonia.Controls;
using Avalonia.Interactivity;
using Server.Controller;

namespace Server;

public partial class MainWindow : Window
{
    private readonly MainWindowController _controller;
    public MainWindow()
    {
        InitializeComponent();
        _controller = new MainWindowController();
        DataContext = _controller;
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button button) return;
        
        var res = _controller.SwitchPane();
        button.Content = res ? ">" : "<";
    }
}