using Avalonia.Controls;
using Avalonia.Interactivity;
using uWidgets.ViewModels;

namespace uWidgets.Views.Controls;

public partial class ThemeButton : UserControl
{
    public ThemeButton()
    {
        InitializeComponent();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        (DataContext as ThemeViewModel)!.Apply();
    }
}