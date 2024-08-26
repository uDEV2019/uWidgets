using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace uWidgets.ViewModels;

public record ThemeViewModel(bool UseNativeFrame, FontFamily FontFamily, bool IsSelected, Action Apply)
{
    public CornerRadius CornerRadius => new(Radius);
    public double Radius => UseNativeFrame ? 2 : 8;
    public Thickness BorderThickness => new(UseNativeFrame ? 1 : 0);
    public BoxShadows Shadow => UseNativeFrame 
        ? new(BoxShadow.Parse("0 0 10 0 #40000000"))
        : new();

    public Classes Classes => IsSelected ? ["Active"] : [];
}