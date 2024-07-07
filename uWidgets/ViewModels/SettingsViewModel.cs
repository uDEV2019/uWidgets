﻿using System;
using Avalonia.Controls;
using ReactiveUI;
using uWidgets.Core.Interfaces;
using uWidgets.Models;
using Appearance = uWidgets.Views.Pages.Appearance;

namespace uWidgets.ViewModels;

public class SettingsViewModel : ReactiveObject
{
    private readonly IAppSettingsProvider appSettingsProvider;

    private UserControl? currentPage;
    public UserControl? CurrentPage
    {
        get => currentPage;
        set => this.RaiseAndSetIfChanged(ref currentPage, value);
    }

    private string? currentPageTitle;
    public string? CurrentPageTitle
    {
        get => currentPageTitle;
        set => this.RaiseAndSetIfChanged(ref currentPageTitle, value);
    }

    public SettingsViewModel(IAppSettingsProvider appSettingsProvider)
    {
        this.appSettingsProvider = appSettingsProvider;
    }

    public ListItemTemplate[] MenuItems =>
    [
        new ListItemTemplate(typeof(Appearance), "Appearance"),
        new ListItemTemplate(null, "Language"),
        new ListItemTemplate(null, "About")
    ];

    public void SetCurrentPage(ListItemTemplate? value)
    {
        CurrentPage = value?.Type != null
            ? (UserControl?)Activator.CreateInstance(value.Type, appSettingsProvider)
            : null;
        CurrentPageTitle = value?.Text;
    }
}